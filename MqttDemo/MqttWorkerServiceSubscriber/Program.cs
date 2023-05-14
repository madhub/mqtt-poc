// See https://aka.ms/new-console-template for more information
using DynamicLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MqttSupport;
using MqttWorkerServiceSubscriber;
using MqttWorkerServiceSubscriber.TopicProcessors;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configure =>
    {
        configure.AddJsonFile("appSettings.json", false, true);
        configure.AddLoggingConfiguration("logging");
    })
    .ConfigureLogging((_, loggingBuilder) =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();

    })
    .ConfigureServices((hostCtx, services) =>
    {
        services.AddSingleton<ConfigurationHelper>(); // refactor AddLoggingConfiguration should add helper
        services.AddMqttSupport(hostCtx.Configuration)
        .AddTopicSubscription<NotifyTopicProcessor>("msdemo/events/notify/configchange")   // subscribe to config change
        .AddTopicSubscription<PingTopicProcessor>("msdemo/events/command/ping")  // subscribe to ping
        .AddTopicSubscription<AllTopicProcessor>("msdemo/events/#") // subscribe to all events
        .AddTopicSubscription<LogChangeTopicProcessor>("msdemo/events/logging/changeloglevel");
        // add background service to demonstrate the changing logging level
        services.AddHostedService<TimedBackgroundWorker>();
    })
    .Build();

host.Run();
