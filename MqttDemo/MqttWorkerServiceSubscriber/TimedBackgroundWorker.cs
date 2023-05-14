using DynamicLogging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace MqttWorkerServiceSubscriber;

public class TimedBackgroundWorker : BackgroundService
{
    private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
    private readonly ILogger<TimedBackgroundWorker> logger;
    public TimedBackgroundWorker(ILogger<TimedBackgroundWorker> logger)
    {
        this.logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (await timer.WaitForNextTickAsync())
        {
            //Business logic
            logger.LogInformation("This is information logging");
            logger.LogTrace("This is trace logging");
            logger.LogDebug("This is debug logging");
        }

    }
}
