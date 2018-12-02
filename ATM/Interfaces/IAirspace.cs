using System;
using System.Collections.Generic;

namespace ATM.Interfaces
{
    public interface IAirspace
    {
        event EventHandler<TrackDataEventArgs> OnAirspaceCheckEventDone;
        event EventHandler<TrackEventArgs> OnPlaneEnteringAirspace;
        event EventHandler<TrackEventArgs> OnPlaneExitingAirspace;
    }
}
