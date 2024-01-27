﻿using CarGo.Models;
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
    /// Interaction logic for AutoPark.xaml
    /// </summary>
    public partial class AutoPark : Window
    {
        public AutoPark()
        {
            InitializeComponent();
            SaveBTN.Click += SaveData;
        }

        private void SaveData(Object sender, EventArgs e)
        {
            CarModel carm = new CarModel("DAMAS", CarNumber.Text, CarDriver.Text, Imei.Text);
            
        }
    }
}
