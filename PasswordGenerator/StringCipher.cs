using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace PasswordGenerator
{
    public static class StringCipher
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
