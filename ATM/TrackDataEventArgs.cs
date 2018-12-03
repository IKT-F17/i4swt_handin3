using ATM.Interfaces;
using System;

namespace ATM
{
    public class TrackEventArgs : EventArgs
    {
        public ITrack Track { get; }
        public TrackEventArgs(ITrack track)
        {
            Track = track;
        }
    }

    public class CollisionEventArgs : EventArgs
    {
        public ITrack Plane1 { get; }
        public ITrack Plane2 { get; }

        public CollisionEventArgs(ITrack plane1, ITrack plane2)
        {
            Plane1 = plane1;
            Plane2 = plane2;
        }
    }
}
