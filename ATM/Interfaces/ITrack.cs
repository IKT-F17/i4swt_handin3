using System;

namespace ATM.Interfaces
{
    public interface ITrack
    {
        //1 Tag Track tag(text)
        //2 X coordinate Track X coordinate in meters
        //3 Y coordinate Track Y coordinate in meters
        //4 Altitude Track altitude in meters
        //5 Timestamp Timestamp of the above data(”yyyymmddhhmmssfff”)

        string Tag { get; set; }

        int XCoord { get; set; }
        int XCoordDelta { get; set; }

        int YCoord { get; set; }
        int YCoordDelta { get; set; }

        int Altitude { get; set; }
        int AltitudeDelta { get; set; }

        DateTime TimeStamp { get; set; }
        int TimeStampDelta { get; set; }

        int Heading { get; set; }

        int Velocity { get; set; }

        void UpdateTrack(ITrack track);
    }
}
