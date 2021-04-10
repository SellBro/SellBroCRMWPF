using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using SellBroCRMWPF.Auth;
using SellBroCRMWPF.Desktop;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace SellBroCRMWPF.API
{
    public class TablesAPI
    {
        public static async Task<Table> RequestTable(int id = 1)
        {
            Table table = new Table();
            
            Debug.WriteLine("Request");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            MessageBox.Show(AuthenticationUser.GetInstance().Token);
            client.DefaultRequestHeaders.Add("Authorization", AuthenticationUser.GetInstance().Token);
            var response = await client.GetAsync("https://sellbro-crm-api.herokuapp.com/tables/1");
            
            //if (!response.IsSuccessStatusCode) return false;
            
            var res = response.Content.ReadAsStringAsync();

            if (!res.IsCompleted)
            {
                throw new Exception("Login Result exception");
            }
            Debug.WriteLine(res.Result);

            MessageBox.Show(res.Result);

            return ParseTable(res.Result);
        }

        private static Table ParseTable(string json)
        {
            Table table = new Table();
            dynamic parsedTable = JsonConvert.DeserializeObject(json);

            table.Id = parsedTable["data"]["table"]["id"];
            table.userId = parsedTable["data"]["table"]["userId"];

            var fields = parsedTable["data"]["table"]["fieldNames"];

            foreach (var f in fields)
            {
                Field field = new Field();
                field.Id = f["id"];
                field.Type = f["type"];
                field.fieldType = f["fieldValues"];
                
                table.fields.Add(field);
            }

            return table;
        }
    }
}