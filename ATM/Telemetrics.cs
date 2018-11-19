using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class Telemetrics : ITelemetrics
    {
        public double CalculateHeading(double DeltaX, double DeltaY)
        {
            var heading = 90.0d - Math.Atan2(DeltaY, DeltaX) * 180 / Math.PI;

            if (heading < 0.0d) heading += 360.0;

            return heading;
        }
    }



}
