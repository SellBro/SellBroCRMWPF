using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Commands;
using SellBroCRMWPF;
using SellBroCRMWPF.API;

namespace SellbroCRMWPF.Tables
{
    public partial class TablesView : UserControl
    {
        private TablesModel _tablesModel;

        public TablesView(Action goToAuth)
        {
            InitializeComponent();
            _tablesModel = new TablesModel(goToAuth);
            DataContext = _tablesModel;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }

        int runningIndex;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = "a" + runningIndex;

            var col = new DataGridTextColumn();
            col.Header = text;
            col.Binding = new Binding(text);
            dataGrid.Columns.Add(col);

            runningIndex++;
        }
    }
}