using System;
using System.Linq.Expressions;

namespace ATM.Interfaces
{
    public interface ISeparation
    {
        event EventHandler<CollisionEventArgs> OnPlaneCollision;
        event EventHandler<CollisionEventArgs> OnPlaneAvoidedCollision;

        //event EventHandler<TrackDataEventArgs> OnSeparationEvents; /// TODO: Hvis Output skulle implementeres skal eventet her laves. 

    }
}