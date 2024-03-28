using ECommService.Data;
using Microsoft.EntityFrameworkCore;
using OrderService.ServiceClients;

namespace OrderService;

public static class ConfigureServices
{
    public static void Configure(WebApplicationBuilder builder)
    {
        //builder.Services.AddScoped<IInventoryServiceClient, InventoryServiceClient>();

        builder.Services.AddHttpClient<IInventoryServiceClient, InventoryServiceClient>(client => {
            client.BaseAddress = new Uri(builder.Configuration["InventoryService:BaseAddress"]!);
        });

        builder.Services.AddControllers();

        builder.Services.AddDbContext<OrderDbContext>(opt =>
            opt.UseInMemoryDatabase("OrderDb"));

        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options => {
            options.AddPolicy("MyPolicy",
                policy => {
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
        });
    }

}
