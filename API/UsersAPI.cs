using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;
using SellBroCRMWPF.AES;
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


            token = AesOperation.EncryptString(Instance.MacAdress, token);
            resp.Text = token; 

            FileStream stream = new FileStream(Instance.EnviromentPath + Instance.JwtFileName, FileMode.Create);
            stream.Close();
            
            StreamWriter sw = new StreamWriter(Instance.EnviromentPath + Instance.JwtFileName, true, Encoding.UTF8);
            sw.Write(token);
            sw.Close();
        }

        public static string ValidateToken()
        {
            string activeToken = "";
            string jwtPath = Instance.EnviromentPath + Instance.JwtFileName;
            if (File.Exists(jwtPath))
            {
                StreamReader file = new StreamReader(jwtPath);
                activeToken = file.ReadLine();
                activeToken = AesOperation.DecryptString(Instance.MacAdress, activeToken);
                file.Close();
            }

            return activeToken;
        }
        
        public static void SaveData(string[] data)
        {
            FileStream stream = new FileStream(Instance.EnviromentPath + Instance.DataFileName, FileMode.Create);
            stream.Close();
            
            StreamWriter sw = new StreamWriter(Instance.EnviromentPath + Instance.DataFileName, true, Encoding.UTF8);

            foreach (string item in data)
            {
                string temp = AesOperation.EncryptString(Instance.MacAdress,item);
                sw.Write(temp + "\n");
            }
            sw.Close();
        }
        
        public static CurrentUser LoadData()
        {
            CurrentUser userToLoadIn = new CurrentUser {Email = "", Password = ""};
            
            string dataPath = Instance.EnviromentPath + Instance.DataFileName;
            
            if (File.Exists(dataPath))
            {
                StreamReader file = new StreamReader(dataPath);
                userToLoadIn.Email = AesOperation.DecryptString(Instance.MacAdress, file.ReadLine());
                userToLoadIn.Password = AesOperation.DecryptString(Instance.MacAdress, file.ReadLine());
                file.Close();
            }

            return userToLoadIn;
        }
    }
}