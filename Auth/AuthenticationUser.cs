namespace SellBroCRMWPF.Auth
{
    public class AuthenticationUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool RememberMe { get; set; }

        public string[] SaveUser()
        {
            string[] data = {Email, Password};
            return data;
        }
    }
}