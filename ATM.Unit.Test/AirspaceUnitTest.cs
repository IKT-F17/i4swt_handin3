using System;
using System.Collections.Generic;
using System.Diagnostics;
using ATM.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace ATM.Unit.Test
{
    [TestFixture]
    public class AirspaceUnitTest
    {
        private ITrackFactory _fakeTrackFactory;
        private IAirspace _uut;
        //private ITrack _track;

        private Track _outsideAirspaceTrackY;
        private Track _insideAirspaceTrack;
        private Track _outsideAirspaceTrackAlt;
        private Track _outsideAirspaceTrackX;

        [SetUp]
        public void Setup()
        {
            _fakeTrackFactory = Substitute.For<ITrackFactory>();
            _uut = new Airspace(_fakeTrackFactory);

            _insideAirspaceTrack = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "PIE284",
                TimeStamp = DateTime.Now,//DateTime.Parse("20170914T225015100Z"),
                TimeStampOld = DateTime.Now,//DateTime.Parse("20170914T225015100Z"),
                Velocity = 0,
                XCoord = 15000,
                YCoord = 25000,
                XCoordOld = 0,
                YCoordOld = 0
            };

            _outsideAirspaceTrackAlt = new Track()
            {
                Altitude = 10,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "AAA666",
                TimeStamp = DateTime.Now,//DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Now,//DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 15000,
                YCoord = 25000,
                XCoordOld = 0,
                YCoordOld = 0
            };

            _outsideAirspaceTrackX = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "BBB666",
                TimeStamp = DateTime.Now,//DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Now,//DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 150000,
                YCoord = 25000,
                XCoordOld = 0,
                YCoordOld = 0
            };

            _outsideAirspaceTrackY = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "CCC666",
                TimeStamp = DateTime.Now,//DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Now,//DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 15000,
                YCoord = 250000,
                XCoordOld = 0,
                YCoordOld = 0
            };
        }

        [Test]
        public void NoPlanesInAirspace()
        {
            _uut.OnPlaneEnteringAirspace += (s, e) => { Assert.Fail(); };

            _uut.OnPlaneExitingAirspace += (s, e) => { Assert.Fail(); };
            
            var testDictionary = new Dictionary<string, ITrack>();
            testDictionary.Add(_outsideAirspaceTrackAlt.Tag, _outsideAirspaceTrackAlt);
            testDictionary.Add(_outsideAirspaceTrackX.Tag, _outsideAirspaceTrackX);
            testDictionary.Add(_outsideAirspaceTrackY.Tag, _outsideAirspaceTrackY);

            _fakeTrackFactory.OnTrackListDoneEvent += Raise.EventWith(this, new TrackDataEventArgs(testDictionary));
        }

        [Test]
        public void PlaneEntersAirspace()
        {
            var testDictionary = new Dictionary<string, ITrack>();
            testDictionary.Add(_outsideAirspaceTrackAlt.Tag, _outsideAirspaceTrackAlt);
            testDictionary.Add(_outsideAirspaceTrackX.Tag, _outsideAirspaceTrackX);
            testDictionary.Add(_outsideAirspaceTrackY.Tag, _outsideAirspaceTrackY);
            var outsideAirspaceTrackXEntered = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "BBB666",
                TimeStamp = DateTime.Now,//DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Now,//DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 75000,
                YCoord = 25000,
                XCoordOld = 0,
                YCoordOld = 0
            };
            bool InAirspace = false;
            bool FoundInAirspace = false;
            _uut.OnPlaneEnteringAirspace += (s, e) =>
            {
                if (InAirspace)
                {
                    FoundInAirspace = true;
                    Assert.IsTrue(e.Track.Tag == outsideAirspaceTrackXEntered.Tag);
                }
                else Assert.Fail();
            };
            _uut.OnPlaneExitingAirspace += (s, e) => { Assert.Fail(); };

            _fakeTrackFactory.OnTrackListDoneEvent += Raise.EventWith(this, new TrackDataEventArgs(testDictionary));


            testDictionary[_outsideAirspaceTrackX.Tag] = outsideAirspaceTrackXEntered;
            InAirspace = true;

            _fakeTrackFactory.OnTrackListDoneEvent += Raise.EventWith(this, new TrackDataEventArgs(testDictionary));
            
            Assert.IsTrue(FoundInAirspace);
        }

        [Test]
        public void PlaneLeavesAirspace()
        {
            var testDictionary = new Dictionary<string, ITrack>();
            testDictionary.Add(_outsideAirspaceTrackAlt.Tag, _outsideAirspaceTrackAlt);
            testDictionary.Add(_outsideAirspaceTrackX.Tag, _outsideAirspaceTrackX);
            testDictionary.Add(_outsideAirspaceTrackY.Tag, _outsideAirspaceTrackY);
            var outsideAirspaceTrackXEntered = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "BBB666",
                TimeStamp = DateTime.Now,//DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Now,//DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 75000,
                YCoord = 25000,
                XCoordOld = 0,
                YCoordOld = 0
            };
            bool LeavesAirspace = false;
            bool NotInAirspace = false;

            bool InAirspace = false;
            //bool FoundInAirspace = false;
            _uut.OnPlaneEnteringAirspace += (s, e) =>
            {
                if (InAirspace)
                {
                    //FoundInAirspace = true;
                    Assert.IsTrue(e.Track.Tag == outsideAirspaceTrackXEntered.Tag);
                }
                else Assert.Fail();
            };
            _uut.OnPlaneExitingAirspace += (s, e) =>
            {
                if (LeavesAirspace)
                {
                    NotInAirspace = true;
                    Assert.IsTrue(e.Track.Tag == outsideAirspaceTrackXEntered.Tag);
                }
                else Assert.Fail();
            };

            _fakeTrackFactory.OnTrackListDoneEvent += Raise.EventWith(this, new TrackDataEventArgs(testDictionary));

            testDictionary[_outsideAirspaceTrackX.Tag] = outsideAirspaceTrackXEntered;
            InAirspace = true;

            _fakeTrackFactory.OnTrackListDoneEvent += Raise.EventWith(this, new TrackDataEventArgs(testDictionary));

            testDictionary[_outsideAirspaceTrackX.Tag] = _outsideAirspaceTrackX;
            LeavesAirspace = true;
            _fakeTrackFactory.OnTrackListDoneEvent += Raise.EventWith(this, new TrackDataEventArgs(testDictionary));

            Assert.IsTrue(NotInAirspace);
        }

        [Test]
        public void UnknownPlaneEntersAirspace()
        {
            var testDictionary = new Dictionary<string, ITrack>();
            testDictionary.Add(_outsideAirspaceTrackAlt.Tag, _outsideAirspaceTrackAlt);
            testDictionary.Add(_outsideAirspaceTrackX.Tag, _outsideAirspaceTrackX);
            testDictionary.Add(_outsideAirspaceTrackY.Tag, _outsideAirspaceTrackY);
            var outsideAirspaceTrackXEntered = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "ZZZ666",
                TimeStamp = DateTime.Now,//DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Now,//DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 75000,
                YCoord = 25000,
                XCoordOld = 0,
                YCoordOld = 0
            };
            bool InAirspace = false;
            bool FoundInAirspace = false;
            _uut.OnPlaneEnteringAirspace += (s, e) =>
            {
                if (InAirspace)
                {
                    FoundInAirspace = true;
                    Assert.IsTrue(e.Track.Tag == outsideAirspaceTrackXEntered.Tag);
                }
                else Assert.Fail();
            };
            _uut.OnPlaneExitingAirspace += (s, e) => { Assert.Fail(); };
            
            testDictionary.Add(outsideAirspaceTrackXEntered.Tag, outsideAirspaceTrackXEntered);
            InAirspace = true;
            
            _fakeTrackFactory.OnTrackListDoneEvent += Raise.EventWith(this, new TrackDataEventArgs(testDictionary));


            Assert.IsTrue(FoundInAirspace);
        }
    }
}