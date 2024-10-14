using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;

namespace MoneySmart.Telemetry;

/// <summary>
/// Send telemetry to the Application Insights service.
/// </summary>
public class ApplicationTelemetry : ITelemetryService
{
    private readonly TelemetryClient _telemetryClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationTelemetry"/> class.
    /// </summary>
    /// <param name="telemetryClient">The telemetry client</param>
    /// <exception cref="ArgumentNullException">Throws if telemetryClient is null</exception>
    /// <remarks>
    /// This class is a thin wrapper around the Application Insights <see cref="TelemetryClient"/>.
    /// </remarks>
    public ApplicationTelemetry(TelemetryClient telemetryClient)
    {
        _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
    }

    /// <summary>
    /// Track an event with the username and the event name.
    /// </summary>
    /// <param name="eventName">The event name.</param>
    /// <param name="username">The username.</param>
    public void TrackEvent(string eventName, string username)
    {
        if (eventName == null)
        {
            throw new ArgumentNullException(nameof(eventName));
        }

        if (username == null)
        {
            throw new ArgumentNullException(nameof(username));
        }

        var properties = new Dictionary<string, string> { { TelemetryConstants.UsernameKey, username } };

       TrackEvent(eventName, properties);
    }

    /// <summary>
    /// Track an event with optional properties.
    /// </summary>
    /// <param name="eventName">The event name.</param>
    /// <param name="properties">The (optional) properties.</param>
    public void TrackEvent(string eventName, IDictionary<string, string> properties = null)
    {
        _telemetryClient.TrackEvent(eventName, properties);
    }
}