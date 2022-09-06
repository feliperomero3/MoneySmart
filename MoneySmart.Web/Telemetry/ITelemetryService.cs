namespace MoneySmart.Telemetry
{
    public interface ITelemetryService
    {
        void TrackEvent(string eventName);
    }
}