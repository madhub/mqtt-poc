using DynamicLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MqttPublisherApi;
using MqttSupport;
using MqttWorkerServiceSubscriber;
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Mqtt>(builder.Configuration.GetSection("mqtt"));
// Add services to the container.
builder.Configuration.AddLoggingConfiguration("logging");
builder.Services.AddHostedService<MqttManagedConnectionWorker>();
builder.Services.AddSingleton<MqttPublisherClient>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
