using NUnit.Framework;

namespace RaceCarSetup.Test
{
    [TestFixture]
    public class CarConfigurationTest
    {
        private const string ExpectedTrackName = "ExpectedTrackName";
        private CarConfiguration repo;

        [SetUp]
        public void SetUp()
        {
            repo = new CarConfiguration(new[]
            {
                new TrackPerformance() { TrackName = "NotExpected0" },
                new TrackPerformance() { TrackName = "NotExpected0" },
                new TrackPerformance() { TrackName = "NotExpected0" },
                new TrackPerformance() { TrackName = ExpectedTrackName },
                new TrackPerformance() { TrackName = "NotExpected0" },
                new TrackPerformance() { TrackName = "NotExpected0" },
                new TrackPerformance() { TrackName = "NotExpected0" }
            });
        }

        [Test]
        public void CarConfigurationRetrievesTrackPerformanceWhenItExists()
        {
            Assert.AreEqual(ExpectedTrackName, repo.GetPerformanceForTrack(ExpectedTrackName).TrackName);
        }

        [Test]
        public void CarConfigurationReturnsNullWhenTrackPerformanceDoesNotExist()
        {
            Assert.IsNull(repo.GetPerformanceForTrack("NotSupportedTrackName"));
        }
    }
}