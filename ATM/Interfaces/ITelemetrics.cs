using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface ITelemetrics
    {
        double CalculateHeading(double DeltaX, double DeltaY);
    }
}
