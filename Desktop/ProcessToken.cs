using System.IO;
using System.Text;
using System.Windows;
using Newtonsoft.Json.Linq;
using SellBroCRMWPF.AES;
using SellBroCRMWPF.API;
using SellBroCRMWPF.Auth;

namespace SellBroCRMWPF.Desktop
{
    public static class ProcessToken
    {
        public static void ParseToken(string json)
        {
            JObject obj = JObject.Parse(json);
            string token = (string) obj.SelectToken("data.authToken");

            // Save token to current user
            AuthenticationUser.GetInstance().Token = token;
            Client.SetToken();

            token = AesOperation.EncryptString(Variables.MacAdress, token);

            FileStream stream = new FileStream(Variables.EnviromentPath + Variables.JwtFileName, FileMode.Create);
            stream.Close();

            StreamWriter sw = new StreamWriter(Variables.EnviromentPath + Variables.JwtFileName, true, Encoding.UTF8);
            sw.Write(token);
            sw.Close();
        }

        public static string ValidateToken()
        {
            string activeToken = "";
            string jwtPath = Variables.EnviromentPath + Variables.JwtFileName;
            if (File.Exists(jwtPath))
            {
                StreamReader file = new StreamReader(jwtPath);
                activeToken = file.ReadLine();
                activeToken = AesOperation.DecryptString(Variables.MacAdress, activeToken);
                file.Close();
            }

            return activeToken;
        }
    }
}