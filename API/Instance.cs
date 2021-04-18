namespace SellBroCRMWPF.API
{
    public abstract class Instance
    {
        private const string mediaType = "application/json";
        private static string baseURL = "https://sellbro-crm-api.herokuapp.com/";
        
        private static string login = $"{baseURL}login";
        private static string register = $"{baseURL}register";
        private static string trueJSON = $"{baseURL}truejson/truejson.jpg";
        private static string getTable = $"{baseURL}tables";

        private static string githubLink = "https://github.com/SellBro";

        public static string MediaType => mediaType;
        
        public static string BaseUrl => baseURL;
        public static string Login => login;
        public static string Register => register;

        public static string TrueJson => trueJSON;
        public static string GithubLink => githubLink;
        public static string GetTable => getTable;
    }
}