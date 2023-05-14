using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace MqttSupport;

/// <summary>
/// Service collectio extension to register service required for mqtt support
/// </summary>
public static class ServiceCollectionExtensions
{
    public static ITopicSubscriptionBuilder AddMqttSupport(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<Mqtt>(configuration.GetSection("mqtt"));
        serviceCollection.AddHostedService<MonitorService>();
        return new TopicSubscriptionBuilder(serviceCollection);
        ;
    }

   
}
