using NUnit.Framework;

namespace RaceCarSetup.Test
{
    [TestFixture]
    public class TrackPerformanceRepositoryTest
    {
        private const string ExpectedTrackName = "ExpectedTrackName";
        private TrackPerformanceRepository repo;

        [SetUp]
        public void SetUp()
        {
            repo = new TrackPerformanceRepository(new[]
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
        public void TrackPerformanceRepositoryRetrievesTrackPerformanceWhenItExists()
        {
            Assert.AreEqual(ExpectedTrackName, repo.FindPerformanceForTrack(ExpectedTrackName).TrackName);
        }

        [Test]
        public void TrackPerformanceRepositoryReturnsNullWhenTrackPerformanceDoesNotExist()
        {
            Assert.IsNull(repo.FindPerformanceForTrack("NotSupportedTrackName"));
        }
    }
}