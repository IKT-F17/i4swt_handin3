using System;
using ATM.Interfaces;

namespace ATM
{
    public static class Utilities
    {
        

        public static void CalculateHeading(ITrack track)
        {
            var xDiff = track.XCoord - track.XCoordOld;
            var yDiff = track.YCoord - track.YCoordOld;

            var heading = 90.0d - Math.Atan2(yDiff, xDiff) * 180 / Math.PI;

            if (heading < 0.0d) heading += 360.0;

            track.Heading = (int)heading;
        }

        public static void CalculateVelocity(ITrack track)
        {
            var xDiff = Math.Abs(track.XCoord - track.XCoordOld);
            var yDiff = Math.Abs(track.YCoord - track.YCoordOld);

            var distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            var deltaTime = track.TimeStamp.Subtract(track.TimeStampOld).TotalSeconds;

            var velocity = distance / deltaTime;

            track.Velocity = (int)velocity;
        }
    }
}