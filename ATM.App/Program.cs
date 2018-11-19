﻿using System;
using System.Collections.Generic;
using TransponderReceiver;

namespace ATM.App
{
    class ATM_App
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
