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
       
        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory(Instance.EnviromentPath);
            
            Instance.MacAdress = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
        }
    }
}
