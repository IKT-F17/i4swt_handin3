using System;
using System.Collections.Generic;
using System.Globalization;
using ATM.Interfaces;
using TransponderReceiver;

namespace ATM
{
    public class TrackFactory : ITrackFactory
    {
        private Dictionary<string, ITrack> globalTrackData;

        public TrackFactory(ITransponderReceiver receiver)
        {
            globalTrackData = new Dictionary<string, ITrack>();

            receiver.TransponderDataReady += Receiver_TransponderDataReady;
        }

        private void Receiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            //globalTrackData.Clear();

            var rawTrackData = e.TransponderData;

            foreach (var item in rawTrackData)
            {
                var track = SpawnTrack(item);

                if (!globalTrackData.ContainsKey(track.Tag))
                {
                    globalTrackData.Add(track.Tag, track);
                }
                else
                {
                    globalTrackData[track.Tag].UpdateTrack(track);
                }
            }
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