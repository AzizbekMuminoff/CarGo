using CarGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Controllers
{
    class Vetor2DController
    {

        public static double CalculateRotation(GPSModel last, GPSModel current)
        {
            double x = current.Latitude1 - last.Latitude1;
            double y = current.Longitude1 - last.Longitude1;

            double size = Math.Sqrt(x*x + y*y);
            double xD = x / size;
            double xY = y / size;


            return Math.Atan2(xY, xD);
        }

        public static double CalculateRotation(double x, double y)
        {

            double size = Math.Sqrt(x * x + y * y);
            double xD = x / size;
            double xY = y / size;


            return 57.2958*Math.Atan2(xD, xY);
        }
    }
}
