using MqttSupport;

namespace AspNetCoreWebApiWithMqttSubscription.TopicProcessors;
/// <summary>
/// Process all the topics from MQTT Broker
/// </summary>
public class AllTopicProcessor : ITopicProcessor
{
    public void Process(string topic, string message)
    {
        LogHelper.Log($"AllTopicProcessor called with '{topic}' , message '{message}'", ConsoleColor.DarkCyan);
    }
}
