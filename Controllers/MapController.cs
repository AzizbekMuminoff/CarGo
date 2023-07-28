using CarGo.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        private Dictionary<string, Tuple<MapPolyline, Tuple<Image, Location>>> myColors;
        private Random rnd;
        public MapController(Map map)
        {
            myMap = map;   
            rnd = new Random();
            myColors = new Dictionary<string, Tuple<MapPolyline, Tuple<Image, Location>>>();
        }

        public void DrawByCoordinates(GPSModel gp)
        {
            Tuple<MapPolyline, Tuple<Image, Location>> p = myColors.GetValueOrDefault(gp.CarID1);
            Location loc = new Location(gp.Latitude1, gp.Longitude1);

            if (p == null || p == default || p.Item1==null){

                //line create
                MapPolyline polyline = InitPolyline();

                //Image init
                Image img = InitImage();

                MapLayer layer = new MapLayer();
                layer.HorizontalAlignment = HorizontalAlignment.Stretch;    
                layer.VerticalAlignment = VerticalAlignment.Stretch;

                
                layer.AddChild(img, loc, PositionOrigin.Center);

                p = new(polyline, new Tuple<Image, Location>(img, loc));
               
                myColors.Add(gp.CarID1, p);
                    
                myMap.Children.Add(p.Item1);
                myMap.Children.Add(layer);

            }

            p.Item1.Locations.Add(new Location(gp.Latitude1, gp.Longitude1));

            Rotate(p);

            p.Item2.Item2.Latitude = gp.Latitude1;   
            p.Item2.Item2.Longitude = gp.Longitude1;
        }

        

        public void Empty()
        {
            foreach(KeyValuePair<string, Tuple<MapPolyline, Tuple<Image, Location>>> t in myColors){

            }

            myMap.Children.Clear();
        }

        internal void close()
        {

        }



        private void Rotate(Tuple<MapPolyline, Tuple<Image, Location>> p)
        {
            if (p.Item1.Locations.Count > 1)
            {
                int countL = p.Item1.Locations.Count;
                double angle = 
                    Vetor2DController.CalculateRotation(p.Item1.Locations[countL-1].Latitude - p.Item1.Locations[countL - 2].Latitude,
                    p.Item1.Locations[countL - 1].Longitude - p.Item1.Locations[countL - 2].Longitude);

                RotateTransform rt = new RotateTransform();

                p.Item2.Item1.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
                //rt.CenterX = gp.Longitude1;
                //rt.CenterY = gp.Latitude1;

                rt.Angle = -angle;

                p.Item2.Item1.RenderTransform = rt;

                //Debug.WriteLine(angle);
            }

        }

        private MapPolyline InitPolyline()
        {
            MapPolyline polyline = new MapPolyline();
            System.Windows.Media.Color randomColor = System.Windows.Media.Color.FromRgb((Byte)rnd.Next(256), (Byte)rnd.Next(256), (Byte)rnd.Next(256));
            randomColor = Colors.DarkKhaki;
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(randomColor);
            polyline.StrokeThickness = 8;
            polyline.Opacity = 0.8;
            polyline.Locations = new LocationCollection();

            return polyline;
        }

        private Image InitImage()
        {
            Image img = new Image();
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(GeneralCOntroller.CARIMGADDRESS);
            bmp.EndInit();
            img.Source = bmp;
            img.Width = 44;

            return img;
        }
    }
}
