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

        public event EventHandler<Dictionary<string, ITrack>> OnAirspaceCheckEventDone;

        public Airspace(ITrackFactory track)
        {
            track.OnTrackListDoneEvent += Track_OnTrackListDoneEvent;
        }

        private void Track_OnTrackListDoneEvent(object sender, Dictionary<string, ITrack> e)
        {
            foreach (var track in e)
            {
                if (IsInsideAirspace(track.Value))
                {
                    if (!insideAirspace.Contains(track.Key)) insideAirspace.Add(track.Key);
                    if (outsideAirspace.Contains(track.Key))
                    {
                        outsideAirspace.Remove(track.Key); // If true track just entered airspace.
                        // TODO: Raise “TrackEnteredAirspace”-event for 5 seconds. Include rendition of the track and the time.
                        Console.SetCursorPosition(0, 9);
                        Console.Write($"{track.Key} - entered Airspace.");
                    }
                }
                else
                {
                    if (!outsideAirspace.Contains(track.Key)) outsideAirspace.Add(track.Key);
                    if (insideAirspace.Contains(track.Key))
                    {
                        insideAirspace.Remove(track.Key);   // If true track just left airspace.
                        // TODO: Raise "Track Left Airspace”-event for 5 seconds. Include rendition of the track and the time.
                        Console.SetCursorPosition(0, 10);
                        Console.Write($"{track.Key} - left Airspace.");
                    }
                }

                // DEBUG:
                Console.SetCursorPosition(0,8);
                Console.Write($"Outside: {outsideAirspace.Count} Inside: {insideAirspace.Count}");
            }
            
            // TODO: Remove all tracks, from the global track dictionary, which are outside the airspace.


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