namespace MqttSupport;

public sealed class TopicSubscriptionRegistration
{
    private string _topicName;
    private Func<IServiceProvider, ITopicProcessor> _factory;
    public string TopicName
    {
        get
        {
            return _topicName;
        }
        private set
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            _topicName = value;
        }
    }
    public Func<IServiceProvider, ITopicProcessor> Factory
    {
        get
        {
            return _factory;
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            _factory = value;
        }
    }

    public TopicSubscriptionRegistration(string topicName, ITopicProcessor topicProcessor)
    {
        if (string.IsNullOrEmpty(topicName))
        {
            throw new ArgumentException($"'{nameof(topicName)}' cannot be null or empty.", nameof(topicName));
        }

        if (topicProcessor is null)
        {
            throw new ArgumentNullException(nameof(topicProcessor));
        }

        TopicName = topicName;
        _factory = (_) => topicProcessor;
    }
    public TopicSubscriptionRegistration(string topicName, Func<IServiceProvider, ITopicProcessor> factory)
    {
        if (string.IsNullOrEmpty(topicName))
        {
            throw new ArgumentException($"'{nameof(topicName)}' cannot be null or empty.", nameof(topicName));
        }
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));

        TopicName = topicName;
        
    }
}
