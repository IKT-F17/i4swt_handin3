using System;
using TransponderReceiver;

namespace ATM.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var track = new TrackFactory(TransponderReceiverFactory.CreateTransponderDataReceiver());
            new Airspace(track);

            Console.ReadKey();
        }
    }
}
