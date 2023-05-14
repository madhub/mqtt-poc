using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLogging;

public class ConfigurationHelper
{
    private readonly IConfigurationRoot configurationRoot;
    private readonly ILoggingConfigurationProvider loggingConfigurationProvider;
    public ConfigurationHelper(IConfiguration configuration)
    {
        configurationRoot = configuration as IConfigurationRoot;
        foreach (var item in configurationRoot.Providers)
        {
            if (item.GetType() == typeof(LoggingConfigurationProvider))
            {
                loggingConfigurationProvider = item as ILoggingConfigurationProvider;
                break;
            }
        }
    }
    public void ChangeLogLevel(LogLevel logLevel, string logCategory)
    {
        loggingConfigurationProvider.SetLevel(logLevel, logCategory);
    }

}
