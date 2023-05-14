using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttSupport;

/// <summary>
/// Support helper class 
/// </summary>
public static class Helper
{
    private const string TopicSeparator = "/";
    static readonly string MultiWildcard = "#";
    static readonly string Wildcard = "+";
    /// <summary>
    /// Matches the subscription topic with raw topic received from the MQTT Server
    /// reference : https://codegolf.stackexchange.com/questions/188397/mqtt-subscription-topic-match
    /// </summary>
    /// <param name="subscriptionTopic"></param>
    /// <param name="rawTopic"></param>
    /// <returns></returns>
    public static bool MatchTopic(String subscriptionTopic, String rawTopic)
    {
        if (subscriptionTopic.Equals(MultiWildcard))
        {
            return true;
        }
        // if the topics are an exact match, bail early with a cheap comparison
        if (subscriptionTopic.Equals(rawTopic))
        {
            return true;
        }
        string[] rawTopicFragments = rawTopic.Split(TopicSeparator);
        string[] subscriptionTopicFragments = subscriptionTopic.Split(TopicSeparator);

        for (var i = 0; i < subscriptionTopicFragments.Length; i++)
        {
            var lhsFragment = subscriptionTopicFragments[i];
            if (lhsFragment.Equals(MultiWildcard))
            {
                return true;
            }
            var isLhsWildcard = lhsFragment.Equals(Wildcard);

            if (isLhsWildcard && rawTopicFragments.Length <= i)
            {
                return false;
            }
            if (!isLhsWildcard)
            {
                var rhsFragment = rawTopicFragments[i];
                // if lhs fragment is not wildcard then we need an exact match 
                if (!lhsFragment.Equals(rhsFragment))
                {
                    return false;
                }
            }

            if (i + 1 == subscriptionTopicFragments.Length &&
                rawTopicFragments.Length > subscriptionTopicFragments.Length)
            {
                return false;
            }
        }
        return true;
    }

}
