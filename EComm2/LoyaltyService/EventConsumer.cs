using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LoyaltyService;

public class EventConsumer : BackgroundService
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IModel _channel;

    private readonly ILogger<EventConsumer> _logger;

    public EventConsumer(ILogger<EventConsumer> logger)
    {
        _logger = logger;

        _factory = new ConnectionFactory() {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/",
            Port = 5672
        };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "orders",
                                durable: false,
                                exclusive: false,
                                autoDelete: true,
                                arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Shutdown += OnConsumerShutdown;
        consumer.Registered += OnConsumerRegistered;
        consumer.Unregistered += OnConsumerUnregistered;
        consumer.ConsumerCancelled += OnConsumerConsumerCancelled;


        consumer.Received += (model, ea) => {
            _logger.LogInformation("Message Received");
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            _logger.LogInformation(message);
        };


        _channel.BasicConsume(queue: "orders",
                             autoAck: false,
                             consumer: consumer);

        return Task.CompletedTask;
    }

    private void OnConsumerConsumerCancelled(object? sender, ConsumerEventArgs e) { }
    private void OnConsumerUnregistered(object? sender, ConsumerEventArgs e) { }
    private void OnConsumerRegistered(object? sender, ConsumerEventArgs e) { }
    private void OnConsumerShutdown(object? sender, ShutdownEventArgs e) { }
    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e) { }
}
