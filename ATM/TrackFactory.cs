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


        public event EventHandler<Track> OnNewTrackDataReadyEvent;

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
            var Track = new Track(Tag, XCoord, YCoord, Altitude, TimeStamp);
            
            OnNewTrackDataReadyEvent?.Invoke(this,Track);
            return null;

        }
    }


}




//{
//public string Tag { get; set; }
//public int XCoord { get; set; }
//public int YCoord { get; set; }
//public int Altitude { get; set; }
//public DateTime TimeStamp { get; set; }
//public ITrack SpawnTrack(string rawTrackData)
//{
//string[] rawDataSplit = rawTrackData.Split(';');
//Tag = rawDataSplit[0];
//XCoord = Convert.ToInt32(rawDataSplit[1]);
//YCoord = Convert.ToInt32(rawDataSplit[2]);
//Altitude = Convert.ToInt32(rawDataSplit[3]);
//TimeStamp = DateTime.ParseExact(rawDataSplit[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

//return new Track(Tag, XCoord, YCoord, Altitude, TimeStamp);
//}
//}