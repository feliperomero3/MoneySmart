using System.Collections.Generic;

namespace MoneySmart.Telemetry;

public interface ITelemetryService
{
    void TrackEvent(string eventName, IDictionary<string, string> properties = null);
}