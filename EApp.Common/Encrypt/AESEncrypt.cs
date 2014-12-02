using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EApp.Common.Encrypt
{
    public class AESEncrypt
    {
        private string encryptKey;

        private ICryptoTransform transformEncryptAES;

        private ICryptoTransform transformDecryptAES;

        public AESEncrypt(string encryptKey) 
        {
            this.encryptKey = encryptKey;

            this.SetAESKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptKey"></param>
        private void SetAESKey()
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(this.encryptKey);

            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Key = keyArray;
            rijndaelManaged.Mode = CipherMode.ECB;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
         
            this.transformEncryptAES = rijndaelManaged.CreateEncryptor();
            this.transformDecryptAES = rijndaelManaged.CreateDecryptor();
        }

        /// <summary>
        /// Current encrypt key
        /// </summary>
        public string EncryptKey 
        {
            get 
            {
                return this.encryptKey;
            }
        }

        ///<summary>
        ///DES encrypt the string
        ///</summary>
        ///<param name="encryptString">string to encrypt</param>
        public string Encrypt(string encryptString)
        {
            try
            {
                if (encryptString == null)
                {
                    return null;
                }

                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(encryptString);

                byte[] resultArray = this.transformEncryptAES.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return encryptString;
            }
        }

        ///<summary>
        ///DES decrypt the string
        ///</summary>
        ///<param name="decryptString">string to decrypt</param>
        public string Decrypt(string decryptString)
        {
            try
            {
                if (decryptString == null)
                {
                    return null;
                }

                byte[] toEncryptArray = Convert.FromBase64String(decryptString);

                byte[] resultArray = this.transformDecryptAES.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return decryptString;
            }
        }

    }
}
