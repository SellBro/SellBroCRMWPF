using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using SellBroCRMWPF.Auth;

namespace SellBroCRMWPF.API
{
    public class TablesAPI
    {
        public static async Task<Table> RequestTable(int id = 1)
        {
            Table table = new Table();
            
            Debug.WriteLine("Request");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Instance.MediaType));
            client.DefaultRequestHeaders.Add("Authorization", AuthenticationUser.GetInstance().Token);
            var response = await client.GetAsync(Instance.GetTable + "/" + id);
            
            // TODO: Handle fail state
            if (!response.IsSuccessStatusCode) return null;
            
            var res = response.Content.ReadAsStringAsync();

            
            return ParseTable(res.Result);
        }

        public static async Task<Table> CreateTable(string name = "Table")
        {
            CreateTable t = new CreateTable();
            t.Name = name;
            string body = JsonSerializer.Serialize(t);
            var tableName = new StringContent(body, Encoding.UTF8, Instance.MediaType);
            
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", AuthenticationUser.GetInstance().Token);
            var response = await client.PostAsync(Instance.GetTable, tableName);
            
            // TODO: Handle fail state
            if (!response.IsSuccessStatusCode) return null;
            
            var res = response.Content.ReadAsStringAsync();

            MessageBox.Show(res.Result);
            
            return ParseTable(res.Result); 
        }
        
        // TODO: Get All Tables

        private static Table ParseTable(string json)
        {
            Table table = new Table();
            dynamic parsedTable = JsonConvert.DeserializeObject(json);

            table.Id = parsedTable["data"]["table"]["id"];
            table.UserId = parsedTable["data"]["table"]["userId"];

            var fields = parsedTable["data"]["table"]["fieldNames"];

            foreach (var f in fields)
            {
                Field field = new Field();
                field.Id = f["id"];
                field.Type = f["type"];

                var items = f["fieldValues"];

                foreach (var i in items)
                {
                    Item item = new Item();
                    item.Id = i["id"];
                    item.Value = i["value"];
                    
                    field.Items.Add(item);
                }
                
                table.Fields.Add(field);
            }

            return table;
        }
    }
}