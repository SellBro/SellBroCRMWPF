using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SellBroCRMWPF.AES
{
    public class AesOperation  
    {  
        public static string EncryptString(string password, string input)  
        {  
            try
            {
                // Get the bytes of the strings
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                int iterations = 2444;

                byte[] bytesEncrypted = null;

                // Set your salt here the salt must be at least 8 bytes.
                byte[] saltBytes = { 95, 70, 46, 145, 68, 167, 230, 153, 138, 178, 195, 212, 31, 27, 189, 99, 74, 176, 166, 103, 132, 184, 75, 201, 107, 224, 15, 248, 228, 215 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (AesManaged AES = new AesManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, iterations);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }
                        bytesEncrypted = ms.ToArray();
                        return Convert.ToBase64String(bytesEncrypted); 
                    }
                }
            }
            catch (Exception ex )
            {
                return ex.Message;
            }
        }  
  
        public static string DecryptString(string password, string input)  
        {  
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                int iterations = 2444;

                byte[] bytesDecrypted = null;

                // Set your salt here the salt bytes must be at least 8 bytes.
                byte[] saltBytes = new byte[] { 95, 70, 46, 145, 68, 167, 230, 153, 138, 178, 195, 212, 31, 27, 189, 99, 74, 176, 166, 103, 132, 184, 75, 201, 107, 224, 15, 248, 228, 215 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (AesManaged AES = new AesManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, iterations);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        bytesDecrypted = ms.ToArray();
                    }
                }
                return Encoding.UTF8.GetString(bytesDecrypted);                   
            }
            catch (Exception ex)
            {
                return ex.Message;
            } 
        }  
    }  
}