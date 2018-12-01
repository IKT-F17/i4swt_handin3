using System;
using System.Collections.Generic;
using System.Globalization;
using ATM.Interfaces;
using TransponderReceiver;

namespace ATM
{
    public class TrackFactory : ITrackFactory
    {
        public event EventHandler<Dictionary<string, ITrack>> OnTrackListDoneEvent;
        private Dictionary<string, ITrack> globalTrackData;

        public TrackFactory(ITransponderReceiver receiver)
        {
            globalTrackData = new Dictionary<string, ITrack>();

            receiver.TransponderDataReady += Receiver_TransponderDataReady;
        }

        private void Receiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var item in e.TransponderData)
            {
                // DEBUG PRINT RAW DATA:
                //Console.SetCursorPosition(0,0);
                //Console.Write($"Raw   data: {item}");

                var track = SpawnTrack(item);

                // If a tag exist in our dictionary then update it:
                if (!globalTrackData.ContainsKey(track.Tag))
                {
                    globalTrackData.Add(track.Tag, track);
                }
                else
                {
                    UpdateTrack(track);
                }               
            }

            OnTrackListDoneEvent?.Invoke(this, globalTrackData);
        }

        private void UpdateTrack(ITrack track)
        {
            // Keeps old data:
            globalTrackData[track.Tag].XCoordOld = globalTrackData[track.Tag].XCoord;
            globalTrackData[track.Tag].YCoordOld = globalTrackData[track.Tag].YCoord;
            globalTrackData[track.Tag].AltitudeOld = globalTrackData[track.Tag].Altitude;
            globalTrackData[track.Tag].TimeStampOld = globalTrackData[track.Tag].TimeStamp;

            // Updates with new data:
            globalTrackData[track.Tag].XCoord = track.XCoord;
            globalTrackData[track.Tag].YCoord = track.YCoord;
            globalTrackData[track.Tag].Altitude = track.Altitude;
            globalTrackData[track.Tag].TimeStamp = track.TimeStamp;

            // Updating heading and velocity:
            CalculateHeading(track.Tag);
            CalculateVelocity(track.Tag);
        }

        private void CalculateHeading(string tag)
        {
            var xDiff = globalTrackData[tag].XCoord - globalTrackData[tag].XCoordOld;
            var yDiff = globalTrackData[tag].YCoord - globalTrackData[tag].YCoordOld;

            var heading = 90.0d - Math.Atan2(yDiff, xDiff) * 180 / Math.PI;

            if (heading < 0.0d) heading += 360.0;

            globalTrackData[tag].Heading = (int)heading;
        }

        private void CalculateVelocity(string tag)
        {
            var xDiff = Math.Abs(globalTrackData[tag].XCoord - globalTrackData[tag].XCoordOld);
            var yDiff = Math.Abs(globalTrackData[tag].YCoord - globalTrackData[tag].YCoordOld);
            var distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            var deltaTime = globalTrackData[tag].TimeStamp.Subtract(globalTrackData[tag].TimeStampOld).TotalSeconds;

            var velocity = distance / deltaTime;

            globalTrackData[tag].Velocity = (int) velocity;
        }

        public ITrack SpawnTrack(string rawTrackData)
        {
            string[] rawDataSplit = rawTrackData.Split(';');

            var track = new Track
            {
                Tag = rawDataSplit[0],
                XCoord = Convert.ToInt32(rawDataSplit[1]),
                YCoord = Convert.ToInt32(rawDataSplit[2]),
                Altitude = Convert.ToInt32(rawDataSplit[3]),
                TimeStamp = DateTime.ParseExact(rawDataSplit[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)
            };

            return track;
        }
    }
}
