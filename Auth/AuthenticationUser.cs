using System;
using System.Collections.Generic;
using SellBroCRMWPF.API;

namespace SellBroCRMWPF.Auth
{
    public class AuthenticationUser
    {
        private static AuthenticationUser _instance;
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool RememberMe { get; set; }
        
        public List<Table> Tables = new List<Table>();

        public static AuthenticationUser GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AuthenticationUser();
            }

            return _instance;
        }

        public bool IsDataValid()
        {
            return !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password);
        }

        public bool IsTokenValid()
        {
            return !String.IsNullOrEmpty(Token);
        }

        public async void UpdateToken()
        {
            await UsersAPI.LoginPostRequest();
        }
        
        public string[] SaveUser()
        {
            string[] data = {Email, Password};
            return data;
        }
    }
}