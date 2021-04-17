using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellBroCRMWPF
{
    public class Field
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")] 
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("fieldValues")] 
        public List<Item> Items { get; set; }

        public Field()
        {
            Items = new List<Item>();
        }
        
        public override string ToString()
        {
            string res = "";
            res += Type;
            
            foreach (Item item in Items)
            {
                res += "\nItem - " + item.ToString();
            }
            
            return res;
        }
    }
}