using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Interfaces;

namespace ATM
{
    public class Airspace
    {
        private int XMin = 10000;
        private int YMin = 10000;
        private int XMax = 90000;
        private int YMax = 90000;
        private int MinAltitude = 500;
        private int MaxAltitude = 20000;

        public event EventHandler<Dictionary<string, ITrack>> OnAirspaceCheckEventDone;
        private ITrackFactory trackFromTrackFactory;

        public Airspace(ITrackFactory track)
        {
            trackFromTrackFactory = track;
            trackFromTrackFactory.OnTrackListDoneEvent += TrackFromTrackFactory_OnTrackListDoneEvent;
        }

        private void TrackFromTrackFactory_OnTrackListDoneEvent(object sender, Dictionary<string, ITrack> e)
        {
            foreach (var item in e.ToList())
            {
                if (!IsInsideAirspace(item.Value))
                    e.Remove(item.Key);

                //Console.WriteLine($"TAG:{item.Value.Tag}, X:{item.Value.XCoord}, Y:{item.Value.YCoord}, TIME:{item.Value.TimeStamp}");
                Console.WriteLine($"AIRSPACE - TAG:{item.Value.Tag} X:{item.Value.XCoord} in e");
            }

            OnAirspaceCheckEventDone?.Invoke(this, e);
        }

        private bool IsInsideAirspace(ITrack track)
        {
            return track.XCoord >= XMin
                && track.XCoord <= XMax
                && track.YCoord >= YMin
                && track.YCoord <= YMax
                && track.Altitude >= MinAltitude
                && track.Altitude <= MaxAltitude;
        }
    }
}
