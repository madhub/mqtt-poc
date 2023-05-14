using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MqttSupport;

/// <summary>
/// Background service that connects to MQTT server , subscribes to topics & dispatche
/// messages to registered topic processors
/// </summary>
public class MonitorService : BackgroundService
{
    private MqttFactory mqttFactory;
    private IManagedMqttClient managedMqttClient;

    private readonly IOptions<Mqtt> mqttOptions;
    private readonly IOptions<TopicSubscriptionOptions> topicSubscription;
    private readonly IServiceScopeFactory scopeFactory;
        
    public MonitorService(IOptions<Mqtt> mqttOptions, 
        IOptions<TopicSubscriptionOptions> topicSubscription,
        IServiceScopeFactory scopeFactory)
    {
        mqttFactory = new MqttFactory();
        managedMqttClient = mqttFactory.CreateManagedMqttClient();
        this.mqttOptions = mqttOptions;
        this.topicSubscription = topicSubscription;
        this.scopeFactory = scopeFactory;
    }
    /// <summary>
    /// Connect to MQTT server, subscribe to topics & Start processing the messages
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

        // Create Subscription Topics
        MqttClientSubscribeOptionsBuilder mqttClientSubscribeOptionsBuilder = mqttFactory.CreateSubscribeOptionsBuilder();
        foreach(var topicRegistration in topicSubscription.Value.Registrations)
        {
            mqttClientSubscribeOptionsBuilder.WithTopicFilter(topicRegistration.TopicName);

        }
        var mqttSubscribeOptions = mqttClientSubscribeOptionsBuilder.Build();

        // Subscribe
        await managedMqttClient.SubscribeAsync(mqttSubscribeOptions.TopicFilters);

        // Register MessageHandler to process messages
        managedMqttClient.ApplicationMessageReceivedAsync += mqttAMEventArgs =>
        {
            ProcessesMessagge(mqttAMEventArgs);

            return Task.CompletedTask;
        };
        LogHelper.Log($"Listening for messages from MQTT Broker", ConsoleColor.DarkYellow);
        await managedMqttClient.StartAsync(managedMqttClientOptions);

    }

   
    /// <summary>
    /// Disconnect from Mqtt Server
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        LogHelper.Log($"StopAsync", ConsoleColor.DarkYellow);
        await managedMqttClient.StopAsync(true);

    }
    /// <summary>
    /// Process MQTT Message
    /// </summary>
    /// <param name="mqttMessageEventArgs"></param>
    private void ProcessesMessagge(MqttApplicationMessageReceivedEventArgs mqttMessageEventArgs)
    {
        var topic = mqttMessageEventArgs.ApplicationMessage.Topic;
        var message = Encoding.UTF8.GetString(mqttMessageEventArgs.ApplicationMessage.PayloadSegment);
        LogHelper.Log($"Received application message from `{mqttMessageEventArgs.ClientId}`, of topic `{topic}' ,with message" +
               $"'{message}'", ConsoleColor.Yellow);
        IServiceScope scope = scopeFactory.CreateScope();

        var filteredList = topicSubscription.Value.Registrations.Where(reg =>
        {
            if (Helper.MatchTopic(reg.TopicName, topic))
            {
                return true;
            }
            else
            {
                return false;
            }
        }).ToList();
        LogHelper.Log($"Filterd Reg Count: {filteredList.Count}", ConsoleColor.DarkRed);

        foreach (var topicRegistration in filteredList)
        {
            var topicProcessor = topicRegistration.Factory(scope.ServiceProvider);
            topicProcessor.Process(topic, message);

        }
    }
}
