using System;

namespace SellBroCRMWPF.API
{
    public abstract class Instance
    {
        private static string mediaType = "application/json";
        
        private static string baseURL = "http://localhost:8000";
        
        private static string login = "http://localhost:8000/login";
        private static string register = "http://localhost:8000/register";

        private static string enviromentPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SellBroCRM/";
        private static string macAdress;
        private static string jwtFileName = "jwt.dat";
        private static string dataFileName = "user.dat";

        public static string MediaType => mediaType;

        public static string BaseUrl => baseURL;

        public static string Login => login;
        public static string Register => register;

        public static string JwtFileName => jwtFileName;
        
        public static string DataFileName => dataFileName;
        
        public static string EnviromentPath => enviromentPath;

        public static string MacAdress
        {
            get => macAdress;
            set => macAdress = value;
        }

    }
}