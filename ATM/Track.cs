using System;
using ATM.Interfaces;

namespace ATM
{
    public class Track : ITrack
    {
        public Track()
        {
            XCoord = 0;
            YCoord = 0;
            Altitude = 0;
            TimeStamp = DateTime.MinValue;
            Heading = 0;
            Velocity = 0;
        }

        public string Tag { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }

        public int Heading { get; set; }
        public int Velocity { get; set; }

        public int XCoordOld { get; set; } = 0;
        public int YCoordOld { get; set; } = 0;
        public int AltitudeOld { get; set; } = 0;
        public DateTime TimeStampOld { get; set; } = DateTime.MinValue;
    }
}
