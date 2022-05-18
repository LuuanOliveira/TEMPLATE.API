using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Template.Shared.Kernel.Util
{
    public class CryptographyUtil : ICryptographyUtil
    {
        public string EncryptRijndael(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var secretKey = "xzTuIU0LcxKjuZINT85jvCFbOjrQ9pqe";
            var key = Encoding.UTF8.GetBytes(secretKey);
            var iv = Encoding.UTF8.GetBytes(secretKey);

            byte[] encrypted;

            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            swEncrypt.Write(value);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public string DecryptRijndael(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var secretKey = "xzTuIU0LcxKjuZIN";
            var bytes = Convert.FromBase64String(value);
            var key = Encoding.UTF8.GetBytes(secretKey);
            var iv = Encoding.UTF8.GetBytes(secretKey);

            string plaintext = null;

            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (var msDecrypt = new MemoryStream(bytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }
    }
}
