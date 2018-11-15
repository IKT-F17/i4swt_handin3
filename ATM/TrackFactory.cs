using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace ATM
{
    public class TrackFactory : ITrackFactory
    {
        private string _tag;

        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }

        public event EventHandler<ITrack> OnNewTrackDataReadyEvent;

        public void Track(string tag, int xcoord, int ycoord, int altitude, DateTime timeStamp)
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

        public ITrack SpawnTrack(string rawTrackData)
        {
            string[] rawDataSplit = rawTrackData.Split(';');
            string Tag = rawDataSplit[0];
            int XCoord = Convert.ToInt32(rawDataSplit[1]);
            int YCoord = Convert.ToInt32(rawDataSplit[2]);
            int Altitude = Convert.ToInt32(rawDataSplit[3]);
            DateTime TimeStamp =
                DateTime.ParseExact(rawDataSplit[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            //return new Track(Tag, XCoord, YCoord, Altitude, TimeStamp);

            var track = new Track(Tag, XCoord, YCoord, Altitude, TimeStamp);
            OnNewTrackDataReadyEvent?.Invoke(this, track);
            return null;
        }

        void UpdateTrack(ITrack _track)
        {
            var track = _track;
            track.Track_OnNewTrackDataReadyEvent += Track_OnNewTrackDataReadyEvent;
        }
    }
}