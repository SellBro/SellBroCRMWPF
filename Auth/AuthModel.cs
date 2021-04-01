using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Prism.Commands;
using SellBroCRMWPF.API;
using SellbroCRMWPF.Desktop;
using SellBroCRMWPF.Desktop;

namespace SellBroCRMWPF.Auth
{
    public class AuthModel: INotifyPropertyChanged
    {
        private AuthenticationUser _authUser = new AuthenticationUser();
        private CurrentUser _currentUser;
        private Action _goToApp;
        
        private bool saveData = true;
        
        public DelegateCommand SignInCommand { get; }
        public DelegateCommand SignUpCommand { get; }

        public AuthModel(Action goToApp)
        {
            _goToApp = goToApp;
            SignInCommand = new DelegateCommand(SignIn, ValidateFields);
            SignUpCommand = new DelegateCommand(SignUp);
            Directory.CreateDirectory(Variables.EnviromentPath);
            
            Variables.MacAdress = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
            
            _currentUser = ProcessData.LoadData();
            if (_currentUser.Email == "" || _currentUser.Password == "")
            {
                // TODO: Handle no user data
            }
            _currentUser.Token = ProcessToken.ValidateToken();
            if (_currentUser.Token == "")
            {
                // TODO: Handle no token
            }
        }

        public string Email
        {
            get
            {
                return _authUser.Email;
            }
            set
            {
                if (_authUser.Email != value)
                {
                    _authUser.Email = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Password
        {
            get
            {
                return _authUser.Password;
            }
            set
            {
                if (_authUser.Password != value)
                {
                    _authUser.Password = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private async void SignIn()
        {
            bool result = await UsersAPI.LoginPostRequest();
            
            // remember me checkbox
            if (saveData)
            {
                string[] dataToSave = {"Pepe@gamil.com", "322"};
                ProcessData.SaveData(dataToSave);
            }
            
            _goToApp.Invoke();
        }

        private async void SignUp()
        {
            bool result = await UsersAPI.RegisterPostRequest(Email, Password);
            
            // remember me checkbox
            if (saveData)
            {
                string[] dataToSave = {"Pepe@gamil.com", "322"};
                ProcessData.SaveData(dataToSave);
            }
        }

        private bool ValidateFields()
        {
            return !String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(Password);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}