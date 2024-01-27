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
    /// Interaction logic for ParcerWindow.xaml
    /// </summary>
    public partial class ParcerWindow : Window
    {
        public ParcerWindow()
        {
            InitializeComponent();
            ParcerTextBox.Text= DebugLogController.GetLog() ;
        }
        private void OkBtn(Object sender, RoutedEventArgs e)
        {
            
        }
    }
}
