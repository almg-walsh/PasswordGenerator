namespace PasswordGenerator.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using PasswordGenerator.Migrations;
    using PasswordGenerator.Models;

    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <param name="userAccount">
        /// The user Account.
        /// </param>
        /// <returns>
        /// Return the Register view.
        /// </returns>
        public ActionResult Register(UserAccount userAccount)
        {
            if (userAccount != null)
            {
                using (var db = new UserDbContext())
                {
                    var usr = db.UserAccount.FirstOrDefault(u => u.UserId == userAccount.UserId);
                    userAccount = usr;
                    return this.View(userAccount);
                }
            }

            return this.View();
        }

        /// <summary>
        /// Registers the specified account.
        /// </summary>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <returns>
        /// The register view.
        /// </returns>
        [HttpPost]
        public ActionResult Register(string userId)
        {
            if (ModelState.IsValid)
            {
                using (var db = new UserDbContext())
                {
                    var usr = db.UserAccount.FirstOrDefault(u => u.UserId == userId);
                    var account = new UserAccount();
                    
                    if (usr == null)
                    {
                        account.UserId = userId;
                        account.Password = this.GeneratePassword(userId);
                        account.Date = DateTime.Now;
                        db.UserAccount.Add(account);
                        db.SaveChanges();
                        ViewBag.Message = userId + " successfully registered. Please be aware that it will only be valid for 30 seconds.";
                        
                        return this.View(account);
                    }

                    account = usr;
                    this.ViewBag.Message = "Existing User";
                    return this.View(account);
                }
            }

            return this.View();
        }

        /// <summary>
        /// Validates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>The view.</returns>
        public ActionResult Validate(string userId, string password)
        {
            using (var db = new UserDbContext())
            {
                var usr = db.UserAccount.FirstOrDefault(u => u.UserId == userId);
                var account = usr;

                var valid = this.ValidatePassword(userId, password);

                if (valid)
                {
                    if (account != null)
                    {
                        account.ValidPassword = true;
                        return this.View(account);
                    }
                }

                return this.View(account);
            }
        }

        /// <summary>
        /// The validate password.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ValidatePassword(string userId, string password)
        {
            // We get the password from the Database and decrypt ready for comparison.
            var decryptedString = Encryptor.Decrypt(password);

            // We extrapolate the time from the stored password.
            var storedPassTime = DateTime.Parse(decryptedString.Substring(decryptedString.Length - 8));

            bool validPassword = decryptedString.Contains(userId);

            // We extrapolate the difference between current time and stored time to check if more than 30 seconds have passed.
            var difference = DateTime.Now - storedPassTime;
            if (validPassword)
            {
                return !(difference.TotalSeconds > 30);
            }

            return false;
        }

        /// <summary>
        /// Edits the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>the register view on success.</returns>
        public ActionResult Edit(string userId)
        {
            UserAccount account;
            using (var db = new UserDbContext())
            {
                var usr = db.UserAccount.FirstOrDefault(u => u.UserId == userId);
                if (usr != null)
                {
                    usr.Password = this.GeneratePassword(userId);
                }

                db.SaveChanges();
                account = usr;
            }

            ViewBag.Message = "Success.";
            return this.View("Register", account);
        }

        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The encrypted string.</returns>
        public string GeneratePassword(string userId)
        {
            var timeStamp = DateTime.Now;

            var password = userId + timeStamp.ToLongTimeString();

            string encryptedString = password.Encrypt();

            return encryptedString;
        }
    }
}