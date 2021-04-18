using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Controls;
using SellBroCRMWPF.Auth;
using SellbroCRMWPF.Desktop;
using SellBroCRMWPF.Desktop;
using SellbroCRMWPF.Tables;

namespace SellBroCRMWPF
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            if (LoadUserData()) return;

            Content = new AuthView(GoToApp);
        }

        private bool LoadUserData()
        {
            // TODO: Loading Screen

            UpdateMacAddress();
            
            ProcessData.LoadData();
            
            AuthenticationUser.GetInstance().UpdateToken();
            
            if (AuthenticationUser.GetInstance().IsDataValid())
            {
                // Creating AuthView to initialize AuthModel _goToApp action
                AuthView authView = new AuthView(GoToApp, false);
                AuthModel.SignIn();
                return true;
            }

            return false;
        }

        private void UpdateMacAddress()
        {
            Variables.MacAdress = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
        }

        public void GoToAuth()
        {
            Content = new AuthView(GoToApp);
        }
        
        public void GoToApp()
        {
            Content = new TablesView(GoToAuth);
        }
    }
}