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

            GetTableData();
        }

        private async void GetAllTables()
        {
            await TablesAPI.RequestAllTables();
        }

        private async void GetTableData(int tableNum = 1)
        {
            // If no tables - create
            Table t = await TablesAPI.RequestTable() ?? await TablesAPI.CreateTable();

            DataTable dt = new DataTable();

            List<string> columnNames = new List<string>();
            dynamic expando = new ExpandoObject();
            
            foreach (var fieldN in t.Fields)
            {
                MessageBox.Show("building expando" + fieldN.Name);
                columnNames.Add(fieldN.Name);
                var fieldNn = fieldN.Name;
                ((IDictionary<String, Object>)expando)[fieldN.ToString()] = fieldNn;
            }

            foreach (KeyValuePair<string, object> kvp in expando)
            {
                MessageBox.Show(kvp.Value.ToString());
                DataGridTextColumn column = new DataGridTextColumn();
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

            MessageBox.Show(maxLength.ToString());

            for (int i = 0; i < maxLength; i++)
            {
                dynamic newRow = new ExpandoObject();
                newRow.Names = "Names";
                newRow.Lastnames = "lastname";
                rows.Add(newRow);
                dataGrid.Items.Add(newRow);
            }
            
            
           // dataGrid.Items.Add(row);

        /*foreach (var fieldName in t.Fields)
        {
            dt.Columns.Add(fieldName.Name);
            MessageBox.Show(fieldName.Name);

            foreach (var fieldValue in fieldName.Items)
            {
                DataRow row = dt.NewRow();
                row[0] = fieldValue.Value;
                dt.Rows.Add(row);
            }
        */
            //}

            //dataGrid.ItemsSource = dt.AsEnumerable();
            dataGrid.CanUserSortColumns = false;
            //TODO: Use table date in the table
            // MessageBox.Show(t.ToString());
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
                    //usc = new UserControlHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate": 
                    // usc = new UserControlCreate();
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