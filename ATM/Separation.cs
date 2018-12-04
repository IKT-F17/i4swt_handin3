using System;
using System.Collections.Generic;
using ATM.Interfaces;

namespace ATM
{
    public class Separation : ISeparation
    {
        private int verticalSeparation = 300;
        private int horizontalSeparation = 5000;

        private List<string> _planesOnCollision =new List<string>();

        public event EventHandler<CollisionEventArgs> OnPlaneCollision;
        public event EventHandler<CollisionEventArgs> OnPlaneAvoidedCollision;

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
                    string collisionKey = $"{plane1.Key};{plane2.Key}";
                    if (plane1.Key == plane2.Key) continue;
                    if (Math.Abs(plane1.Value.Altitude - plane2.Value.Altitude) <= verticalSeparation 
                        && Math.Abs(plane1.Value.XCoord - plane2.Value.XCoord) <= horizontalSeparation 
                        && Math.Abs(plane1.Value.YCoord - plane2.Value.YCoord) <= horizontalSeparation)
                    {
                        
                        if (!_planesOnCollision.Contains(collisionKey))
                        {
                            _planesOnCollision.Add(collisionKey);
                            OnPlaneCollision?.Invoke(this, new CollisionEventArgs(plane1.Value, plane2.Value));
                        }
                    }
                    else if (_planesOnCollision.Contains(collisionKey))
                    {
                        _planesOnCollision.Remove(collisionKey);
                        OnPlaneAvoidedCollision?.Invoke(this, new CollisionEventArgs(plane1.Value, plane2.Value));
                    }
                }
            }
        }
    }
}