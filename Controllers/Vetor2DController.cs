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
        public static double CalculateRotation(double x, double y)
        {

            double size = Math.Sqrt(x * x + y * y);
            double xD = x / size;
            double xY = y / size;


            return 57.2958*Math.Atan2(xD, xY);
        }
    }
}
