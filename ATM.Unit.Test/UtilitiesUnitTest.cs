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
        private Track _UtilitiesTrack;

        [SetUp]
        public void Setup()
        {
            _UtilitiesTrack = new Track()
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
        public void CalculateCorrectHeading()
        {
            int heading = 225;

            Utilities.CalculateHeading(_UtilitiesTrack);
            Assert.AreEqual(_UtilitiesTrack.Heading, heading);
        }

        [Test]
        public void CalculateCorrectVelocity()
        {
            int velocity = 1414;

            Utilities.CalculateVelocity(_UtilitiesTrack);
            Assert.AreEqual(_UtilitiesTrack.Velocity,velocity);
        }
    }
}