using CarGo.Controllers;
using CarGo.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CarGo
{
    /// <summary>
    /// Interaction logic for ServerConf.xaml
    /// </summary>
    public partial class ServerConf : Window
    {
        public ServerConf()
        {
            InitializeComponent();
        }
        private void OkBtn(Object sender, RoutedEventArgs e)
        {
            TCPController.IPADDRESS = AdressBox.Text;
            TCPController.PORTID = Int32.Parse(PortBox.Text);

        }
    }

  
}
