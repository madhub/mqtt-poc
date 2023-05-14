using Microsoft.Extensions.DependencyInjection;

namespace MqttSupport;

internal class TopicSubscriptionBuilder : ITopicSubscriptionBuilder
{
    public IServiceCollection Services { get; }

    public TopicSubscriptionBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public ITopicSubscriptionBuilder Add(TopicSubscriptionRegistration registration)
    {
        TopicSubscriptionRegistration registration2 = registration;
        if (registration2 == null)
        {
            throw new ArgumentNullException("registration");
        }
        Services.Configure(delegate (TopicSubscriptionOptions options)
        {
            options.Registrations.Add(registration2);
        });
        return this;
    }
}
