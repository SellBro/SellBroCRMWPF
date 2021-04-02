using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SellBroCRMWPF.Auth;
using SellBroCRMWPF.Desktop;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace SellBroCRMWPF.API
{
    public class UsersAPI
    {
        public static async Task<bool> LoginPostRequest(AuthenticationUser user)
        {
            LoginUser loginUser = new LoginUser(user);
            
            // TODO: extract this logic
            string json = JsonSerializer.Serialize(loginUser);
            var data = new StringContent(json, Encoding.UTF8, Instance.MediaType);

            using var client = new HttpClient();
            
            var response = await client.PostAsync(Instance.Login, data);
            
            var res = response.Content.ReadAsStringAsync();

            if (!res.IsCompleted)
            {
                throw new Exception("Login Result exception");
            }
            Debug.WriteLine(res.Result);
            
            ProcessToken.ParseToken(res.Result);
            // TODO: handle request
            return true;
        }

        public static async Task<bool> RegisterPostRequest(string email, string password)
        {
            RegisterUser registerUser = new RegisterUser{Email = email, Password = password};
            
            // TODO: extract this logic
            string json = JsonSerializer.Serialize(registerUser);
            var data = new StringContent(json, Encoding.UTF8, Instance.MediaType);
            
            using var client = new HttpClient();
            
            var response = await client.PostAsync(Instance.Register, data);
            
            var res = response.Content.ReadAsStringAsync();
            
            if (!res.IsCompleted)
            {
                throw new Exception("Register Result exception");
            } 
            Debug.WriteLine(res.Result);
            
            ProcessToken.ParseToken(res.Result);
            // TODO: handle request
            return true;
        }
        
        
    }
}