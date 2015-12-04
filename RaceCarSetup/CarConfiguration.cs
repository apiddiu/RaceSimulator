namespace RaceCarSetup
{
    public class CarConfiguration
    {
        private readonly TrackPerformance[] trackPerformances;


        public CarConfiguration(TrackPerformance[] trackPerformances)
        {
            this.trackPerformances = trackPerformances;
        }

        public TrackPerformance GetPerformanceForTrack(string trackName)
        {
            return ArrayUtils.FindElement(trackPerformances, (t) => t.TrackName.Equals(trackName));
        }
    }
}
