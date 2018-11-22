using System;
using ATM.Interfaces;

namespace ATM
{
    public class Track : ITrack
    {
        public string Tag { get; set; }

        public int XCoord { get; set; }
        public int XCoordOld { get; set; } = 0;

        public int YCoord { get; set; }
        public int YCoordOld { get; set; } = 0;

        public int Altitude { get; set; }
        public int AltitudeOld { get; set; } = 0;

        public DateTime TimeStamp { get; set; }
        public DateTime TimeStampOld { get; set; }

        public int Heading { get; set; } = 0;
        public int Velocity { get; set; } = 0;
    }
}