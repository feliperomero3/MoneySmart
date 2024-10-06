using System.Collections.Generic;

namespace MoneySmart.Telemetry;

/// <summary>
/// Send telemetry to a telemetry service.
/// </summary>
public interface ITelemetryService
{
    /// <summary>
    /// Track an event with optional properties.
    /// </summary>
    /// <param name="eventName">The event name.</param>
    /// <param name="properties">The (optional) properties.</param>
    void TrackEvent(string eventName, IDictionary<string, string> properties = null);

    /// <summary>
    /// Track an event with the username and the event name.
    /// <param name="eventName">The event name.</param>
    /// <param name="username">The username.</param>
    /// </summary>
    void TrackEvent(string eventName, string username);
}