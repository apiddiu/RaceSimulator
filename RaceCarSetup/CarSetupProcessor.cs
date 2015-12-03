namespace RaceCarSetup
{
    public class CarSetupProcessor
    {
        private readonly ITrackRepository trackRepository;
        private readonly IRaceSimulator raceSimulator;

        public CarSetupProcessor(Track[] tracks)
            : this(new TrackRepository(tracks), new RaceSimulator())
        {

        }

        public CarSetupProcessor(ITrackRepository trackRepository, IRaceSimulator raceSimulator)
        {
            this.trackRepository = trackRepository;
            this.raceSimulator = raceSimulator;
        }

        public RaceSimulationResult BestConfigurationForTrack(string trackName, CarConfiguration[] carConfigurations)
        {
            return RankConfigurationsForTrack(trackName, carConfigurations)[0];
        }

        public RaceSimulationResult[] RankConfigurationsForTrack(string trackName, CarConfiguration[] carConfigurations)
        {
            var track = trackRepository.FindTrack(trackName);
            var results = new RaceSimulationResult[carConfigurations.Length];

            foreach (var carConfiguration in carConfigurations)
            {
                var raceResult = raceSimulator.SimulateRace(track, carConfiguration.GetPerformanceForTrack(trackName));
                InsertSorted(ref results, BuildResult(raceResult, track, carConfiguration));
            }

            return results;
        }

        private RaceSimulationResult BuildResult(float raceTime, Track track, CarConfiguration setup)
        {
            return new RaceSimulationResult()
            {
                RaceTime = raceTime,
                Track = track,
                Setup = setup
            };
        }

        public void InsertSorted(ref RaceSimulationResult[] inputArray, RaceSimulationResult newElement)
        {
            for (var i = 0; i < inputArray.Length - 1; i++)
            {
                if (inputArray[i] == null)
                {
                    inputArray[i] = newElement;
                    return;
                }
                if (float.IsNaN(inputArray[i].RaceTime) || inputArray[i].RaceTime > newElement.RaceTime)
                {
                    ShiftRight(ref inputArray, i);
                    inputArray[i] = newElement;
                    return;
                }
            }
        }

        public void ShiftRight(ref RaceSimulationResult[] inputArray, int fromIndex)
        {
            for (var i = inputArray.Length - 2; i >= fromIndex; i--)
            {
                inputArray[i + 1] = inputArray[i];
            }
        }
    }
}
