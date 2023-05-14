namespace MqttPublisherApi;

/// <summary>
/// LogData data model
/// </summary>
/// <param name="logLevel"> Allowed values are , Information,Warning,Trace,Debug,Error,Critical</param>
/// <param name="logCategory">Log category.Ex: AspNetCoreWebApiWithMqttSubscription</param>
public record LogData(string logLevel, string logCategory);