namespace RaceCarSetup
{
    public interface ITrackPerformanceRepository
    {
        TrackPerformance FindPerformanceForTrack(string track);
    }
}