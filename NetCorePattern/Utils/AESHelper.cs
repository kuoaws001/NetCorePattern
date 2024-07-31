using System.Security.Cryptography;
using System.Text;

namespace NetCorePattern.Utils
{
    public class AESHelper
    {
        private Aes _aes;
        private ICryptoTransform _crypto;

        public AESHelper(string key, string IV)
        {
            _aes = Aes.Create();
            _aes.BlockSize = 128;
            _aes.KeySize = 256;
            _aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
            if (!string.IsNullOrEmpty(IV))
            {
                _aes.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            }

            _aes.Padding = PaddingMode.Zeros;
            _aes.Mode = CipherMode.CBC;
        }

        public string encrypt(string message)
        {
            _crypto = _aes.CreateEncryptor(_aes.Key, _aes.IV);
            byte[] encrypted = _crypto.TransformFinalBlock(
                ASCIIEncoding.ASCII.GetBytes(message), 0, ASCIIEncoding.ASCII.GetBytes(message).Length);
            _crypto.Dispose();
            return System.Convert.ToBase64String(encrypted);
        }

        public string decrypt(string message)
        {
            _crypto = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            byte[] decrypted = _crypto.TransformFinalBlock(System.Convert.FromBase64String(message), 0, System.Convert.FromBase64String(message).Length);
            _crypto.Dispose();
            return ASCIIEncoding.ASCII.GetString(decrypted);
        }
    }
}
