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

namespace CarGo.Controllers
{
    public class GeneralCOntroller
    {
        private CarListController carListController;
        MapController mapController;
        TCPController tcpController;

        public static string CARIMGADDRESS = "C:\\Users\\azizb\\source\\repos\\CarGo\\res\\car.png";

        public GeneralCOntroller(Map map) {
            mapController = new MapController(map);
            tcpController = new TCPController();
            carListController = new CarListController();

            GPSModel.ParseMode = GPSModel.ParsingMode.Split("\n");
        }

        //Initialize the server
        public void Init()
        {
            tcpController.gc = this;
            Thread bg = new Thread(new ThreadStart(tcpController.Work));
            bg.SetApartmentState(ApartmentState.STA);

            bg.Start();

        }
     
        public void AddGPS(GPSModel gps)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {

                Draw(gps);
                carListController.addGPS(gps);
            }));
            //gpsAwaiting.Add(gps);

        }
        public void Draw(GPSModel gps)
        {
            mapController.DrawByCoordinates(gps);
        }


        public void Clear()
        {
            mapController.Empty();
        }

        public void CloseAll()
        {
            mapController.close();
            tcpController.close();
            carListController.close();
        }




        
      public void SendIP()
      {
          Console.WriteLine("SENDING THE MESSAGE");
          try
          {
              using TcpClient client = new("213.230.76.151", TCPController.PORTID);
              Byte[] data = System.Text.Encoding.ASCII.GetBytes("69.249007 41.2806029 \n 22 22/55/66");
              NetworkStream stream = client.GetStream();
              stream.Write(data, 0, data.Length);

              data = new Byte[256];

              // String to store the response ASCII representation.
              String responseData = String.Empty;

              // Read the first batch of the TcpServer response bytes.
              Int32 bytes = stream.Read(data, 0, data.Length);
              responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
              Debug.WriteLine("Received: {0}", responseData);

          } catch (Exception e)
          {
              Debug.WriteLine("W: ", e.Message);
          }
      }
    }
}
