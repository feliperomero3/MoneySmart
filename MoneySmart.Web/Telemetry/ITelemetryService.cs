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
}