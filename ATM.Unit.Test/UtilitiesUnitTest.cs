using System;
using System.Collections.Generic;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM.Unit.Test
{
    [TestFixture()]
    public class UtilitiesUnitTest
    {
        private Track _UtilitiesTrack1;
        private Track _UtilitiesTrack2;

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
                XCoord = 15000,
                YCoord = 15000,
                XCoordOld = 16000,
                YCoordOld = 16000
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
        public void CalculateCorrectHeadingInThirdQuadrant()
        {
            int heading = 225;

            Utilities.CalculateHeading(_UtilitiesTrack2);
            Assert.AreEqual(_UtilitiesTrack2.Heading, heading);
        }

        [Test]
        public void CalculateCorrectVelocity()
        {
            int velocity = 1414;

            Utilities.CalculateVelocity(_UtilitiesTrack1);
            Assert.AreEqual(_UtilitiesTrack1.Velocity,velocity);
        }
    }
}