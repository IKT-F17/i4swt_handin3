using System;


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

        public void updateTrack(ITrack track)
        {
            // TODO: Heading/Course
            var DeltaX = track.XCoord - this.XCoord;
            var DeltaY = track.YCoord - this.YCoord;
            var Heading = CalcHeading(DeltaX, DeltaY);
            //CalcHeading(DeltaX, DeltaY);

            Console.WriteLine($"Flight: {track.Tag}, have a heading of: {Heading} degrees.");

            // TODO: Speed/Velocity (m/s)
        }

        private static double CalcHeading(double DeltaX, double DeltaY)
        {
            var heading = 90.0d - Math.Atan2(DeltaY, DeltaX) * 180 / Math.PI;

            if (heading < 0.0d) heading += 360.0;

            return heading;
        }
    }
}