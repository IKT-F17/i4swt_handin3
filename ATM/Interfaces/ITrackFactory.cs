using System;

namespace ATM.Interfaces
{
    public interface ITrackFactory
    {
        ITrack SpawnTrack(string rawTrackData);
    }
}
