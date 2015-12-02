namespace RaceCarSetup
{
    public class CarConfiguration : ICarConfiguration
    {
        private readonly ITrackPerformanceRepository trackPerformanceRepository;


        public CarConfiguration(TrackPerformance[] trackPerformances)
            : this(new TrackPerformanceRepository(trackPerformances))
        { }

        public CarConfiguration(ITrackPerformanceRepository trackPerformanceRepository)
        {
            this.trackPerformanceRepository = trackPerformanceRepository;
        }

        public float SimulateRace(Track track)
        {
            try
            {
                var trackPerformance = trackPerformanceRepository.FindPerformanceForTrack(track.TrackName);

                var requiredFuelForLap = track.LapDistance * trackPerformance.AvgConsumption;

                var requiredFuelForRace = track.RaceLenght * requiredFuelForLap;

                var requiredPitstopsPerRace = (int)(requiredFuelForRace / trackPerformance.FuelCapacity);

                var raceTime = (track.RaceLenght * trackPerformance.LapTime) + (track.PitstopTime * requiredPitstopsPerRace);

                return raceTime > 0 ? raceTime : float.NaN;
            }
            catch
            {
                return float.NaN;
            }
        }

    }
}
