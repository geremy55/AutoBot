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

namespace AutoBot.Views
{
    /// <summary>
    /// Interaction logic for PlayerSettingsView.xaml
    /// </summary>
    public partial class PlayerSettingsView : Window
    {
        public PlayerSettingsView()
        {
            InitializeComponent();
        }

        private void PlayerForm_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }               

    }
}
