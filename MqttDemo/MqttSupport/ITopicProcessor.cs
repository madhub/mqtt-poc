namespace MqttSupport;

/// <summary>
/// Interface for definition for Topic processor implementation
/// </summary>
public interface ITopicProcessor
{
    void Process(string topic, string message);
}
