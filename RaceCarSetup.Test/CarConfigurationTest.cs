using Moq;
using NUnit.Framework;

namespace RaceCarSetup.Test
{
    [TestFixture]
    public class CarConfigurationTest
    {
        private const string ExpectedTrackName = "ExpectedTrackName";
        private Mock<ITrackPerformanceRepository> mockRepository;
        private CarConfiguration carConfiguration;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new Mock<ITrackPerformanceRepository>(MockBehavior.Strict);
            carConfiguration = new CarConfiguration(mockRepository.Object);
        }

        [Test]
        public void CarConfigurationCalculatesRaceTimeWhenTrackPerformanceExistsForGivenTrack()
        {
            GivenRepositoryReturns(TrackPerformanceForTrack());
            Assert.AreEqual(5060.0f, carConfiguration.SimulateRace(GivenTrack()));
        }

        [Test]
        public void CarConfigurationReturnsNaNWhenTrackPerformanceDoesNotExistsForGivenTrack()
        {
            GivenRepositoryReturns(null);
            Assert.AreEqual(float.NaN, carConfiguration.SimulateRace(GivenTrack()));
        }

        [Test]
        public void CarConfigurationReturnsNaNWhenTrackPerformanceIsMalformedForGivenTrack()
        {
            GivenRepositoryReturns(MalformedTrackPerformance());
            Assert.AreEqual(float.NaN, carConfiguration.SimulateRace(GivenTrack()));
        }

        [Test]
        public void CarConfigurationReturnsNaNWhenGivenTrackIsMalformed()
        {
            GivenRepositoryReturns(TrackPerformanceForTrack());
            Assert.AreEqual(float.NaN, carConfiguration.SimulateRace(MalformedTrack()));
        }

        private void GivenRepositoryReturns(TrackPerformance trackPerformance)
        {
            mockRepository.Setup(r => r.FindPerformanceForTrack(It.Is<string>(s => s.Equals(ExpectedTrackName))))
                .Returns(trackPerformance);
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