using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Track : ITrack
    {
        private string _tag;
        //private int _xcoord;
        //private int _ycoord;
        //private int _altitude;
        //private DateTime _timeStamp;

        public Track(string tag, int xcoord, int ycoord, int altitude, DateTime timeStamp)
        {
            Tag = tag;
            XCoord = xcoord;
            YCoord = ycoord;
            Altitude = altitude;
            TimeStamp = timeStamp;
        }

        public string Tag
        {
            get => _tag;
            set
            {
                if (value.Length != 6)
                    return;

                _tag = value;
            }
        }

        public int XCoord { get; set; }

        public int YCoord { get; set; }

        public int Altitude { get; set; }

        public DateTime TimeStamp { get; set; }

        public void updateTrack(ITrack _track)
        {

            var track = _track;
            track.OnNewTrackDataReadyEvent += Track_OnNewTrackDataReadyEvent;


            // TODO: Heading/Course
            //var xDiff = track.XCoord - this.XCoord;
            //var yDiff = track.YCoord - this.YCoord;


            // TODO: Speed/Velocity (m/s)
        }

        public void Track_OnNewTrackDataReadyEvent(object sender, Track e)
        {
            XCoord = e.XCoord;
            YCoord = e.YCoord;
            Altitude = e.Altitude;
            TimeStamp = e.TimeStamp;
            Tag = e.Tag;


            var DeltaX = e.XCoord - this.XCoord;
            var DeltaY = e.YCoord - this.YCoord;
            var DeltaTime = e.TimeStamp - this.TimeStamp;
            var DeltaAltitude = e.Altitude - this.Altitude;

            var heading = CalcHeading(DeltaX, DeltaY);


            /// Note to self: 
            /// INFO: Velocity calc: (sqrt(DeltaX^2 + DeltaY^2)) / DeltaTime
            /// INFO: Heading calc: atan2(DeltaY,DeltaX)*180/PI -> if result < 0 += 306

            Console.WriteLine($"Flight: {Tag}, have a heading of: {heading} degrees.");


        }

        private static double CalcHeading(double xDiff, double yDiff)
        {
            var heading = 90.0d - Math.Atan2(yDiff, xDiff) * 180 / Math.PI;

            if (heading < 0.0d) heading += 360.0;

            return heading;
        }
    }
}