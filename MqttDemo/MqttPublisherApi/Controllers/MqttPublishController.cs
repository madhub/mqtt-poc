using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MqttPublisherApi.Controllers;
[ApiController]
[Route("[controller]")]
public class MqttPublishController : ControllerBase
{

    private readonly ILogger<MqttPublishController> _logger;
    private readonly MqttPublisherClient mqttPublisherClient;

    public MqttPublishController(ILogger<MqttPublishController> logger,MqttPublisherClient mqttPublisherClient )
    {
        _logger = logger;
        this.mqttPublisherClient = mqttPublisherClient;
    }

    /// <summary>
    /// Send message to connected clients to with message provided in the payload
    /// </summary>
    /// <param name="messageData"> message payload</param>
    /// <returns></returns>
    [HttpPost]
    [Route("notify")]
    public async Task<ActionResult> Notify([FromBody] MessageData messageData)
    {
        var topic = "msdemo/events/notify/configchange";
        _logger.LogInformation("Publishing a message to MQTT broker");
        await mqttPublisherClient.PublisMessage(topic, messageData.message);

        return Ok();
    }

    /// <summary>
    /// Send message to connected clients to changes log level 
    /// </summary>
    /// <param name="logData">LogData data model</param>
    /// <returns></returns>
    [HttpPost]
    [Route("changloglevel")]
    public async Task<ActionResult> Changloglevel([FromBody] LogData logData)
    {
        var topic = "msdemo/events/logging/changeloglevel";
        _logger.LogInformation("Publishing a message to MQTT broker");
        var logMsg = $"{logData.logLevel}|{logData.logCategory}";
        await mqttPublisherClient.PublisMessage(topic, logMsg);
        return Ok();
    }

}
