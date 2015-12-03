namespace RaceCarSetup
{
    public class CarConfiguration
    {
        private readonly TrackPerformance[] trackPerformances;


        public CarConfiguration(TrackPerformance[] trackPerformances)
        {
            this.trackPerformances = trackPerformances;
        }

        public TrackPerformance GetPerformanceForTrack(string track)
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
