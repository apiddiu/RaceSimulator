namespace RaceCarSetup
{
    public class CarConfiguration
    {
        public string ConfigurationName { get; private set; }
        private readonly TrackPerformance[] trackPerformances;


        public CarConfiguration(string configurationName, TrackPerformance[] trackPerformances)
        {
            ConfigurationName = configurationName;
            this.trackPerformances = trackPerformances;
        }

        public TrackPerformance GetPerformanceForTrack(string trackName)
        {
            return ArrayUtils.FindElement(trackPerformances, (t) => t.TrackName.Equals(trackName));
        }
    }
}
