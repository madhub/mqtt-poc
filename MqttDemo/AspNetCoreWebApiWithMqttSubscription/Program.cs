using AspNetCoreWebApiWithMqttSubscription;
using AspNetCoreWebApiWithMqttSubscription.TopicProcessors;
using DynamicLogging;
using MqttSupport;

var builder = WebApplication.CreateBuilder(args);
 builder.Services.AddSingleton<ConfigurationHelper>(); // refactor AddLoggingConfiguration should add helper
builder.Configuration.AddLoggingConfiguration("logging");
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// add background service to demonstrate the changing logging level
builder.Services.AddHostedService<TimedBackgroundWorker>();

// Add services to the container.

builder.Services.AddMqttSupport(builder.Configuration)
    .AddTopicSubscription<NotifyTopicProcessor>("msdemo/events/notify/configchange")   // subscribe to config change
    .AddTopicSubscription<PingTopicProcessor>("msdemo/events/command/ping")  // subscribe to ping
    .AddTopicSubscription<LogChangeTopicProcessor>("msdemo/events/logging/changeloglevel")
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
