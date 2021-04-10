using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SellBroCRMWPF.API;

namespace SellBroCRMWPF.Tablets
{
    public partial class TabletsView : UserControl
    {
        private List<Table> _tables = new List<Table>();
        public TabletsView()
        {
            InitializeComponent();
            
            GetTableData();
        }

        private async void GetTableData(int tableNum = 1)
        {
            Table t = await TablesAPI.RequestTable();
            
            //TODO: Use table date in the table
            
            MessageBox.Show(t.ToString());
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