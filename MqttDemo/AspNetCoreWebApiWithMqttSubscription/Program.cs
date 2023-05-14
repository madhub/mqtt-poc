using AspNetCoreWebApiWithMqttSubscription.TopicProcessors;
using MqttSupport;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMqttSupport(builder.Configuration)
    .AddTopicSubscription<NotifyTopicProcessor>("msdemo/events/notify/configchange")   // subscribe to config change
    .AddTopicSubscription<PingTopicProcessor>("msdemo/events/command/ping")  // subscribe to ping
    .AddTopicSubscription<AllTopicProcessor>("msdemo/events/#"); // subscribe to all events

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
