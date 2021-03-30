using System;

namespace SellBroCRMWPF.Desktop
{
    public static class Variables
    {
        private static string enviromentPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SellBroCRM/";
        private static string macAdress;
        private static string jwtFileName = "jwt.dat";
        private static string dataFileName = "user.dat";
        
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