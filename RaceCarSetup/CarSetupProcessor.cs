namespace RaceCarSetup
{
    public class CarSetupProcessor
    {
        private readonly Track[] tracks;
        private readonly IRaceSimulator raceSimulator;

        public CarSetupProcessor(Track[] tracks)
            : this(tracks, new RaceSimulator())
        {

        }

        public CarSetupProcessor(Track[] tracks, IRaceSimulator raceSimulator)
        {
            this.tracks = tracks;
            this.raceSimulator = raceSimulator;
        }

        public RaceSimulationResult BestConfigurationForTrack(string trackName, CarConfiguration[] carConfigurations)
        {
            return RankConfigurationsForTrack(trackName, carConfigurations)[0];
        }

        public RaceSimulationResult[] RankConfigurationsForTrack(string trackName, CarConfiguration[] carConfigurations)
        {
            var track = ArrayUtils.FindElement(tracks, (t) => t.TrackName.Equals(trackName));
            var results = new RaceSimulationResult[carConfigurations.Length];

            foreach (var carConfiguration in carConfigurations)
            {
                var raceResult = raceSimulator.SimulateRace(track, carConfiguration.GetPerformanceForTrack(trackName));
                ArrayUtils.InsertSorted(ref results, BuildResult(raceResult, track, carConfiguration),
                    (t) => (float.IsNaN(t.RaceTime) || t.RaceTime > raceResult));
            }

            return results;
        }

        private RaceSimulationResult BuildResult(float raceTime, Track track, CarConfiguration carConfiguration)
        {
            return new RaceSimulationResult()
            {
                RaceTime = raceTime,
                Track = track,
                CarConfiguration = carConfiguration
            };
        }
    }
}
