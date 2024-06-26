﻿using Polly;
using Polly.CircuitBreaker;
using Polly.Contrib.WaitAndRetry;
using System.Net;

namespace OrderService.ServiceClients;

public class InventoryServiceClient : IInventoryServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<InventoryServiceClient> _logger;

    private static readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy =
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(resp => resp.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
            //.RetryAsync(2);
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3));

    private static readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakPolicy =
        Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 503)
        .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));
         
    public InventoryServiceClient(HttpClient httpClient, 
        ILogger<InventoryServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<int> ReduceInventoryAsync(int productId, int count)
    {
        var response = await _circuitBreakPolicy.ExecuteAsync(() =>
            _retryPolicy.ExecuteAsync(() =>
            _httpClient.GetAsync($"inventory/reduce/{productId}/{count}")));

        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) {
            return -1;
        }

        return await response.Content.ReadFromJsonAsync<int>();
    }
}
