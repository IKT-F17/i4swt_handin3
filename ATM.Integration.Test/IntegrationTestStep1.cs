



using System;
using System.Collections.Generic;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.Integration.Test
{

    [TestFixture]
    public class IntegrationTestStep1
    {
        private ITransponderReceiver _trFakeData;
        private ITrackFactory _trackFactory;
        private IAirspace _uut;
        private ITrack _track;

        private Track _outsideAirspaceTrackY;
        private Track _insideAirspaceTrack;
        private Track _outsideAirspaceTrackAlt;
        private Track _outsideAirspaceTrackX;



        [SetUp]
        public void Setup()
        {
            _trFakeData = Substitute.For<ITransponderReceiver>();


            _trackFactory = new TrackFactory(_trFakeData);
            _uut = new Airspace(_trackFactory);

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





            //_fakeTrackFactory.OnTrackListDoneEvent += (o, args) => { testDictionary = args; };

        }

        //[Test]
        //public void ReceiveCorrectTrackFromTrackFactory()
        //{

        //}

        [Test]
        public void NoPlanesInAirspace()
        {
            _uut.OnPlaneEnteringAirspace += (s, e) => { Assert.Fail(); };

            _uut.OnPlaneExitingAirspace += (s, e) => { Assert.Fail(); };


            var testDictionary = new List<string>();
            testDictionary.Add(_outsideAirspaceTrackAlt.ToString());
            testDictionary.Add(_outsideAirspaceTrackX.ToString());
            testDictionary.Add(_outsideAirspaceTrackY.ToString());

            _trFakeData.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testDictionary));
        }

        [Test]
        public void PlaneEntersAirspace()
        {
            var testDictionary = new List<string>();
            testDictionary.Add(_outsideAirspaceTrackAlt.ToString());
            testDictionary.Add(_outsideAirspaceTrackX.ToString());
            testDictionary.Add(_outsideAirspaceTrackY.ToString());
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

            _trFakeData.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testDictionary));


            testDictionary[1] = outsideAirspaceTrackXEntered.ToString();
            InAirspace = true;



            _trFakeData.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testDictionary));


            Assert.IsTrue(FoundInAirspace);
        }

        [Test]
        public void PlaneLeavesAirspace()
        {
            var testDictionary = new List<string>();
            testDictionary.Add(_outsideAirspaceTrackAlt.ToString());
            testDictionary.Add(_outsideAirspaceTrackX.ToString());
            testDictionary.Add(_outsideAirspaceTrackY.ToString());
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
            _uut.OnPlaneExitingAirspace += (s, e) =>
            {
                if (LeavesAirspace)
                {
                    NotInAirspace = true;
                    Assert.IsTrue(e.Track.Tag == outsideAirspaceTrackXEntered.Tag);
                }
                else Assert.Fail();
            };

            _trFakeData.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testDictionary));
            testDictionary[1] = outsideAirspaceTrackXEntered.ToString();
            InAirspace = true;

            _trFakeData.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testDictionary));
            testDictionary[1] = _outsideAirspaceTrackX.ToString();
            LeavesAirspace = true;
            _trFakeData.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testDictionary));
            Assert.IsTrue(NotInAirspace);
        }

        [Test]
        public void UnknownPlaneEntersAirspace()
        {
            var testDictionary = new List<string>();
            testDictionary.Add(_outsideAirspaceTrackAlt.ToString());
            testDictionary.Add(_outsideAirspaceTrackX.ToString());
            testDictionary.Add(_outsideAirspaceTrackY.ToString());
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


            testDictionary.Add(outsideAirspaceTrackXEntered.ToString());
            InAirspace = true;



            _trFakeData.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testDictionary));


            Assert.IsTrue(FoundInAirspace);
        }


    }
}