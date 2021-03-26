using System.Text.Json.Serialization;

namespace SellBroCRMWPF
{
    public class TokenClass
    {
        [JsonPropertyName("authToken")]
        public string Token { get; set; }
    }
}