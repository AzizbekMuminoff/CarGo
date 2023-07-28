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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarGo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GeneralCOntroller gc;
        public MainWindow()
        {
            InitializeComponent();

            Map map = (Map)FindName("myMap");
            gc = new GeneralCOntroller(map);
            gc.Init();

            gc.Draw(new Models.GPSModel(69.239007, 41.2820029, "23", "22/55/66"));
            gc.Draw(new Models.GPSModel(69.238007, 41.2846029, "23", "22/55/66"));
            gc.Draw(new Models.GPSModel(69.239007, 41.2856029, "23", "22/55/66"));
            gc.Draw(new Models.GPSModel(69.240007, 41.2866029, "23", "22/55/66"));
            gc.Draw(new Models.GPSModel(69.241007, 41.2886029, "23", "22/55/66"));

        }

        private void SendIP(Object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine("BUTTON PRESSED");
            gc.SendIP();
        }

        private void ParserOpen(Object sender, RoutedEventArgs e)
        {
            Window wd = new ParcerWindow();

            //Debug.WriteLine("Parcer PRESSED");
            wd.Owner = this;
            wd.ShowDialog();
        }
        private void ServerConfOpen(Object sender, RoutedEventArgs e)
        {
            Window wd = new ServerConf();
            wd.Owner = this; 
            wd.ShowDialog();
        }
        private void CloseAll(Object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Debug.WriteLine("Closed");
            gc.CloseAll();
        }
        private void ClearView(Object sender, RoutedEventArgs e)
        {
            gc.Clear();
        }
    }
}
