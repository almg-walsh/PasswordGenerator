namespace PasswordGenerator.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The UserAccount model.
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Key]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserAccount"/> is used.
        /// </summary>
        /// <value>
        ///   <c>true</c> if used; otherwise, <c>false</c>.
        /// </value>
        public bool ValidPassword { get; set; }
    }
}
