using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttSupport;

/// <summary>
/// POCO class to hold the MQTT configuration
/// </summary>
public record class Mqtt
{
    public string Endpoint { get; init; } = string.Empty;
    public string ClientId { get; init; } = $"{Guid.NewGuid}";
}
