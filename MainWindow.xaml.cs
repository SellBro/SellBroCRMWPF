using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using SellBroCRMWPF.Desktop;

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
            Directory.CreateDirectory(Variables.EnviromentPath);
            
            Variables.MacAdress = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
        }
    }
}
