using System;
using ATM.Interfaces;

namespace ATM
{
    public class Separation : ISeparation
    {
        private int verticalSeparation = 300;
        private int horizontalSeparation = 5000;

        public event EventHandler<CollisionEventArgs> OnPlaneCollision;

        public Separation(IAirspace airspace)
        {
            airspace.OnAirspaceCheckEventDone += FindPlanesOnCollision;
        }

        private void FindPlanesOnCollision(object sender, TrackDataEventArgs e)
        {
            foreach (var plane1 in e.TrackData)
            {
                foreach (var plane2 in e.TrackData)
                {
                    if (plane1.Key == plane2.Key) continue;
                    if (Math.Abs(plane1.Value.Altitude - plane2.Value.Altitude) <= verticalSeparation 
                        && Math.Abs(plane1.Value.XCoord - plane2.Value.XCoord) <= horizontalSeparation 
                        && Math.Abs(plane1.Value.YCoord - plane2.Value.YCoord) <= horizontalSeparation)
                    {
                        OnPlaneCollision?.Invoke(this, new CollisionEventArgs(plane1.Value, plane2.Value));

                        Console.SetCursorPosition(0, 21);
                        Console.WriteLine($"Airplane {plane2.Key} and {plane1.Key} is on a colliding path.");
                    }
                }
            }
        }
    }
}