using System;
using System.Collections.Generic;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.Unit.Test
{
    [TestFixture]
    public class TrackFactoryUnitTest
    {
        private ITransponderReceiver _trFakeData;
        private TrackFactory _uut;

        
        [SetUp]
        public void Setup()
        {
            // Used in ReceiveCorrectDataFromTransponderReceiverDll:
            _trFakeData = Substitute.For<ITransponderReceiver>();

            // Used in SpawnTrackFromTrackFactory:
            _uut = new TrackFactory(_trFakeData);
        }

        [Test]
        public void ReceiveCorrectDataFromTransponderReceiverDll()
        {
            var testDataList = new List<string>
            {
                "PIE284;29388;49932;2000;20151006213456789",
                "LIR511;46000;6800;8000;20171122211255510"
            };

            _trFakeData.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(testDataList));
        }

    }
}
