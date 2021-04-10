using System.Text.Json.Serialization;

namespace SellBroCRMWPF
{
    public class Field
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("fieldValues")] 
        public dynamic fieldType { get; set; }

        public override string ToString()
        {
            string res = "";
            res += Type + " " + fieldType + ";";
            return res;
        }
    }
}