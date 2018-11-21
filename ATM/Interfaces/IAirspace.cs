using System;
using System.Collections.Generic;

namespace ATM.Interfaces
{
    public interface IAirspace
    {
        event EventHandler<Dictionary<string, ITrack>> OnAirspaceCheckEventDone;
    }
}
