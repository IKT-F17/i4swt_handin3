using System;
using System.Linq.Expressions;

namespace ATM.Interfaces
{
    public interface ISeparation
    {
        void FindPlanesOnCollision(TrackDataEventArgs airspaceTracks);
        //event EventHandler<TrackDataEventArgs> OnSeparationEvents; /// TODO: Hvis Output skulle implementeres skal eventet her laves. 

    }
}