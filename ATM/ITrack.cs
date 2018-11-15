using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
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

            int YCoord { get; set; }

            int Altitude { get; set; }

            DateTime TimeStamp { get; set; }

            void updateTrack(ITrack track);


        }
}
