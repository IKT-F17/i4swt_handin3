using System;
using TransponderReceiver;

namespace ATM.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(200,60);

            var trackFactory = new TrackFactory(TransponderReceiverFactory.CreateTransponderDataReceiver());
            var airspace = new Airspace(trackFactory);
            var separation = new Separation(airspace);

            new Output(trackFactory, airspace,separation);

            Console.ReadKey();
        }
    }
}
