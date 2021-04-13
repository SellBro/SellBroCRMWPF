using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SellBroCRMWPF.API;
using SellBroCRMWPF.Auth;

namespace SellBroCRMWPF.Tablets
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
    }
}