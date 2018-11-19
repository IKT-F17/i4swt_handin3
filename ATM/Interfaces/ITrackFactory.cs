using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ATM
{
    public interface ITrackFactory
    {
        //string Tag { get; set; }
        //int XCoord { get; set; }
        //int YCoord { get; set; }
        //int Altitude { get; set; }
        //DateTime TimeStamp { get; set; }

        ITrack SpawnTrack(string rawTrackData);

    }


}
