﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLogging;

// <summary>
/// Changes logging configuration at runtime.
/// </summary>
public class LoggingConfigurationProvider : ConfigurationProvider, ILoggingConfigurationProvider
{
    private readonly IEnumerable<string> _parentPath;

    /// <summary>
    /// Initializes new instance of <see cref="LoggingConfigurationProvider"/>.
    /// </summary>
    /// <param name="parentPath">Path to logging section.</param>
    public LoggingConfigurationProvider(IEnumerable<string> parentPath)
    {
        _parentPath = parentPath ?? throw new ArgumentNullException(nameof(parentPath));

        if (_parentPath.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("The segments of the parent path must be null nor empty.");
    }

    /// <inheritdoc />
    public void SetLevel(LogLevel level, string? category = null, string? provider = null)
    {
        var path = BuildLogLevelPath(category, provider);
        Data[path] = GetLevelName(level);
        OnReload();
    }

    /// <inheritdoc />
    public void ResetLevel(string? category = null, string? provider = null)
    {
        if (!string.IsNullOrEmpty(category) || !string.IsNullOrWhiteSpace(provider))
        {
            var path = BuildLogLevelPath(category, provider);
            Data.Remove(path);
        }
        else
        {
            Data.Clear();
        }

        OnReload();
    }

    private static string GetLevelName(LogLevel level)
    {
        return Enum.GetName(typeof(LogLevel), level) ?? throw new ArgumentException($"Provided value is not a valid {nameof(LogLevel)}: {level}", nameof(level));
    }

    private string BuildLogLevelPath(string? category, string? provider)
    {
        var segments = _parentPath.ToList();

        if (!string.IsNullOrWhiteSpace(provider))
            segments.Add(provider!.Trim());

        segments.Add("LogLevel");
        segments.Add(string.IsNullOrWhiteSpace(category) ? "Default" : category!.Trim());
        return ConfigurationPath.Combine(segments);
    }
}
