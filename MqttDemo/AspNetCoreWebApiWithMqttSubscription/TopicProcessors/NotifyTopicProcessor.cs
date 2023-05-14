using MqttSupport;

namespace AspNetCoreWebApiWithMqttSubscription.TopicProcessors;
/// <summary>
/// Process the Notify topic
/// </summary>
public class NotifyTopicProcessor : ITopicProcessor
{
    public void Process(string topic, string message)
    {
        LogHelper.Log($"{nameof(NotifyTopicProcessor)}called with '{topic}' , message '{message}'", ConsoleColor.DarkCyan);
    }
}
