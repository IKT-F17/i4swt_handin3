﻿using System;
using System.Collections.Generic;
using ATM.Interfaces;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;

namespace ATM.Unit.Test
{
    [TestFixture]
    public class ATM_Unit_Test
    {
        private ITransponderReceiver _fakeData;
        private TrackFactory _uut;

        [SetUp]
        public void Setup()
        {
            // Used in ReceiveCorrectDataFromTransponderReceiverDll:
            _fakeData = Substitute.For<ITransponderReceiver>();

            // Used in SpawnTrackFromTrackFactory:
            _uut = new TrackFactory(_fakeData);
        }
         
        [Test]
        public void ReceiveCorrectDataFromTransponderReceiverDll()
        {
            var testDataList = new List<string>
            {
                "PIE284;29388;49932;2000;20151006213456789"
            };

            _fakeData.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testDataList));
        }

        [Test]
        public void SpawnTrackFromTrackFactory()
        {
            ITrack testTrack = _uut.SpawnTrack("PIE284;29388;49932;2000;20151006213456789");

            Assert.That(testTrack.Tag, Is.EqualTo("PIE284"));
            Assert.That(testTrack.XCoord, Is.EqualTo(29388));
            Assert.That(testTrack.YCoord, Is.EqualTo(49932));
            Assert.That(testTrack.Altitude, Is.EqualTo(2000));

            var tesTime = new DateTime(2015, 10, 06, 21, 34, 56, 789);
            Assert.That(testTrack.TimeStamp, Is.EqualTo(tesTime));
        }

        [Test]
        public void SpawnTrackFromTrackFactoryWrongString()
        {
            ITrack testTrack = _uut.SpawnTrack("QIE284;29388;49932;2000;20151006213456789");

            Assert.AreNotEqual(testTrack.Tag, Is.EqualTo("PIE284"));
            Assert.That(testTrack.XCoord, Is.EqualTo(29388));
            Assert.That(testTrack.YCoord, Is.EqualTo(49932));
            Assert.That(testTrack.Altitude, Is.EqualTo(2000));

            var tesTime = new DateTime(2015, 10, 06, 21, 34, 56, 789);
            Assert.That(testTrack.TimeStamp, Is.EqualTo(tesTime));
        }
    }
}
