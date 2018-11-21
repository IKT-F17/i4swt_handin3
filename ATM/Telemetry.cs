using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Interfaces;

namespace ATM
{
    public class Telemetry
    {
        private ITrackFactory trackFromAirspace;

        public Telemetry(ITrackFactory track)
        {
            trackFromAirspace = track;
            trackFromAirspace.OnTrackListDoneEvent += TrackFromAirspace_OnTrackListDoneEvent;
        }

        private void TrackFromAirspace_OnTrackListDoneEvent(object sender, Dictionary<string, ITrack> e)
        {

            foreach (var item in e.ToList())
            {
                Console.WriteLine($"TAG:{item.Value.Tag}, X:{item.Value.XCoord}, Y:{item.Value.YCoord}, TIME:{item.Value.TimeStamp}");
                //if (item.Value.XCoordOld == 0)
                //{
                    
                //    UpdateOldValues(item);
                //}
                //else
                //{
                //    Console.WriteLine($"Tag: {item.Value.Tag} ");
                //    Console.WriteLine($"XCoord: {item.Value.XCoord} ");
                //    Console.WriteLine($"XCoordOld: {item.Value.XCoordOld} ");
                //    Console.WriteLine($"Time: {item.Value.TimeStamp} ");
                //    CalculateHeading(item);
                //    //CalculateVelocity(item);
                //    UpdateOldValues(item);
                //    Console.WriteLine($"Tag: {item.Value.Tag} ");
                //    Console.WriteLine($"XCoord: {item.Value.XCoord} ");
                //    Console.WriteLine($"XCoordOld: {item.Value.XCoordOld} ");
                //    Console.WriteLine($"Time: {item.Value.TimeStamp} ");
                //}
            }
        }

        private void CalculateHeading(KeyValuePair<string, ITrack> item)
        {
            var xDiff = item.Value.XCoord - item.Value.XCoordOld;
            var yDiff = item.Value.YCoord - item.Value.YCoordOld;

            var heading = 90.0d - Math.Atan2(yDiff, xDiff) * 180 / Math.PI;

            if (heading < 0.0d) heading += 360.0;

            item.Value.Heading = (int) heading;
        }

        private void CalculateVelocity(KeyValuePair<string, ITrack> item)
        {
            var xDiff = Math.Abs(item.Value.XCoord - item.Value.XCoordOld);
            var yDiff = Math.Abs(item.Value.YCoord - item.Value.YCoordOld);
            var deltaTime = Math.Abs((item.Value.TimeStamp - item.Value.TimeStampOld).Seconds);

            var distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
            item.Value.Velocity = (int) distance / deltaTime;
        }

        private void UpdateOldValues(KeyValuePair<string, ITrack> item)
        {
            item.Value.XCoordOld = item.Value.XCoord;
            item.Value.YCoordOld = item.Value.YCoord;
            item.Value.AltitudeOld = item.Value.Altitude;
            item.Value.TimeStampOld = item.Value.TimeStamp;
        }
    }
}
