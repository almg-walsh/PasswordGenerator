namespace PasswordGenerator
{
    using System;
    using System.Text;

    /// <summary>
    /// The encryptor.
    /// </summary>
    public static class Encryptor
    {
        /// <summary>
        /// The encrypt.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Encrypt(this string text)
        {
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(text));
        }

        /// <summary>
        /// The decrypt.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Decrypt(this string text)
        {
            return Encoding.Unicode.GetString(Convert.FromBase64String(text));
        }
    }
}
