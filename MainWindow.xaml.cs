using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using SellBroCRMWPF.AES;
using SellBroCRMWPF.API;

namespace SellBroCRMWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CurrentUser currentUser;
        
        private bool saveData = true;
        
        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory(Instance.EnviromentPath);
            
            Instance.MacAdress = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
            
            
            currentUser = UsersAPI.LoadData();
            if (currentUser.Email == "" || currentUser.Password == "")
            {
                // TODO: Handle no user data
            }
            currentUser.Token = UsersAPI.ValidateToken();
            if (currentUser.Token == "")
            {
                // TODO: Handle no token
            }
            
            resp.Text = currentUser.Email + "\n" + currentUser.Password + "\n" + currentUser.Token;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsersAPI.LoginPostRequest(resp);
            if (saveData)
            {
                string[] dataToSave = {"Pepe@gamil.com", "322"};
                UsersAPI.SaveData(dataToSave);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UsersAPI.RegisterPostRequest(resp);
        }
    }
}
