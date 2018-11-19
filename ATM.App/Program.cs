using System;
using TransponderReceiver;

namespace ATM.App
{
    class Program
    {
        static void Main(string[] args)
        {
            new TrackFactory(TransponderReceiverFactory.CreateTransponderDataReceiver());

            Console.ReadKey();
        }
    }
}
