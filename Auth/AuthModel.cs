using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
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
        public DelegateCommand TrueJSON { get; }
        public DelegateCommand GoToGithub { get; }

        public AuthModel(Action goToApp, bool loadUI = true)
        {
            _goToApp = goToApp;

            if (loadUI)
            {
                SignInCommand = new DelegateCommand(SignIn, ValidateFields);
                SignUpCommand = new DelegateCommand(SignUp);
                TrueJSON = new DelegateCommand(ShowTrueJSON);
                GoToGithub = new DelegateCommand(RedirectToGithub);
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
            else
            {
                string caption = "Login Error";
                string text = "Login Error. Invalid Email or Password";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxButton button = MessageBoxButton.OK;

                MessageBox.Show(text, caption, button, icon);
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
            else
            {
                string caption = "Register Error";
                string text = "Register Error. Invalid Email or Password";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxButton button = MessageBoxButton.OK;

                MessageBox.Show(text, caption, button, icon);
            }
        }

        private bool ValidateFields()
        {
            return !String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(Password);
        }

        private void ShowTrueJSON()
        {
            var uri = Instance.TrueJson;
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }

        private void RedirectToGithub()
        {
            var uri = Instance.GithubLink;
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}