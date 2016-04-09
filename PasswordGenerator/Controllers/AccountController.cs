namespace PasswordGenerator.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using PasswordGenerator.Models;

    public class AccountController : Controller
    {
        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Register(UserAccount userAccount)
        {
            if (userAccount != null)
            {
                using (UserDbContext db = new UserDbContext())
                {
                    var usr = db.userAccount.Where(u => u.UserId == userAccount.UserId).FirstOrDefault();
                    var account = new UserAccount();
                    account = usr;
                    return View(userAccount);
                }
            }

            return View();
            
        }

        /// <summary>
        /// Registers the specified account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(string userId)
        {
            if(ModelState.IsValid)
            {
                using (UserDbContext db = new UserDbContext())
                {
                    var usr = db.userAccount.Where(u => u.UserId == userId).FirstOrDefault();
                    var account = new UserAccount();
                    

                    if (usr == null)
                    {
                        account.UserId = userId;
                        account.Password = this.GeneratePassword(userId);
                        account.Date = DateTime.Now;
                        db.userAccount.Add(account);
                        db.SaveChanges();
                        ViewBag.Message = userId + " successfully registered. Please be aware that it will only be valid for 30 seconds.";
                        
                        return View(account);
                    }
                    else
                    {
                        account = usr;
                        ViewBag.Message = "Existing User";
                        return View(account);
                    }
                }
            }
            return View();
        }

        /// <summary>
        /// Validates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public ActionResult Validate(string userId, string password)
        {
            using (UserDbContext db = new UserDbContext())
            {
                var usr = db.userAccount.Where(u => u.UserId == userId).FirstOrDefault();
                var account = new UserAccount();
                account = usr;

                // We get the password from the Database and decrypt ready for comparison.
                var decryptedString = StringCipher.Decrypt(password);

                // We extrapolate the time from the stored password.
                DateTime storedPassTime = DateTime.Parse(decryptedString.Substring(decryptedString.Length - 8));

                // We extrapolate the difference between current time and stored time to check if more than 30 seconds have passed.
                var difference = DateTime.Now - storedPassTime;

                if (difference.TotalSeconds > 30)
                {
                    account.ValidPassword = false;
                    return View(account);
                }
                else
                {
                    account.ValidPassword = true;
                    ViewBag.Message = "valid";
                    return View(account);
                }
            }
        }

        /// <summary>
        /// Edits the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public ActionResult Edit(string userId)
        {
            var account = new UserAccount();
            using (UserDbContext db = new UserDbContext())
            {
                var usr = db.userAccount.Where(u => u.UserId == userId).FirstOrDefault();
                usr.Password = GeneratePassword(userId);
                db.SaveChanges();
                account = usr;
            }
            ViewBag.Message = "Success.";
            return View("Register", account);
        }

        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string GeneratePassword(string userId)
        {
            DateTime timeStamp = DateTime.Now;

            var password = userId + timeStamp.ToLongTimeString();

            string encryptedString = StringCipher.Encrypt(password);

            return encryptedString;
        }
    }
}