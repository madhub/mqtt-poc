using Microsoft.Extensions.DependencyInjection;

namespace MqttSupport;
/// <summary>
/// Interface for building topic subscription
/// </summary>
public interface ITopicSubscriptionBuilder
{
    IServiceCollection Services { get; }

    ITopicSubscriptionBuilder Add(TopicSubscriptionRegistration registration);
}
