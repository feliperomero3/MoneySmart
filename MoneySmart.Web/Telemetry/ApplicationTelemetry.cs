using Microsoft.ApplicationInsights;

namespace MoneySmart.Telemetry;

public class ApplicationTelemetry : ITelemetryService
{
    private readonly TelemetryClient _telemetryClient;

    public ApplicationTelemetry(TelemetryClient telemetryClient)
    {
        _telemetryClient = telemetryClient ?? throw new System.ArgumentNullException(nameof(telemetryClient));
    }

    public void TrackEvent(string eventName)
    {
        _telemetryClient.TrackEvent(eventName);
    }
}