using System;
using NUnit.Framework;

namespace RaceCarSetup.Test
{
    [TestFixture]
    public class ArrayUtilsTest
    {
        private const string ExpectedTrackName = "X_Track";



        [Test]
        public void FindElementRetrievesArrayElementWhenItExists()
        {
            Assert.AreEqual(ExpectedTrackName, ArrayUtils.FindElement(GivenTracks(), (t) => t.TrackName.Equals(ExpectedTrackName)).TrackName);
        }

        [Test]
        public void FindElementReturnsNullWhenArrayElementDoesNotExist()
        {
            Assert.IsNull(ArrayUtils.FindElement(GivenTracks(), (t) => t.TrackName.Equals("NotSupportedTrackName")));
        }

        [Test]
        public void WhenArrayIsEmptyInsertSortedAddsElementInFirstPosition()
        {
            var newElement = new TrackPerformance();
            var trackPerformances = new TrackPerformance[5];
            ArrayUtils.InsertSorted(ref trackPerformances, newElement, (t) => false);

            Assert.AreEqual(newElement, trackPerformances[0]);
        }

        [Test]
        public void WhenArrayElementsAreSmallerInsertSortedAddsElementInFirstEmptyPosition()
        {
            var newElement = new TrackPerformance();
            var trackPerformances = new TrackPerformance[] { new TrackPerformance(), null, null, null, };
            ArrayUtils.InsertSorted(ref trackPerformances, newElement, (t) => false);

            Assert.AreEqual(newElement, trackPerformances[1]);
        }

        [Test]
        public void InsertSortedAddsElementInExpectedPosition()
        {
            var newElement = new Track(){TrackName = "D_Track"};
            var trackPerformances = GivenTracks();
            ArrayUtils.InsertSorted(ref trackPerformances, newElement, (t) => String.Compare(t.TrackName, newElement.TrackName, StringComparison.Ordinal)>0);

            Assert.AreEqual(newElement, trackPerformances[3]);
        }

        private static Track[] GivenTracks()
        {
            return new[]
            {
                new Track() { TrackName = "A_Track" },
                new Track() { TrackName = "B_Track" },
                new Track() { TrackName = "C_Track" },
                new Track() { TrackName = ExpectedTrackName },
                null
            };
        }
    }
}