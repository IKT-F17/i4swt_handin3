using System;
using System.Collections.Generic;
using System.Globalization;
using ATM.Interfaces;
using TransponderReceiver;

namespace ATM
{
    public class TrackFactory : ITrackFactory
    {
        public event EventHandler<TrackDataEventArgs> OnTrackListDoneEvent;
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

            OnTrackListDoneEvent?.Invoke(this, new TrackDataEventArgs(globalTrackData));
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
            //CalculateHeading(track.Tag);
           // CalculateVelocity(track.Tag);
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
