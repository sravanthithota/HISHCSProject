using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
   public class Common
    {
        public static string ConStr { get; set; }
        public static string ExelConStr{ get; set; }


        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ProperMS");
        public static void WriteInfoLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Applog.txt"))
                {
                    using (File.Create(AppDomain.CurrentDomain.BaseDirectory + "\\Applog.txt"))
                    {
                        sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Applog.txt", true);
                        sw.WriteLine(DateTime.Now.ToString() + ":" + message);
                        sw.Flush();
                        sw.Close();
                    }
                }
                else
                {
                    sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Applog.txt", true);
                    sw.WriteLine(DateTime.Now.ToString() + ":" + message);
                    sw.Flush();
                    sw.Close();
                }

            }
            catch (Exception ex)
            { throw ex; }
        }

        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();

            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        /// <summary>
        /// Decrypt a crypted string.
        /// </summary>
        /// <param name="cryptedString">The crypted string.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown 
        /// when the crypted string is null or empty.</exception>
        public static string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();


        }

        public static object GetSystemMessage(int Id)
        {
            try
            {


                return SqlHelper.ExecuteScalar(Common.ConStr, "USP_GetSystemMessage", Id);

            }

            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - Common.GetSystemMessage" + " , SPName : pr_Validate_User ,Exception:" + ex.Message.ToString());

                return null;
            }
        }
    }
}
