using ECommService.Data;
using Microsoft.EntityFrameworkCore;
using OrderService.ServiceClients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IInventoryServiceClient, InventoryServiceClient>();

builder.Services.AddHttpClient<InventoryServiceClient>("InventoryService", client => {
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
