using ECommService.Data;
using Microsoft.EntityFrameworkCore;
using OrderService;
using OrderService.ServiceClients;

var builder = WebApplication.CreateBuilder(args);

#region Services

ConfigureServices.Configure(builder);

#endregion Services

var app = builder.Build();

#region Pipeline

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

#endregion Pipeline

app.Run();
