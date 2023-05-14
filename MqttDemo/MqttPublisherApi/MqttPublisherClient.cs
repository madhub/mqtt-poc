using MQTTnet.Extensions.ManagedClient;
using MQTTnet;
using MqttSupport;
using MQTTnet.Client;
using MQTTnet.Formatter;
using Microsoft.Extensions.Options;
using MQTTnet.Server;

namespace MqttPublisherApi;

public class MqttPublisherClient
{
    private readonly IOptions<Mqtt> mqttOptions;
    private MqttFactory mqttFactory;
    private IManagedMqttClient managedMqttClient;

    public MqttPublisherClient(IOptions<Mqtt> config)
	{
        mqttFactory = new MqttFactory();
        managedMqttClient = mqttFactory.CreateManagedMqttClient();
        this.mqttOptions = config;
    }
    public async Task ConnectAsync()
    {
        var mqttClientOptions = new MqttClientOptionsBuilder()
       .WithClientId(mqttOptions.Value.ClientId)
       .WithCleanSession(true)
       .WithTls(new MqttClientOptionsBuilderTlsParameters()
       {
           UseTls = true,
           AllowUntrustedCertificates = true,
           CertificateValidationHandler = ctx => // ignore cert validation errors
           {
               return true;
           }
       })
       .WithWillQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
       .WithProtocolVersion(MqttProtocolVersion.V500)
       .WithConnectionUri(mqttOptions.Value.Endpoint).Build();
        // build managed MqttClient option 
        var managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
           .WithClientOptions(mqttClientOptions)
           .Build();
        await managedMqttClient.StartAsync(managedMqttClientOptions);

    }
    public async Task DisconnectAsync()
    {
        await managedMqttClient.StopAsync();
    }
    public async Task PublisMessage(String topic,String message)
    {
       
        var applicationMessage = new MqttApplicationMessageBuilder()
         .WithTopic(topic)
         .WithPayload(message)
         .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
         .Build();

        await managedMqttClient.EnqueueAsync(new ManagedMqttApplicationMessage() { ApplicationMessage = applicationMessage });
    }

}
