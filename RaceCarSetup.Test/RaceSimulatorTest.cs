using Moq;
using NUnit.Framework;

namespace RaceCarSetup.Test
{
    [TestFixture]
    public class RaceSimulatorTest
    {
        private const string ExpectedTrackName = "ExpectedTrackName";
        private Mock<CarConfiguration> carConfigurationMock;
        private RaceSimulator raceSimulator;

        [SetUp]
        public void SetUp()
        {
            carConfigurationMock = new Mock<CarConfiguration>(MockBehavior.Strict);
            raceSimulator = new RaceSimulator();
        }

        [Test]
        public void CarConfigurationCalculatesRaceTimeWhenTrackPerformanceExistsForGivenTrack()
        {
            Assert.AreEqual(5060.0f, raceSimulator.SimulateRace(GivenTrack(), TrackPerformanceForTrack()));
        }

        [Test]
        public void CarConfigurationReturnsNaNWhenTrackPerformanceDoesNotExistsForGivenTrack()
        {
            Assert.AreEqual(float.NaN, raceSimulator.SimulateRace(GivenTrack(), null));
        }

        [Test]
        public void CarConfigurationReturnsNaNWhenTrackPerformanceIsMalformedForGivenTrack()
        {
            Assert.AreEqual(float.NaN, raceSimulator.SimulateRace(GivenTrack(), MalformedTrackPerformance()));
        }

        [Test]
        public void CarConfigurationReturnsNaNWhenGivenTrackIsMalformed()
        {
            Assert.AreEqual(float.NaN, raceSimulator.SimulateRace(MalformedTrack(), TrackPerformanceForTrack()));
        }

        private TrackPerformance TrackPerformanceForTrack()
        {
            return new TrackPerformance
            {
                FuelCapacity = 100.0f,
                AvgConsumption = 1.6f,
                LapTime = 100.0f,
                TrackName = ExpectedTrackName
            };
        }

        private TrackPerformance MalformedTrackPerformance()
        {
            return new TrackPerformance { TrackName = ExpectedTrackName };
        }

        private Track GivenTrack()
        {
            return new Track() { TrackName = ExpectedTrackName, LapDistance = 4.0f, PitstopTime = 20.0f, RaceLenght = 50 };
        }

        private Track MalformedTrack()
        {
            return new Track() { TrackName = ExpectedTrackName };
        }
    }
}