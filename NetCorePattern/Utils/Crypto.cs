using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace NetCorePattern.Utils
{
    public static class Crypto
    {
        public static string Decrypt(string cipher, string hashKey, string ivKey)
        {
            byte[] cipherBytes = Encoding.UTF8.GetBytes(cipher);
            byte[] key = Encoding.UTF8.GetBytes(hashKey);
            byte[] iv = Encoding.UTF8.GetBytes(ivKey);
            byte[] decryptedBytes;

            // Set up the encryption objects
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.Zeros;

                // Decrypt the input ciphertext using the AES algorithm
                using ICryptoTransform decryptor = aes.CreateDecryptor();
                decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            }

            string str1 = Encoding.Default.GetString(decryptedBytes);
            string str2 = Encoding.UTF8.GetString(decryptedBytes);


            return str2;
        }

        //public static string Decrypt(string combinedString, string hashKey, string ivKey)
        //{
        //    string plainText;
        //    byte[] combinedData = Convert.FromBase64String(combinedString);
        //    Aes aes = Aes.Create();
        //    aes.Key = Encoding.UTF8.GetBytes(hashKey);
        //    byte[] iv = new byte[aes.BlockSize / 8];
        //    byte[] cipherText = new byte[combinedData.Length - iv.Length];
        //    Array.Copy(combinedData, iv, iv.Length);
        //    Array.Copy(combinedData, iv.Length, cipherText, 0, cipherText.Length);
        //    aes.IV = iv;
        //    aes.Mode = CipherMode.CBC;
        //    aes.Padding = PaddingMode.Zeros;

        //    ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);

        //    using (MemoryStream ms = new MemoryStream(cipherText))
        //    {
        //        using (CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read))
        //        {
        //            using (StreamReader sr = new StreamReader(cs))
        //            {
        //                plainText = sr.ReadToEnd();
        //                plainText = UnicodeToUTF8(plainText);
        //            }
        //        }

        //        return plainText;
        //    }
        //}

        public static string UnicodeToUTF8(string from)
        {
            var bytes = Encoding.UTF8.GetBytes(from);
            return new string(bytes.Select(b => (char)b).ToArray());
        }
    }
}
