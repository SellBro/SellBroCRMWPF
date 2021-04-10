using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellBroCRMWPF
{
    public class Table
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("userId")]
        public int userId { get; set; }
        [JsonPropertyName("fieldNames")] 
        public List<Field> fields { get; set; }

        public Table()
        {
            fields = new List<Field>();
        }
        
        public override string ToString()
        {
            string res = "";

            res += "Id - " + Id;
            res += "\nUserId - " + userId;

            foreach (Field item in fields)
            {
                res += "\nItem - " + item.ToString();
            }

            return res;
        }
    }
}