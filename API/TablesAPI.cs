using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", AuthenticationUser.GetInstance().Token);
            var response = await client.GetAsync(Instance.GetTable + id);
            
            // TODO: Handle fail state
            //if (!response.IsSuccessStatusCode) return false;
            
            var res = response.Content.ReadAsStringAsync();

            if (!res.IsCompleted)
            {
                throw new Exception("Login Result exception");
            }
            Debug.WriteLine(res.Result);

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