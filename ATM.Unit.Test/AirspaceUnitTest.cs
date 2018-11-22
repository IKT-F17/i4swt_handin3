using System;
using System.Collections.Generic;
using ATM.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace ATM.Unit.Test
{
    [TestFixture]
    public class AirspaceUnitTest
    {
        private ITrackFactory _fakeTrackFactory;
        private Airspace _uut;
        private ITrack _track;

        private Dictionary<string, ITrack> testDictionary;

        [SetUp]
        public void Setup()
        {
            _fakeTrackFactory = Substitute.For<ITrackFactory>();
            _uut = new Airspace(_fakeTrackFactory);

            ITrack insideAirspaceTrack = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "PIE284",
                TimeStamp = DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 15000,
                YCoord = 25000,
                XCoordOld = 0,
                YCoordOld = 0
            };

            ITrack outsideAirspaceTrackAlt = new Track()
            {
                Altitude = 10,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "AAA666",
                TimeStamp = DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 15000,
                YCoord = 25000,
                XCoordOld = 0,
                YCoordOld = 0
            };

            ITrack outsideAirspaceTrackX = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "BBB666",
                TimeStamp = DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 150000,
                YCoord = 25000,
                XCoordOld = 0,
                YCoordOld = 0
            };

            ITrack outsideAirspaceTrackY = new Track()
            {
                Altitude = 8000,
                AltitudeOld = 0,
                Heading = 0,
                Tag = "CCC666",
                TimeStamp = DateTime.Parse("20170914225015100"),
                TimeStampOld = DateTime.Parse("20170914225015100"),
                Velocity = 0,
                XCoord = 15000,
                YCoord = 250000,
                XCoordOld = 0,
                YCoordOld = 0
            };

            testDictionary = new Dictionary<string, ITrack>();

            testDictionary.Add(insideAirspaceTrack.Tag, insideAirspaceTrack);
            testDictionary.Add(outsideAirspaceTrackAlt.Tag, outsideAirspaceTrackAlt);
            testDictionary.Add(outsideAirspaceTrackX.Tag, outsideAirspaceTrackX);
            testDictionary.Add(outsideAirspaceTrackY.Tag, outsideAirspaceTrackY);

            //_fakeTrackFactory.OnTrackListDoneEvent += (o, args) => { testDictionary = args; };
        }

        [Test]
        public void ReceiveCorrectTrackFromTrackFactory()
        {
            
        }
    }
}
