using System;
using System.Collections.Generic;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ATM.Unit.Test
{
    [TestFixture]
    public class SeparationTest
    {
        private IAirspace _fakeAirspace;
        private ISeparation _uut;
        private Track _insideAirspacePlane1;
        private Track _insideAirspacePlane2;
        private Track _insideAirspacePlane3;

        private int verticalSeparation = 300;
        private int horizontalSeparation = 5000;
        private bool FoundPlanesOnCollision = false;

        [SetUp]
        public void Setup()
        {
            _fakeAirspace = Substitute.For<IAirspace>();
            _uut = new Separation(_fakeAirspace);

            _insideAirspacePlane1 = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 8000,
                Heading = 0,
                Tag = "PIE284",
                TimeStamp = new DateTime(2018, 11, 1, 11, 11, 11, 0),
                TimeStampOld = new DateTime(2018, 11, 1, 11, 11, 10, 0),
                Velocity = 0,
                XCoord = 15000,
                YCoord = 15000,
                XCoordOld = 16000,
                YCoordOld = 16000
            };
            _insideAirspacePlane2 = new Track()
            {
                Altitude = 8200,
                AltitudeOld = 8200,
                Heading = 0,
                Tag = "PIE285",
                TimeStamp = new DateTime(2018, 11, 1, 11, 11, 11, 0),
                TimeStampOld = new DateTime(2018, 11, 1, 11, 11, 10, 0),
                Velocity = 0,
                XCoord = 18000,
                YCoord = 18000,
                XCoordOld = 17000,
                YCoordOld = 17000
            };
            _insideAirspacePlane3 = new Track()
            {
                Altitude = 15000,
                AltitudeOld = 15000,
                Heading = 0,
                Tag = "PIE286",
                TimeStamp = new DateTime(2018, 11, 1, 11, 11, 11, 0),
                TimeStampOld = new DateTime(2018, 11, 1, 11, 11, 10, 0),
                Velocity = 0,
                XCoord = 18000,
                YCoord = 18000,
                XCoordOld = 17000,
                YCoordOld = 17000
            };

            _uut.OnPlaneCollision += (s, e) =>
            {
                FoundPlanesOnCollision = true;

                if (e.Plane1.Tag == e.Plane2.Tag) Assert.Fail();

                if (_insideAirspacePlane1.Tag == e.Plane1.Tag
                    || _insideAirspacePlane1.Tag == e.Plane2.Tag
                    || _insideAirspacePlane2.Tag == e.Plane1.Tag
                    || _insideAirspacePlane2.Tag == e.Plane2.Tag)
                {
                    Assert.IsTrue(Math.Abs(e.Plane1.Altitude - e.Plane2.Altitude) <= verticalSeparation
                                  && Math.Abs(e.Plane1.XCoord - e.Plane2.XCoord) <= horizontalSeparation
                                  && Math.Abs(e.Plane1.YCoord - e.Plane2.YCoord) <= horizontalSeparation);
                }
                else
                {
                    Assert.Fail();
                }
            };
        }


        [Test]
        public void FindPlanesOnCollision_OnCollision()
        {
            var AirspaceTracks = new Dictionary<string, ITrack>();

            FoundPlanesOnCollision = false;

            AirspaceTracks.Add(_insideAirspacePlane1.Tag, _insideAirspacePlane1);
            AirspaceTracks.Add(_insideAirspacePlane2.Tag, _insideAirspacePlane2);
            AirspaceTracks.Add(_insideAirspacePlane3.Tag, _insideAirspacePlane3);

            _fakeAirspace.OnAirspaceCheckEventDone += Raise.EventWith(this, new TrackDataEventArgs(AirspaceTracks));

            Assert.IsTrue(FoundPlanesOnCollision);
        }

        [Test]
        public void FindPlanesOnCollision_NotOnCollision()
        {
            var AirspaceTracks = new Dictionary<string, ITrack>();

            FoundPlanesOnCollision = false;

            AirspaceTracks.Add(_insideAirspacePlane1.Tag, _insideAirspacePlane1);
            AirspaceTracks.Add(_insideAirspacePlane3.Tag, _insideAirspacePlane3);

            _fakeAirspace.OnAirspaceCheckEventDone += Raise.EventWith(this, new TrackDataEventArgs(AirspaceTracks));

            Assert.IsFalse(FoundPlanesOnCollision);
        }

        [Test]
        public void FindPlanesOnCollision_NoPlanesInAirspace()
        {
            var AirspaceTracks = new Dictionary<string, ITrack>();

            FoundPlanesOnCollision = false;

            _fakeAirspace.OnAirspaceCheckEventDone += Raise.EventWith(this, new TrackDataEventArgs(AirspaceTracks));

            Assert.IsFalse(FoundPlanesOnCollision);
        }
    }
}
