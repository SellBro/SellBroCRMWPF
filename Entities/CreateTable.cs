using System.Text.Json.Serialization;

namespace SellBroCRMWPF
{
    public class CreateTable
    {
        [JsonPropertyName("name")] 
        public string Name { get; set; }
    }
}