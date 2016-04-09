namespace PasswordGenerator
{
    using System;
    using System.Text;

    public static class Encryptor
    {
        public static string Encrypt(this string text)
        {
            return Convert.ToBase64String(
                    Encoding.Unicode.GetBytes(text));
        }

        public static string Decrypt(this string text)
        {
            return Encoding.Unicode.GetString(
                     Convert.FromBase64String(text));
        }
    }
}
