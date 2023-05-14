using DynamicLogging;
using Microsoft.Extensions.Logging;
using MqttSupport;

namespace MqttWorkerServiceSubscriber.TopicProcessors;
/// <summary>
/// Process all the topics from MQTT Broker
/// </summary>
public class LogChangeTopicProcessor : ITopicProcessor
{
    private readonly ConfigurationHelper configurationHelper;

    public LogChangeTopicProcessor(ConfigurationHelper configurationHelper)
    {
        this.configurationHelper = configurationHelper;
    }
    public void Process(string topic, string message)
    {
        LogHelper.Log($"LogChangeTopicProcessor  called with '{topic}' , message '{message}'", ConsoleColor.DarkCyan);

        string[] items = message.Split('|');
        var logLevel = items[0];
        var logCategory = items[1];

        if (Enum.TryParse<LogLevel>(logLevel, out var enumlogLevel))
        {
            LogHelper.Log($"Changing the log level '{message}'", ConsoleColor.Yellow);
            configurationHelper.ChangeLogLevel(enumlogLevel, logCategory);
        }
        else
        {
            LogHelper.Log($"Invalid log level '{message}'", ConsoleColor.Green);
        }

    }
}
