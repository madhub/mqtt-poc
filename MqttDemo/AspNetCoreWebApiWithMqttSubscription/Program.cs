using AspNetCoreWebApiWithMqttSubscription.TopicProcessors;
using MqttSupport;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ITopicSubscriptionBuilder topicSubscriptionBuilder = builder.Services.AddMqttSupport(builder.Configuration);

// subscribe to config change
topicSubscriptionBuilder.AddTopicSubscription<NotifyTopicProcessor>("msdemo/events/notify/configchange");
// subscribe to ping
topicSubscriptionBuilder.AddTopicSubscription<PingTopicProcessor>("msdemo/events/command/ping");
// subscribe to all events
topicSubscriptionBuilder.AddTopicSubscription<AllTopicProcessor>("msdemo/events/#");

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
