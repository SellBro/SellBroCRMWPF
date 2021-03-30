using System.IO;
using System.Text;
using SellBroCRMWPF;
using SellBroCRMWPF.AES;
using SellBroCRMWPF.Desktop;

namespace SellbroCRMWPF.Desktop
{
    public static class ProcessData
    {
        
        public static void SaveData(string[] data)
        {
            FileStream stream = new FileStream(Variables.EnviromentPath + Variables.DataFileName, FileMode.Create);
            stream.Close();
            
            StreamWriter sw = new StreamWriter(Variables.EnviromentPath + Variables.DataFileName, true, Encoding.UTF8);

            foreach (string item in data)
            {
                string temp = AesOperation.EncryptString(Variables.MacAdress,item);
                sw.Write(temp + "\n");
            }
            sw.Close();
        }
        
        public static CurrentUser LoadData()
        {
            CurrentUser userToLoadIn = new CurrentUser {Email = "", Password = ""};
            
            string dataPath = Variables.EnviromentPath + Variables.DataFileName;
            
            if (File.Exists(dataPath))
            {
                StreamReader file = new StreamReader(dataPath);
                userToLoadIn.Email = AesOperation.DecryptString(Variables.MacAdress, file.ReadLine());
                userToLoadIn.Password = AesOperation.DecryptString(Variables.MacAdress, file.ReadLine());
                file.Close();
            }

            return userToLoadIn;
        }    
    }
}