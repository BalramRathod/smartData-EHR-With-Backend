using System.Text;

namespace EHR_project.encryption
{
    public class EncryptDecrypt
    {

        public static string key = "@786sanketw";

        public static string Encrypt(string String)
        {
            if (String == null)
            {
                return "";

            }
            else
            {
                String = String + key;
                var StringinBytes = Encoding.UTF8.GetBytes(String);
                return Convert.ToBase64String(StringinBytes);

            }
        }

        public static string Decrypt(string String)
        {
            if (String == null)
            {
                return "";

            }
            else
            {

                var StringinBytes = Convert.FromBase64String(String);
                var actualString = Encoding.UTF8.GetString(StringinBytes);
                actualString = actualString.Substring(0, actualString.Length - key.Length);

                return actualString;

            }
        }
    }
}

