using NUnit.Framework;

namespace RaceCarSetup.Test
{
    [TestFixture]
    public class TrackRepositoryTest
    {
        private const string ExpectedTrackName = "ExpectedTrackName";
        private TrackRepository repo;

        [SetUp]
        public void SetUp()
        {
            repo = new TrackRepository(new[]
            {
                new Track() { TrackName = "NotExpected0" },
                new Track() { TrackName = "NotExpected0" },
                new Track() { TrackName = "NotExpected0" },
                new Track() { TrackName = ExpectedTrackName },
                new Track() { TrackName = "NotExpected0" },
                new Track() { TrackName = "NotExpected0" },
                new Track() { TrackName = "NotExpected0" }
            });
        }

        [Test]
        public void TrackRepositoryRetrievesTrackWhenItExists()
        {
            Assert.AreEqual(ExpectedTrackName, repo.FindTrack(ExpectedTrackName).TrackName);
        }

        [Test]
        public void TrackRepositoryReturnsNullWhenTrackPerformanceDoesNotExist()
        {
            Assert.IsNull(repo.FindTrack("NotSupportedTrackName"));
        }
    }
}