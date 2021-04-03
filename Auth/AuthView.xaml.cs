using System;
using System.Windows;
using System.Windows.Controls;

namespace SellBroCRMWPF.Auth
{
    public partial class AuthView : UserControl
    {
        private AuthModel _authModel;
        public AuthView(Action goToTablets, bool loadUI = true)
        {
            InitializeComponent();
            _authModel = new AuthModel(goToTablets, loadUI);
            DataContext = _authModel;
        }

        private void PbPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            // _authModel.Password = PbPassword.Password;
        }

            }
}