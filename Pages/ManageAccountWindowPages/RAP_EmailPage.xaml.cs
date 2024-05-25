﻿using Mist.Helper;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mist.Pages.ManageAccountWindowPages
{
    /// <summary>
    /// Interaction logic for RAP_EmailPage.xaml
    /// </summary>
    public partial class RAP_EmailPage : Page
    {
        public RAP_EmailPage()
        {
            InitializeComponent();
        }

        private void forgotEmail_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new RAP_PhonePage());
        }
    }
}