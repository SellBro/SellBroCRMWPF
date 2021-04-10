using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellBroCRMWPF
{
    public class Table
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("fieldNames")] 
        public List<Field> Fields { get; set; }

        public Table()
        {
            Fields = new List<Field>();
        }
        
        public override string ToString()
        {
            string res = "";

            res += "Id - " + Id;
            res += "\nUserId - " + UserId;

            foreach (Field item in Fields)
            {
                res += "\nField - " + item.ToString();
            }

            return res;
        }
    }
}