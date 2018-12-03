using System;
using NUnit.Framework;

namespace ATM.Unit.Test
{
    [TestFixture()]
    public class UtilitiesUnitTest
    {
        private Track _UtilitiesTrack1;
        private Track _UtilitiesTrack2;
        private Track _UtilitiesTrack3;
        private Track _UtilitiesTrack4;

        [SetUp]
        public void Setup()
        {
            _UtilitiesTrack1 = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 8000,
                Heading = 0,
                Tag = "CCC666",
                TimeStamp = new DateTime(2018, 11, 1, 11, 11, 11, 0),
                TimeStampOld = new DateTime(2018, 11, 1, 11, 11, 10, 0),
                Velocity = 0,
                XCoord = 16000,
                YCoord = 16000,
                XCoordOld = 15000,
                YCoordOld = 15000
            };

            _UtilitiesTrack2 = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 8000,
                Heading = 0,
                Tag = "CCC666",
                TimeStamp = new DateTime(2018, 11, 1, 11, 11, 11, 0),
                TimeStampOld = new DateTime(2018, 11, 1, 11, 11, 10, 0),
                Velocity = 0,
                XCoord = 16000,
                YCoord = 15000,
                XCoordOld = 15000,
                YCoordOld = 16000
            };

            _UtilitiesTrack3 = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 8000,
                Heading = 0,
                Tag = "CCC666",
                TimeStamp = new DateTime(2018, 11, 1, 11, 11, 11, 0),
                TimeStampOld = new DateTime(2018, 11, 1, 11, 11, 10, 0),
                Velocity = 0,
                XCoord = 15000,
                YCoord = 15000,
                XCoordOld = 16000,
                YCoordOld = 16000
            };

            _UtilitiesTrack4 = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 8000,
                Heading = 0,
                Tag = "CCC666",
                TimeStamp = new DateTime(2018, 11, 1, 11, 11, 11, 0),
                TimeStampOld = new DateTime(2018, 11, 1, 11, 11, 10, 0),
                Velocity = 0,
                XCoord = 15000,
                YCoord = 16000,
                XCoordOld = 16000,
                YCoordOld = 15000
            };
        }

        [Test]
        public void CalculateCorrectHeadingInFirstQuadrant()
        {
            int heading = 45;

            Utilities.CalculateHeading(_UtilitiesTrack1);
            Assert.AreEqual(_UtilitiesTrack1.Heading, heading);
        }

        [Test]
        public void CalculateCorrectHeadingInSecondQuadrant()
        {
            int heading = 135;

            Utilities.CalculateHeading(_UtilitiesTrack2);
            Assert.AreEqual(_UtilitiesTrack2.Heading, heading);
        }

        [Test]
        public void CalculateCorrectHeadingInThirdQuadrant()
        {
            int heading = 225;

            Utilities.CalculateHeading(_UtilitiesTrack3);
            Assert.AreEqual(_UtilitiesTrack3.Heading, heading);
        }

        [Test]
        public void CalculateCorrectHeadingInFourthQuadrant()
        {
            int heading = 315;

            Utilities.CalculateHeading(_UtilitiesTrack4);
            Assert.AreEqual(_UtilitiesTrack4.Heading, heading);
        }

        [Test]
        public void CalculateCorrectVelocity()
        {
            int velocity = 1414;

            Utilities.CalculateVelocity(_UtilitiesTrack1);
            Assert.AreEqual(_UtilitiesTrack1.Velocity, velocity);
        }
    }
}