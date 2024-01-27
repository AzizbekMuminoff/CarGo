using CarGo.Controllers;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CarGo.Models
{
    internal class DrawModel
    {
        MapPolyline mapPoly;
        List<Position> positions; 
        Image carImage;
        Location? carLocation;
        MapLayer drawingLayer;
        Color randomColor;
        private static Color[] randColor = { Colors.Brown, Colors.Navy, Colors.OliveDrab, Colors.PaleVioletRed,
                              Colors.Peru, Colors.Purple, Colors.DarkRed, Colors.Yellow, Colors.DarkOrange, Colors.DeepSkyBlue};
        public DrawModel(Map map)
        {
            Random rnd = new Random();
            positions = new List<Position>();

            randomColor = randColor[rnd.Next(randColor.Length - 1)];

            mapPoly = InitPolyline();
            carImage = InitImage();
            drawingLayer = InitLayer();
            carLocation = null;
            
            map.Children.Add(mapPoly);
            map.Children.Add(drawingLayer);
        }

        public void AddGPS(Position p, string carNum)
        {
            positions.Add(p);
            drawCar(p, carNum);
            DrawLabel(p.GpsTime, p.La, p.Lo);
            DrawLine(p);
        }

        private void DrawLine(Position p)
        {
            mapPoly.Locations.Add(new Location(p.La, p.Lo));
        }

        private void drawCar(Position p, string carNum)
        {
            if (carLocation == null)
            {
                carLocation = new Location(p.La, p.Lo);
                var lbl = InitCarNumLabel(carNum);

                drawingLayer.AddChild(carImage, carLocation, PositionOrigin.Center);
                drawingLayer.AddChild(lbl, carLocation, PositionOrigin.BottomLeft);
            }
            else
            {
                Rotate(p);
                carLocation.Latitude = p.La;
                carLocation.Longitude = p.Lo;
            }
        }

        private Label InitCarNumLabel(string carNum)
        {
            Label lbl = new Label();

            lbl.Content = carNum;
            lbl.Tag = "CarLabel";
            lbl.HorizontalAlignment = HorizontalAlignment.Center;
            lbl.VerticalAlignment = VerticalAlignment.Center;
            lbl.VerticalContentAlignment = VerticalAlignment.Center;
            lbl.FontSize = 12;
            lbl.FontWeight = FontWeights.DemiBold;

            return lbl;
        }

        private void DrawLabel(string gpsTime, double La, double Lo)
        {

            Location location = new Location(La, Lo);

            drawingLayer.AddChild(InitLabel(gpsTime), location, PositionOrigin.TopLeft);
        }

        private Label InitLabel(string gpsTime)
        {
            Label lbl = new Label();

            DateTime dt = DateTime.UnixEpoch.AddMilliseconds(double.Parse(gpsTime)).AddHours(5);
            lbl.Content = dt.Hour + ":" + dt.Minute;

            lbl.HorizontalAlignment = HorizontalAlignment.Stretch;
            lbl.VerticalAlignment = VerticalAlignment.Stretch;
            lbl.VerticalContentAlignment = VerticalAlignment.Center;
            lbl.FontWeight = FontWeights.DemiBold;
            lbl.FontSize = 12;

            return lbl;
        }

        private MapLayer InitLayer()
        {
            MapLayer layer = new MapLayer();

            layer.HorizontalAlignment = HorizontalAlignment.Stretch;
            layer.VerticalAlignment = VerticalAlignment.Stretch;

            return layer;
        }



        private void Rotate(Position n)
        {
            if (carLocation!=null)
            {

                double angle =
                    Vetor2DController.CalculateRotation(n.La - carLocation.Latitude,
                    n.Lo - carLocation.Longitude);

                RotateTransform rt = new RotateTransform();

                carImage.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
                //rt.CenterX = gp.Longitude1;
                //rt.CenterY = gp.Latitude1;


                if (!double.IsNaN(angle) && double.IsNormal(angle))
                {
                    rt.Angle = -angle;
                    carImage.RenderTransform = rt;
                }

            }
        }
        private MapPolyline InitPolyline()
        {
            MapPolyline polyline = new MapPolyline();
            
            polyline.Stroke = new SolidColorBrush(randomColor);
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
            bmp.UriSource = new Uri(AppContext.BaseDirectory + GeneralCOntroller.CARIMGADDRESS);
            bmp.EndInit();
            img.Source = bmp;
            img.Width = 44;

            return img;
        }

        internal void TimeStampOn()
        {
            foreach(var item in drawingLayer.Children)
            {
                if(item is Label)
                {
                    Label lbl = (Label)item;

                    if (lbl.Tag == null || !lbl.Tag.Equals("CarLabel"))
                    {
                        lbl.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        internal void TimeStampOff()
        {
            foreach (var item in drawingLayer.Children)
            {
                if (item is Label)
                {
                    Label lbl = item as Label;

                    if (lbl.Tag == null || !lbl.Tag.Equals("CarLabel"))
                    {
                        lbl.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        internal void ReDoWithin(string minBoundary, string maxBoundary)
        {
            foreach(var item in drawingLayer.Children)
            {
               
            }
        }
    }
}
