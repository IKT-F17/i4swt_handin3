using System;
using TransponderReceiver;

namespace ATM.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            var track = new TrackFactory(TransponderReceiverFactory.CreateTransponderDataReceiver());
            new Airspace(track);

            Console.ReadKey();
        }
    }
}
