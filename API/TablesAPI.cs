using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using SellBroCRMWPF.Auth;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SellBroCRMWPF.API
{
    public class TablesAPI
    {
        public static async Task<bool> RequestAllTables()
        {
            var responce = await Client.GetInstance().GetAsync(Instance.GetTable);

            if (!responce.IsSuccessStatusCode) return false;

            var res = responce.Content.ReadAsStringAsync();
            
            await ParseAllTables(res.Result);

            return true;
        }
        
        public static async Task<Table> RequestTable(int id = 1)
        {
            var response = await Client.GetInstance().GetAsync(Instance.GetTable + "/" + id);
            
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
            
            var response = await Client.GetInstance().PostAsync(Instance.GetTable, tableName);
            
            // TODO: Handle fail state
            if (!response.IsSuccessStatusCode) return null;
            
            var res = response.Content.ReadAsStringAsync();

            return ParseTable(res.Result); 
        }

        private static Task ParseAllTables(string json)
        {
            dynamic parsedTables = JsonConvert.DeserializeObject(json);

            var tables = parsedTables["data"]["tables"];

            foreach (var table in tables)
            {
                Table t = new Table();
                t.Id = table["id"];
                t.Name = table["name"];
                t.UserId = table["userId"];
                AuthenticationUser.GetInstance().Tables.Add(t);
            }

            return Task.CompletedTask;
        }

        private static Table ParseTable(string json)
        {
            dynamic parsedTable = JsonConvert.DeserializeObject(json);

            int id = parsedTable["data"]["table"]["id"];
            
            Table table = AuthenticationUser.GetInstance().Tables.Find(x => x.Id == id);
            
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