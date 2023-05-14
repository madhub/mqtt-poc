using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLogging;

/// <summary>
	/// Logging configuration source for changing logging configuration at runtime.
	/// </summary>
	public class LoggingConfigurationSource : IConfigurationSource
{
    private readonly IEnumerable<string> _parentPath;

    /// <summary>
    /// Initializes new instance of <see cref="LoggingConfigurationSource"/>.
    /// </summary>
    /// <param name="providerCollection">Logging configuration provider collection that newly created providers are going to be added in.</param>
    /// <param name="parentPath">Path to logging section.</param>
    public LoggingConfigurationSource(params string[] parentPath)
    {

        _parentPath = parentPath ?? throw new ArgumentNullException(nameof(parentPath));

        if (_parentPath.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("The segments of the parent path must be null nor empty.");
    }

    /// <inheritdoc />
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        var provider = new LoggingConfigurationProvider(_parentPath);

        return provider;
    }
}
