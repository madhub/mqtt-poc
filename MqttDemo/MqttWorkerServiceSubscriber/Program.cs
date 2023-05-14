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
        ITopicSubscriptionBuilder topicSubscriptionBuilder = services.AddMqttSupport(hostCtx.Configuration);
        // subscribe to config change
        topicSubscriptionBuilder.AddTopicSubscription<NotifyTopicProcessor>("msdemo/events/notify/configchange");
        // subscribe to ping
        topicSubscriptionBuilder.AddTopicSubscription<PingTopicProcessor>("msdemo/events/command/ping");
        // subscribe to all events
        topicSubscriptionBuilder.AddTopicSubscription<AllTopicProcessor>("msdemo/events/#");
    })
    .Build();

host.Run();
