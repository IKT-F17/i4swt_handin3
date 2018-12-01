using System;
using System.Collections.Generic;
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
        private List<string> outsideAirspace = new List<string>();
        private List<string> insideAirspace = new List<string>();
        //private ITrackFactory trackFromTrackFactory;

        public event EventHandler<Dictionary<string, ITrack>> OnAirspaceCheckEventDone;


        public Airspace(ITrackFactory track)
        {
            //trackFromTrackFactory = track;
            //trackFromTrackFactory.OnTrackListDoneEvent += TrackFromTrackFactory_OnTrackListDoneEvent;
            
            track.OnTrackListDoneEvent += TrackFromTrackFactory_OnTrackListDoneEvent;
        }

        private void TrackFromTrackFactory_OnTrackListDoneEvent(object sender, Dictionary<string, ITrack> e)
        {
            foreach (var track in e)
            {
                if (IsInsideAirspace(track.Value))
                {
                    if (!insideAirspace.Contains(track.Key)) insideAirspace.Add(track.Key);
                    if (outsideAirspace.Contains(track.Key))
                    {
                        outsideAirspace.Remove(track.Key); // If true track just entered airspace.
                        Console.SetCursorPosition(0,4);
                        Console.Write($"{track.Key} - entered Airspace.");
                    }
                }
                else
                {
                    if (!outsideAirspace.Contains(track.Key)) outsideAirspace.Add(track.Key);
                    if (insideAirspace.Contains(track.Key))
                    {
                        insideAirspace.Remove(track.Key);   // If true track just left airspace.
                        Console.SetCursorPosition(0, 5);
                        Console.Write($"{track.Key} - left Airspace.");
                    }
                }

                // DEBUG:
                Console.SetCursorPosition(0,2);
                Console.Write($"Outside: {outsideAirspace.Count} Inside: {insideAirspace.Count}");

                //e.Remove(track.Key);

                //Console.WriteLine($"TAG:{track.Value.Tag}, X:{track.Value.XCoord}, Y:{track.Value.YCoord}, TIME:{track.Value.TimeStamp}");
            }

            //OnAirspaceCheckEventDone?.Invoke(this, e);
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