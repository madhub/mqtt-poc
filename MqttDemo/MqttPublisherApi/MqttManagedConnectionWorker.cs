using MqttPublisherApi;

namespace MqttWorkerServiceSubscriber;

public class MqttManagedConnectionWorker : BackgroundService
{
    private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
    private readonly ILogger<MqttManagedConnectionWorker> logger;
    private readonly MqttPublisherClient mqttPublisherClient;

    public MqttManagedConnectionWorker(ILogger<MqttManagedConnectionWorker> logger, MqttPublisherClient mqttPublisherClient)
    {
        this.logger = logger;
        this.mqttPublisherClient = mqttPublisherClient;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await mqttPublisherClient.ConnectAsync();
        logger.LogInformation("Connected to remote MQTT Broker");
    }
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await mqttPublisherClient.DisconnectAsync();
    }
}
