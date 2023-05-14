using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MqttSupport;

public sealed class TopicSubscriptionOptions
{
    public ICollection<TopicSubscriptionRegistration> Registrations { get; } = new List<TopicSubscriptionRegistration>();

}
