﻿using System;
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

namespace SAE_TANK
{
    /// <summary>
    /// Logique d'interaction pour EcrandFin.xaml
    /// </summary>
    public partial class EcrandFin : Window
    {
        public EcrandFin()
        {
            InitializeComponent();

            
        }

        private void bt_Rejouer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bt_Quitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
