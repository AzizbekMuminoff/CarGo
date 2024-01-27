using CarGo.Controllers;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
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
        private bool isTimeShowed = true;
        private GeneralCOntroller gc;
        public MainWindow()
        {
            InitializeComponent();

            Map map = (Map)FindName("myMap");
            gc = new GeneralCOntroller(map);
            gc.Init();

            Position p = new Position();
            p.Lo = 69.2177;
            p.La = 41.2615;
            p.GpsTime = "1691486143979";

            gc.AddGPS(p, "123");

            p = new Position();
            p.Lo = 69.2144;
            p.La = 41.2644;
            p.GpsTime = "1691486143922";
            
            gc.AddGPS(p, "123");
        }

        private void SendIP(Object sender, RoutedEventArgs e)
        {
            TcpClient client = new TcpClient("83.222.7.183", 65355);
            client.GetStream().Write(new byte[] { 0, 0, 0, 1 });
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
            if (isTimeShowed)
            {
                gc.MapClearTimeStamp();
                isTimeShowed = false;
                btn2.Content = "Показать Таймстемп";
            }
            else
            {
                gc.MapShowTimeStamp();
                isTimeShowed = true;
                btn2.Content = "Скрыть Таймстемп";
            }
        }
    }
}
