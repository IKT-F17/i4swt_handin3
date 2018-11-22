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

        //public void UpdateTrack(ITrack track)
        //{
        //    // TODO: Heading/Course
        //    var xDiff = track.XCoord - this.XCoord;
        //    var yDiff = track.YCoord - this.YCoord;
        //    Heading = CalcHeading(xDiff, yDiff);

        //    Console.WriteLine($"Flight: {track.Tag}, have a heading of: {Heading} degrees.");

        //    // TODO: Speed/Velocity (m/s)
        //}
    }
}