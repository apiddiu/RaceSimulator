namespace RaceCarSetup
{
    public class TrackPerformanceRepository : ITrackPerformanceRepository
    {
        private readonly TrackPerformance[] trackPerformances;

        public TrackPerformanceRepository(TrackPerformance[] trackPerformances)
        {
            this.trackPerformances = trackPerformances;
        }
        
        public TrackPerformance FindPerformanceForTrack(string track)
        {
            foreach (var performance in trackPerformances)
            {
                if (performance.TrackName.Equals(track))
                {
                    return performance;
                }
            }
            return null;
        }
    }
}