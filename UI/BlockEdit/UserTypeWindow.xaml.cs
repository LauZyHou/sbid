﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sbid.UI
{
    /// <summary>
    /// UserTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserTypeWindow : Window
    {
        public UserTypeWindow()
        {
            InitializeComponent();
        }
        public UserTypeWindow(string suffixName)
        {
            InitializeComponent();
            this.Title += suffixName;
        }
    }
}