using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Integration.Test
{
    [TestFixture]
    class ATM_Integration_Test
    {


        // this is NOT for the real Integration test - i set this up to be able to get Jenkins up and running.
        //        |
        //       \ /
        //        V
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

        //        ^
        //       / \
        //        |

        // this is NOT for the real Integration test - i set this up to be able to get Jenkins up and running.






    }
}




