using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MqttSupport;
/// <summary>
/// Helper class to add topic subscription & processors
/// </summary>
public static class TopicSubscriptionBuilderAddCheckExtensions
{
    public static ITopicSubscriptionBuilder AddTopicSubscription<T>(this ITopicSubscriptionBuilder builder, string name) where T : class, ITopicProcessor
    {
        return builder.Add(new TopicSubscriptionRegistration(name, (s) => ActivatorUtilities.GetServiceOrCreateInstance<T>(s)));
    }
}
