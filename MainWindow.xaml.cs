using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using SellBroCRMWPF.API;

namespace SellBroCRMWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string activeToken;
        
        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory(Instance.EnviromentPath);
            
            Instance.MacAdress = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
            
            resp.Text = Instance.MacAdress;
            ValidateToken();
        }

        private void ValidateToken()
        {
            string jwtPath = Instance.EnviromentPath + Instance.JwtFileName;
            if (File.Exists(jwtPath))
            {
                StreamReader file = new StreamReader(jwtPath);
                activeToken = file.ReadLine();
                activeToken = Decrypt(activeToken, Instance.MacAdress);
            }
        }
        
        public static string Encrypt(string text, string key)
        {
            return text;
        }

        public static string Decrypt(string encryptedText, string key)
        {
            return encryptedText;
        }
        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsersAPI.LoginPostRequest(resp);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UsersAPI.RegisterPostRequest(resp);
        }
    }
}
