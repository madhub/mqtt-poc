using Microsoft.Extensions.Configuration;
using MqttSupport;

namespace AspNetCoreWebApiWithMqttSubscription.TopicProcessors;

/// <summary>
/// Process the Ping command topic from MQTT Server
/// </summary>
public class PingTopicProcessor : ITopicProcessor
{
    private readonly IConfiguration configuration;

    public PingTopicProcessor(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public void Process(string topic, string message)
    {
        LogHelper.Log($"PingTopicProcessor called with '{topic}' , message '{message}'", ConsoleColor.DarkCyan);
    }
}
