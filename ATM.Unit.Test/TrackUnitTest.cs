using System;
using NUnit.Framework;

namespace ATM.Unit.Test
{
    [TestFixture]
    public class TrackUnitTest
    {
        private DateTime testTime = new DateTime();
        private Track _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Track()
            {
                Tag = "PIE284",
                XCoord = 5000,
                XCoordOld = 4000,
                YCoord = 5000,
                YCoordOld = 4000,
                Altitude = 1000,
                AltitudeOld = 1500,
                Heading = 42,
                Velocity = 200,
                TimeStamp = testTime,
                TimeStampOld = testTime
            };
        }

        [Test]
        public void TestTrackGetNSet()
        {
            Assert.That(_uut.Tag, Is.EqualTo("PIE284"));
            Assert.That(_uut.XCoord, Is.EqualTo(5000));
            Assert.That(_uut.XCoordOld,Is.EqualTo(4000));
            Assert.That(_uut.YCoord,Is.EqualTo(5000));
            Assert.That(_uut.YCoordOld,Is.EqualTo(4000));
            Assert.That(_uut.Altitude,Is.EqualTo(1000));
            Assert.That(_uut.AltitudeOld, Is.EqualTo(1500));
            Assert.That(_uut.Heading,Is.EqualTo(42));
            Assert.That(_uut.Velocity,Is.EqualTo(200));
            Assert.That(_uut.TimeStamp,Is.EqualTo(testTime));
            Assert.That(_uut.TimeStampOld,Is.EqualTo(testTime));
        }
    }
}
