using CarGo.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CarGo.Controllers
{
    public class MapController
    {
        private Map myMap;
        private Dictionary<string, DrawModel> drawingDictionary;
        private Random rnd;
        private static string minBoundary = "";
        private static string maxBoundary = "";
        public MapController(Map map)
        {
            myMap = map;   
            rnd = new Random();
            drawingDictionary = new Dictionary<string, DrawModel>();
        }


        public void RedrawByTime()
        {
            foreach(var pM in drawingDictionary.Values)
            {
                pM.ReDoWithin(minBoundary, maxBoundary);

            }
        }
        public void DrawByCoordinates(Position gp, string imei)
        {
            DrawModel? p = drawingDictionary.GetValueOrDefault(imei);

            if (p == null || p == default){

                p = new DrawModel(myMap);
               
                drawingDictionary.Add(imei, p);
            }

            p.AddGPS(gp, imei);
        }


        public void Empty()
        {
            myMap.Children.Clear();
        }


        public void drawByDate(string t)
        {
            foreach(var imei in drawingDictionary.Keys){
                
            }
        }
        internal void ClearTimeStamps()
        {

            foreach(var item in drawingDictionary.Values)
            {
                item.TimeStampOff();
            }
        }


        internal void ShowTimeStamp()
        {
            foreach (var item in drawingDictionary.Values)
            {
                item.TimeStampOn();
            }
        }


        internal void close()
        {

        }
    }
}
