namespace SellBroCRMWPF.API
{
    public abstract class Instance
    {
        private const string mediaType = "application/json";
        private static string baseURL = "https://sellbro-crm-api.herokuapp.com";
        
        private static string login = $"{baseURL}/login";
        private static string register = $"{baseURL}/register";

        public static string MediaType => mediaType;
        
        public static string BaseUrl => baseURL;
        public static string Login => login;
        public static string Register => register;
    }
}