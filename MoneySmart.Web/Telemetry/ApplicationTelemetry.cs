using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;

namespace MoneySmart.Telemetry;

public class ApplicationTelemetry : ITelemetryService
{
    private readonly TelemetryClient _telemetryClient;

    public ApplicationTelemetry(TelemetryClient telemetryClient)
    {
        _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
    }

    public void TrackEvent(string eventName, IDictionary<string, string> properties = null)
    {
        _telemetryClient.TrackEvent(eventName, properties); _telemetryClient.Context.User.Id = "123";
    }
}