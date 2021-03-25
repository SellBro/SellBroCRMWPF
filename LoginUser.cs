using System.Text.Json.Serialization;

namespace SellBroCRMWPF
{
    public class LoginUser
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        
        
        public override string ToString()
        {
            return $"{Email} {Password}";
        }
    }
}