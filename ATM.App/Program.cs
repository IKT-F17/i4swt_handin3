using System;
using TransponderReceiver;

namespace ATM.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            var _track = new TrackFactory(TransponderReceiverFactory.CreateTransponderDataReceiver());
            var _airspace = new Airspace(_track);
            new Separation(_airspace);

            Console.ReadKey();
        }
    }
}
