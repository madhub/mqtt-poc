// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MqttSupport;
using MqttWorkerServiceSubscriber.TopicProcessors;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configure =>
    {
        configure.AddJsonFile("appSettings.json", false, true);
    })
    .ConfigureServices((hostCtx, services) =>
    {
        services.AddMqttSupport(hostCtx.Configuration)
        .AddTopicSubscription<NotifyTopicProcessor>("msdemo/events/notify/configchange")   // subscribe to config change
        .AddTopicSubscription<PingTopicProcessor>("msdemo/events/command/ping")  // subscribe to ping
        .AddTopicSubscription<AllTopicProcessor>("msdemo/events/#"); // subscribe to all events
    })
    .Build();

host.Run();
