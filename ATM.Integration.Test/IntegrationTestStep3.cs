using System;
using System.Collections.Generic;
using System.Threading;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.Integration.Test
{
    public class IntegrationTestStep3
    {
        private ITransponderReceiver _trFakeData;
        private ITrackFactory _trackFactory;
        private IAirspace _airspace;
        private ISeparation _separation;
        
        private Track _insideAirspacePlane1;
        private Track _insideAirspacePlane2;
        private Track _insideAirspacePlane3;

        private Output _uut;

        [SetUp]
        public void Setup()
        {
            _trFakeData = Substitute.For<ITransponderReceiver>();
            _trackFactory = new TrackFactory(_trFakeData);
            _airspace = new Airspace(_trackFactory);
            _separation = new Separation(_airspace);

            _uut = new Output(_trackFactory, _airspace, _separation);

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
                Altitude = 8200,
                AltitudeOld = 8200,
                Heading = 0,
                Tag = "PIE286",
                TimeStamp = new DateTime(2018, 11, 1, 11, 11, 11, 0),
                TimeStampOld = new DateTime(2018, 11, 1, 11, 11, 10, 0),
                Velocity = 0,
                XCoord = 12000,
                YCoord = 12000,
                XCoordOld = 17000,
                YCoordOld = 17000
            };
        }

        [Test]
        public void SeparationEventOutput()
        {
            _separation.OnPlaneCollision += Raise.EventWith(this,
                new CollisionEventArgs(_insideAirspacePlane1, _insideAirspacePlane2));

            _separation.OnPlaneCollision += Raise.EventWith(this,
                new CollisionEventArgs(_insideAirspacePlane1, _insideAirspacePlane3));

            _separation.OnPlaneAvoidedCollision += Raise.EventWith(this,
                new CollisionEventArgs(_insideAirspacePlane1, _insideAirspacePlane2));

            _separation.OnPlaneAvoidedCollision += Raise.EventWith(this,
                new CollisionEventArgs(_insideAirspacePlane1, _insideAirspacePlane3));
        }

        [Test]
        public void AirspaceEventOutput()
        {
            _airspace.OnPlaneEnteringAirspace += Raise.EventWith(this, new TrackEventArgs(_insideAirspacePlane1));
            _airspace.OnPlaneExitingAirspace += Raise.EventWith(this, new TrackEventArgs(_insideAirspacePlane1));
        }

        [Test]
        public void TrackFactoryMetaDataOutput()
        {
            var tracks = new Dictionary<string, ITrack>()
            {
                {_insideAirspacePlane1.Tag, _insideAirspacePlane1},
                { _insideAirspacePlane2.Tag, _insideAirspacePlane2},
                {_insideAirspacePlane3.Tag, _insideAirspacePlane3}
            };

            _airspace.OnPlaneEnteringAirspace += Raise.EventWith(this, new TrackEventArgs(_insideAirspacePlane1));
            _airspace.OnPlaneExitingAirspace += Raise.EventWith(this, new TrackEventArgs(_insideAirspacePlane3));

            _trackFactory.OnTrackListDoneEvent += Raise.EventWith(this, new TrackDataEventArgs(tracks));
            Thread.Sleep(TimeSpan.FromSeconds(5));
            _trackFactory.OnTrackListDoneEvent += Raise.EventWith(this, new TrackDataEventArgs(tracks));
        }

        [Test]
        public void AirspaceTrackOutput()
        {
            var tracks = new Dictionary<string, ITrack>()
            {
                {_insideAirspacePlane1.Tag, _insideAirspacePlane1},
                { _insideAirspacePlane2.Tag, _insideAirspacePlane2},
                {_insideAirspacePlane3.Tag, _insideAirspacePlane3}
            };

            _separation.OnPlaneCollision += Raise.EventWith(this,
                new CollisionEventArgs(_insideAirspacePlane1, _insideAirspacePlane2));

            _airspace.OnAirspaceCheckEventDone += Raise.EventWith(this, new TrackDataEventArgs(tracks));

            _separation.OnPlaneAvoidedCollision += Raise.EventWith(this,
                new CollisionEventArgs(_insideAirspacePlane1, _insideAirspacePlane2));

            tracks.Remove(_insideAirspacePlane3.Tag);

            _airspace.OnAirspaceCheckEventDone += Raise.EventWith(this, new TrackDataEventArgs(tracks));
        }
    }
}