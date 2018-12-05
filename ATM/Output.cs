using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ATM.Interfaces;

namespace ATM
{
    public class Output
    {
        private int nCharactersCollidingPlanes = 0;
        private int _airplanesInAirspace = 0;
        Dictionary<string, DateTime> _screenTimerEnter = new Dictionary<string, DateTime>();
        Dictionary<string, DateTime> _screenTimerExit = new Dictionary<string, DateTime>();
        Dictionary<string, List<string>> _screenTimerCollisions = new Dictionary<string, List<string>>();


        public Output(ITrackFactory trackFactory, IAirspace airspace, ISeparation separation)
        {
            airspace.OnAirspaceCheckEventDone += Airspace_OnAirspaceCheckEventDone;
            trackFactory.OnTrackListDoneEvent += TrackFactory_OnTrackListDoneEvent;
            airspace.OnPlaneEnteringAirspace += Airspace_OnPlaneEnteringAirspace;
            airspace.OnPlaneExitingAirspace += Airspace_OnPlaneExitingAirspace;
            separation.OnPlaneCollision += Separation_OnPlaneCollision;
            separation.OnPlaneAvoidedCollision += Separation_OnPlaneAvoidedCollision;

        }

        private void Separation_OnPlaneAvoidedCollision(object sender, CollisionEventArgs e)
        {
            List<string> onCollisionWith;
            if (_screenTimerCollisions.TryGetValue(e.Plane1.Tag, out onCollisionWith))
            {
                onCollisionWith.Remove(e.Plane2.Tag);
            }

            if (_screenTimerCollisions.TryGetValue(e.Plane2.Tag, out onCollisionWith))
            {
                onCollisionWith.Remove(e.Plane1.Tag);
            }
        }

        private void Separation_OnPlaneCollision(object sender, CollisionEventArgs e)
        {
            UpdateCollision(e.Plane1, e.Plane2);
            UpdateCollision(e.Plane2, e.Plane1);
        }

        private void UpdateCollision(ITrack plane1, ITrack plane2)
        {
            List<string> onCollisionWith;
            if (!_screenTimerCollisions.TryGetValue(plane1.Tag, out onCollisionWith))
            {
                onCollisionWith = new List<string>() {plane2.Tag}; 
                _screenTimerCollisions.Add(plane1.Tag, onCollisionWith);
            }
            else
            {
                if (!onCollisionWith.Contains(plane2.Tag))
                {
                    onCollisionWith.Add(plane2.Tag);
                }
            }
        }

        private void Airspace_OnPlaneExitingAirspace(object sender, TrackEventArgs e)
        {
            if (!_screenTimerExit.ContainsKey(e.Track.Tag))
            {
                _screenTimerExit.Add(e.Track.Tag, DateTime.Now);
            }
        }

        private void Airspace_OnPlaneEnteringAirspace(object sender, TrackEventArgs e)
        {
            if (!_screenTimerEnter.ContainsKey(e.Track.Tag))
            {
                _screenTimerEnter.Add(e.Track.Tag, DateTime.Now);
            }
        }

        private void TrackFactory_OnTrackListDoneEvent(object sender, TrackDataEventArgs e)
        {
            SetCursorPosition(0, 2);
            Console.Write($"Outside: {e.TrackData.Count - _airplanesInAirspace} Inside: {_airplanesInAirspace}", CultureInfo.CurrentCulture);


            var printValidPlanesEntered =
                _screenTimerEnter.Where(x => (DateTime.Now - x.Value) <= TimeSpan.FromSeconds(5)).Select(x => x.Key).ToList();
            var expiredPlanesEntered = _screenTimerEnter.Keys.Except(printValidPlanesEntered).ToList();
            SetCursorPosition(0, 3);
            Console.Write($"Following planes has entered the airspace: {string.Join(", ", printValidPlanesEntered)}  {string.Join("  ", expiredPlanesEntered.Select(x => "".PadRight(x.Length, ' ')))}", CultureInfo.CurrentCulture);

            var printValidPlanesExited =
                _screenTimerExit.Where(x => (DateTime.Now - x.Value) <= TimeSpan.FromSeconds(5)).Select(x => x.Key).ToList();
            var expiredPlanesExited = _screenTimerExit.Keys.Except(printValidPlanesExited).ToList();

            SetCursorPosition(0, 4);
            Console.Write($"Following planes has exited the airspace: {string.Join(", ", printValidPlanesExited)}  {string.Join("  ", expiredPlanesExited.Select(x => "".PadRight(x.Length, ' ')))}", CultureInfo.CurrentCulture);

            foreach (var tag in expiredPlanesEntered)
            {
                _screenTimerEnter.Remove(tag);
            }

            foreach (var tag in expiredPlanesExited)
            {
                _screenTimerExit.Remove(tag);
            }
        }

        private void Airspace_OnAirspaceCheckEventDone(object sender, TrackDataEventArgs e)
        {
            var nCharCount = 0;
            for (int i = 0; i < e.TrackData.Count; i++)
            {
                var track = e.TrackData.ElementAt(i);


                var collidingPlanesText = "".PadRight(50, ' ');
                List<string> collidingPlaneTag;
                if (_screenTimerCollisions.TryGetValue(track.Value.Tag, out collidingPlaneTag)&&collidingPlaneTag.Any())
                {
                    collidingPlanesText = $"Planes on collision path: {string.Join(", ", collidingPlaneTag)}";
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                SetCursorPosition(0, 5 + i);
                var outputData = ( $"Tag: {track.Value.Tag.PadRight(8,' ')}" +           
                                    $"X-Coord: {track.Value.XCoord.ToString().PadRight(7, ' ')}" +
                                    $"Y-Coord: {track.Value.YCoord.ToString().PadRight(7, ' ')}" +
                                    $"Altitude: {track.Value.Altitude.ToString().PadRight(7, ' ')}" +
                                    $"Timestamp: {track.Value.TimeStamp:yyyyMMddHHmmssfff}   " +
                                    $"Heading: {track.Value.Heading.ToString().PadRight(5, ' ')}" +
                                    $"Velocity: {track.Value.Velocity.ToString().PadRight(5, ' ')}" +
                                    $"{collidingPlanesText}");

                nCharCount = Math.Max(outputData.Length, nCharCount);
                Console.Write(outputData+"".PadRight(Math.Max(0, nCharactersCollidingPlanes - outputData.Length),' '));
            }

            var emptyLines = _airplanesInAirspace - e.TrackData.Count;

            for (int i = 0; i < emptyLines; i++)
            {
                SetCursorPosition(0, 5 + e.TrackData.Count + i);
                Console.Write(" ");
            }

            _airplanesInAirspace = e.TrackData.Count;
            nCharactersCollidingPlanes = nCharCount;
        }

        private void SetCursorPosition(int col, int row)
        {
            try
            {
                Console.SetCursorPosition(col, row);
            }
            catch (Exception e)
            {
                
            }
        }
    }
}