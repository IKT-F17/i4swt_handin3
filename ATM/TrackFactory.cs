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
                // DEBUG RAW DATA:
                Console.SetCursorPosition(0,0);
                Console.Write($"Raw   data: {item}");

                var track = SpawnTrack(item);

                // DEBUG GENERATED TRACK DATA:
                Console.SetCursorPosition(0, 1);
                Console.Write($"Track data: {track.Tag};{track.XCoord};{track.YCoord};{track.Altitude};{track.TimeStamp}");

                // If a tag exist in our dictionary then update it:
                if (!globalTrackData.ContainsKey(track.Tag))
                {
                    globalTrackData.Add(track.Tag, track);
                }
                else
                {
                    globalTrackData[track.Tag] = track;
                }
            }

            OnTrackListDoneEvent?.Invoke(this, globalTrackData);
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