using System;
using System.Collections.Generic;

namespace ATM.Interfaces
{
    public interface ITrackFactory
    {
        ITrack SpawnTrack(string rawTrackData);
        event EventHandler<Dictionary<string, ITrack>> OnTrackListDoneEvent;
    }
}
