using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace ATM.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var tracks = new Dictionary<string, ITrack>();
            ITrackFactory trackFactory = new TrackFactory();

            var controller = new ATM(receiver, trackFactory, tracks);

            Console.ReadKey();
        }
    }
}



