using System;
using ATM.Interfaces;

namespace ATM
{
    class Track : ITrack
    {
        public string Tag { get; set; }

        public int XCoord { get; set; }
        public int XCoordDelta { get; set; } = 0;

        public int YCoord { get; set; }
        public int YCoordDelta { get; set; } = 0;

        public int Altitude { get; set; }
        public int AltitudeDelta { get; set; } = 0;

        public DateTime TimeStamp { get; set; }
        public int TimeStampDelta { get; set; } = 0;

        public int Heading { get; set; } = 0;
        public int Velocity { get; set; } = 0;

        public void UpdateTrack(ITrack track)
        {
            // TODO: Heading/Course
            var xDiff = track.XCoord - this.XCoord;
            var yDiff = track.YCoord - this.YCoord;
            Heading = CalcHeading(xDiff, yDiff);

            Console.WriteLine($"Flight: {track.Tag}, have a heading of: {Heading} degrees.");

            // TODO: Speed/Velocity (m/s)
        }

        private static int CalcHeading(double xDiff, double yDiff)
        {
            var heading = 90.0d - Math.Atan2(yDiff, xDiff) * 180 / Math.PI;

            if (heading < 0.0d) heading += 360.0;

            return (int)heading;
        }

    }
}