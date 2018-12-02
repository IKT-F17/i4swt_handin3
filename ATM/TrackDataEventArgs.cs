using ATM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class TrackEventArgs : EventArgs
    {
        public TrackEventArgs(ITrack track)
        {
            Track = track;
        }
        public ITrack Track { get; set; }
       }
}
