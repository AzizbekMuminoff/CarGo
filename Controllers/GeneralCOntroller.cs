using CarGo.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Controls;

namespace CarGo.Controllers
{
    public class GeneralCOntroller
    {
        MapController mapController;
        TCPController tcpController;

        public static string CARIMGADDRESS = "\\car.png";

        public GeneralCOntroller(Map map) {
            mapController = new MapController(map);
            tcpController = new TCPController();
        }

        //Initialize the server
        public void Init()
        {
            tcpController.gc = this;

            Thread bg = new Thread(new ThreadStart(tcpController.Work));
            bg.SetApartmentState(ApartmentState.STA);

            bg.Start();
            
        }
     
        public void AddGPS(Position gps, string imei)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                mapController.DrawByCoordinates(gps, imei);

            }));

        }

        public void Clear()
        {
            mapController.Empty();
        }

        public void CloseAll()
        {
            mapController.close();
            tcpController.close();
        }

        internal void MapClearTimeStamp()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                mapController.ClearTimeStamps();

            }));
        }

        internal void MapShowTimeStamp()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                mapController.ShowTimeStamp();
            }));
        }
    }
}
