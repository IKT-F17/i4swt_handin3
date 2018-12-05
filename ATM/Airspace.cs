using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ATM.Interfaces;


namespace ATM
{
    public class Airspace : IAirspace
    {
        private int XMin = 10000;
        private int YMin = 10000;
        private int XMax = 90000;
        private int YMax = 90000;
        private int MinAltitude = 500;
        private int MaxAltitude = 20000;
        private List<string> outsideAirspace = new List<string>();
        private List<string> insideAirspace = new List<string>();

        public event EventHandler<TrackDataEventArgs> OnAirspaceCheckEventDone;
        public event EventHandler<TrackEventArgs> OnPlaneEnteringAirspace;
        public event EventHandler<TrackEventArgs> OnPlaneExitingAirspace;

        public Airspace(ITrackFactory track)
        {
            track.OnTrackListDoneEvent += Track_OnTrackListDoneEvent;
        }

        private void Track_OnTrackListDoneEvent(object sender, TrackDataEventArgs e)
        {
            foreach (var track in e.TrackData)
            {
                var _countx = e.TrackData.Count; 
                


                if (IsInsideAirspace(track.Value))
                {
                    if (!insideAirspace.Contains(track.Key))
                    {
                        insideAirspace.Add(track.Key);
                        OnPlaneEnteringAirspace?.Invoke(this, new TrackEventArgs(track.Value));
                    }
                    if (outsideAirspace.Contains(track.Key)) outsideAirspace.Remove(track.Key); // If true track just entered airspace.
                    Utilities.CalculateHeading(track.Value);
                    Utilities.CalculateVelocity(track.Value);
                   
                    // TODO: Raise “TrackEnteredAirspace”-event for 5 seconds. Include rendition of the track and the time.
                    //Console.SetCursorPosition(0, 9);
                    //Console.Write($"{track.Key} - entered Airspace.",CultureInfo.CurrentCulture);
                }
                else
                {
                    if (!outsideAirspace.Contains(track.Key)) outsideAirspace.Add(track.Key);
                    if (insideAirspace.Contains(track.Key))
                    {
                        insideAirspace.Remove(track.Key);   // If true track just left airspace.

                        OnPlaneExitingAirspace?.Invoke(this, new TrackEventArgs(track.Value));

                        // TODO: Raise "Track Left Airspace”-event for 5 seconds. Include rendition of the track and the time.
                        //Console.SetCursorPosition(0, 10);
                        //Console.Write($"{track.Key} - left Airspace.",CultureInfo.CurrentCulture);
                    }
                }
            }

            // TODO: Remove all tracks, from the global track dictionary, which are outside the airspace.

            var AirspaceTracks = e.TrackData.Where(x => insideAirspace.Contains(x.Key)).ToDictionary(x => x.Key, v => v.Value);
            OnAirspaceCheckEventDone?.Invoke(this, new TrackDataEventArgs(AirspaceTracks));
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