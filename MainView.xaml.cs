using System.Windows.Controls;
using SellBroCRMWPF.Auth;
using SellBroCRMWPF.Tablets;

namespace SellBroCRMWPF
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            Content = new AuthView(GoToApp);
        }

        public void GoToAuth()
        {
            Content = new AuthView(GoToAuth);
        }

        public void GoToApp()
        {
            Content = new TabletsView();
        }
    }
}