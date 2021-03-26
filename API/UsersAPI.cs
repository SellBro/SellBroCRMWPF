using System.Net.Http;
using System.Text;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace SellBroCRMWPF.API
{
    public class UsersAPI
    {
        
        public static async void LoginPostRequest(TextBlock resp)
        {
            LoginUser loginUser = new LoginUser{Email = "Pepe228@gmail.com", Password = "228"};
            
            string json = JsonSerializer.Serialize(loginUser);
            var data = new StringContent(json, Encoding.UTF8, Instance.MediaType);

            using var client = new HttpClient();
            
            var response = await client.PostAsync(Instance.Login, data);
            
            var res = response.Content.ReadAsStringAsync();
            
            ParseToken(res.Result, resp);
        }

        public static async void RegisterPostRequest(TextBlock resp)
        {
            RegisterUser registerUser = new RegisterUser{Email = "Pepe228@gmail.com", Password = "228"};
            
            string json = JsonSerializer.Serialize(registerUser);
            var data = new StringContent(json, Encoding.UTF8, Instance.MediaType);
            
            using var client = new HttpClient();
            
            var response = await client.PostAsync(Instance.Register, data);
            
            var res = response.Content.ReadAsStringAsync();
            
            ParseToken(res.Result, resp);
        }
        
        private static void ParseToken(string json, TextBlock resp)
        {
            JObject obj = JObject.Parse(json);
            string token = (string) obj.SelectToken("data.authToken");

            resp.Text = token;
        }
    }
}