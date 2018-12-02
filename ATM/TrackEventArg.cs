using ATM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class TrackDataEventArgs : EventArgs
    {
        public TrackDataEventArgs(Dictionary<string, ITrack> trackData)
        {
            Trackdata = trackData;
        }
        public Dictionary<string,ITrack> Trackdata { get; set; }
       }
}
