using Moq;
using NUnit.Framework;

namespace RaceCarSetup.Test
{
    [TestFixture]
    public class CarSetupProcessorTest
    {
        private CarSetupProcessor processor;

        private Mock<IRaceSimulator> raceSimulatorMock;

        private const float FirstResult = 1.0f;
        private const float SecondResult = 2.0f;
        private const float ThirdResult = 3.0f;
        private const float FourthResult = 4.0f;
        private const float FifthResult = float.NaN;

        [SetUp]
        public void SetUp()
        {

            raceSimulatorMock = new Mock<IRaceSimulator>(MockBehavior.Strict);

            processor = new CarSetupProcessor(GivenTracks(), raceSimulatorMock.Object);
        }

        [Test]
        public void CarSetupProcessorRanksCarConfigurationsInAscendingRaceTimeOrderForAGivenTrack()
        {

            GivenSimulationReturnsANotOrderedResultsSequence();

            var simulationResults = processor.RankConfigurationsForTrack("Track", GivenCarConfigurations());

            Assert.AreEqual(5, simulationResults.Length);
            Assert.AreEqual(FirstResult, simulationResults[0].RaceTime);
            Assert.AreEqual(SecondResult, simulationResults[1].RaceTime);
            Assert.AreEqual(ThirdResult, simulationResults[2].RaceTime);
            Assert.AreEqual(FourthResult, simulationResults[3].RaceTime);
            Assert.AreEqual(FifthResult, simulationResults[4].RaceTime);
        }

        private void GivenSimulationReturnsANotOrderedResultsSequence()
        {
            raceSimulatorMock.SetupSequence(c => c.SimulateRace(It.IsAny<Track>(), It.IsAny<TrackPerformance>()))
                .Returns(ThirdResult)
                .Returns(SecondResult)
                .Returns(FifthResult)
                .Returns(FirstResult)
                .Returns(FourthResult);
        }

        private CarConfiguration[] GivenCarConfigurations()
        {
            return new CarConfiguration[]
            {
                new CarConfiguration(new TrackPerformance[] {}),
                new CarConfiguration(new TrackPerformance[] {}),
                new CarConfiguration(new TrackPerformance[] {}),
                new CarConfiguration(new TrackPerformance[] {}),
                new CarConfiguration(new TrackPerformance[] {}),
            };
        }

        private Track[] GivenTracks()
        {
            return new[] { new Track() { TrackName = "Track" } };
        }
    }
}
