using System.Text.Json.Serialization;
using SellBroCRMWPF.Auth;

namespace SellBroCRMWPF
{
    public class LoginUser
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }


        public LoginUser(AuthenticationUser user)
        {
            Email = user.Email;
            Password = user.Password;
        }
        
        public override string ToString()
        {
            return $"{Email} {Password}";
        }
    }
}