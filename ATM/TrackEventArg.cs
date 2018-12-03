using ATM.Interfaces;
using System;
using System.Collections.Generic;

namespace ATM
{
    public class TrackDataEventArgs : EventArgs
    {
        public TrackDataEventArgs(Dictionary<string, ITrack> trackData)
        {
            TrackData = trackData;
        }
        public Dictionary<string,ITrack> TrackData { get; set; }
       }
}
