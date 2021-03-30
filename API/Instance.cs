namespace SellBroCRMWPF.API
{
    public abstract class Instance
    {
        private static string mediaType = "application/json";
        
        private static string baseURL = "http://localhost:8000";
        
        private static string login = "http://localhost:8000/login";
        private static string register = "http://localhost:8000/register";

        public static string MediaType => mediaType;

        public static string BaseUrl => baseURL;

        public static string Login => login;
        public static string Register => register;
    }
}