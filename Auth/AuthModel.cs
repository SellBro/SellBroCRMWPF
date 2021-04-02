using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Prism.Commands;
using SellBroCRMWPF.API;
using SellbroCRMWPF.Desktop;
using SellBroCRMWPF.Desktop;

namespace SellBroCRMWPF.Auth
{
    public class AuthModel: INotifyPropertyChanged
    {
        private static Action _goToApp;
        
        private bool saveData = true;
        
        public DelegateCommand SignInCommand { get; }
        public DelegateCommand SignUpCommand { get; }

        public AuthModel(Action goToApp, bool loadUI = true)
        {
            _goToApp = goToApp;

            if (loadUI)
            {
                SignInCommand = new DelegateCommand(SignIn, ValidateFields);
                SignUpCommand = new DelegateCommand(SignUp);
                Directory.CreateDirectory(Variables.EnviromentPath);
            }
        }

        public string Email
        {
            get
            {
                return AuthenticationUser.GetInstance().Email;
            }
            set
            {
                if (AuthenticationUser.GetInstance().Email != value)
                {
                    AuthenticationUser.GetInstance().Email = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Password
        {
            get
            {
                return AuthenticationUser.GetInstance().Password;
            }
            set
            {
                if (AuthenticationUser.GetInstance().Password != value)
                {
                    AuthenticationUser.GetInstance().Password = value;
                    OnPropertyChanged();
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public static bool RememberMe
        {
            get
            {
                return AuthenticationUser.GetInstance().RememberMe;
            }
            set
            {
                
                AuthenticationUser.GetInstance().RememberMe = !AuthenticationUser.GetInstance().RememberMe;
            }
        }

        public static async void SignIn()
        {
            bool result = await UsersAPI.LoginPostRequest();

            if (result)
            {
                if (RememberMe)
                {
                    string[] dataToSave = AuthenticationUser.GetInstance().SaveUser();
                    ProcessData.SaveData(dataToSave);
                }

                _goToApp.Invoke();
            }
            
        }

        private async void SignUp()
        {
            bool result = await UsersAPI.RegisterPostRequest(Email, Password);

            if (result)
            {
                if (RememberMe)
                {
                    string[] dataToSave = AuthenticationUser.GetInstance().SaveUser();
                    ProcessData.SaveData(dataToSave);
                }
                
                _goToApp.Invoke();
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