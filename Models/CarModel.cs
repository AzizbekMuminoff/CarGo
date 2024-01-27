using CarGo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Models
{
    public class CarModel
    {
        private string CarName;
        private string CarNumber;
        private string CarDriver;
        private string CarId;
        private string GpsSim;
        private List<Position> gpsModelList = new List<Position>();

        public CarModel(string carName, string carNumber, string carDriver, string carId)
        {
            CarName = carName;
            CarNumber = carNumber;
            CarDriver = carDriver;
            CarId = carId;
        }

        public void AddGPS(Position gPSModel)
        {
            gpsModelList.Add(gPSModel);
        }

        public Position GetLastGPS()
        {
            return gpsModelList.Last();
        }

        public List<Position> getList()
        {
            return gpsModelList;
        }

        internal string getLog()
        {
            string res = "";

            foreach(Position gpsModel in gpsModelList)
            {
                res += gpsModel.ToString() + "\n";
            }

            return res;
        }

        public string CarName1 { get => CarName; set => CarName = value; }
        public string CarNumber1 { get => CarNumber; set => CarNumber = value; }
        public string CarDriver1 { get => CarDriver; set => CarDriver = value; }
        public string CarId1 { get => CarId; set => CarId = value; }
        public string GpsSim1 { get => GpsSim; set => GpsSim = value; }
    }
}
