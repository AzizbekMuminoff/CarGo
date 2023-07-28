using CarGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Controllers
{
    public class CarListController
    {
        private List<CarModel> carModels;
        private List<GPSModel> gpsModels;

        public CarListController() { 
            carModels = new List<CarModel>();
            gpsModels = new List<GPSModel>();
        }

        public void addGPS(GPSModel gpsModel)
        {
            gpsModels.Add(gpsModel);
            string id = gpsModel.CarID1;
            
            foreach(CarModel t in carModels)
            {
                if(t.CarId1 == id)
                {
                    t.AddGPS(gpsModel);
                }
            }
        }

        public List<string> getAllLog()
        {
            List<string> log =  new List<string>();

            foreach(CarModel t in carModels)
            {
                log.Add(t.getLog());
            }
            return log;
        }

        public void addCarModel(CarModel carModel)
        {
            carModels.Add(carModel);
        }

        public void stateCarsList(List<CarModel> cm)
        {
            carModels = cm;
        }

        public CarModel getCarByID(string carID)
        {
            foreach (CarModel t in carModels)
            {
                if(t.CarId1.Equals(carID))
                    return t;
            }

            return null;
        }

        public List<CarModel> returnFreezed(TimeSpan dt)
        {
            List<CarModel> cr = new List<CarModel>();

            foreach (CarModel t in carModels)
            {
                if (DateTime.Parse(t.GetLastGPS().DateStamp).Subtract(DateTime.Now) >= dt)
                {
                    cr.Add(t);
                }
            }

            return cr;
        }

        internal void close()
        {
            carModels.Clear();
            gpsModels.Clear();
        }
    }
}
