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
using SellBroCRMWPF;
using SellBroCRMWPF.API;

namespace SellbroCRMWPF.Tables
{
    public partial class TabletsView : UserControl
    {
    
        public TabletsView()
        {
            InitializeComponent();

            GetAllTables();
        }

        private async void GetAllTables()
        {
            await TablesAPI.RequestAllTables();
           
            GetTableData();
        }

        private async void GetTableData(int tableNum = 1)
        {
            Table t = await TablesAPI.RequestTable() ?? await TablesAPI.CreateTable();

            DataTable dt = new DataTable();

            List<string> columnNames = new List<string>();
            dynamic expando = new ExpandoObject();
            
            foreach (var fieldN in t.Fields)
            {
                columnNames.Add(fieldN.Name);
                var fieldNn = fieldN.Name;
                ((IDictionary<String, Object>)expando)[fieldN.ToString()] = fieldNn;
            }

            foreach (KeyValuePair<string, object> kvp in expando)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.IsReadOnly = false;
                column.Header = kvp.Value.ToString();
                column.Binding = new Binding(kvp.Value.ToString());
                dataGrid.Columns.Add(column);
            }
            
            List<ExpandoObject> rows = new List<ExpandoObject>();

            int maxLength = 0;

            foreach (var fieldName in t.Fields)
            {
                int len = fieldName.Items.Count;
                if (len > maxLength)
                {
                    maxLength = len;
                }
            }

            for (int i = 0; i < maxLength; i++)
            {
                dynamic newRow = new ExpandoObject();
                newRow.Names = "Names";
                newRow.Lastnames = "lastname";
                rows.Add(newRow);
                dataGrid.Items.Add(newRow);
            }
            
            dataGrid.CanUserSortColumns = false;
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
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    GridMain.Children.Add(usc);
                    break; 
                case "ItemCreate":
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
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