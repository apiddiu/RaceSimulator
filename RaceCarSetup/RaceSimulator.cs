namespace RaceCarSetup
{
    public class RaceSimulator : IRaceSimulator
    {
        public float SimulateRace(Track track, TrackPerformance trackPerformance)
        {
            try
            {
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