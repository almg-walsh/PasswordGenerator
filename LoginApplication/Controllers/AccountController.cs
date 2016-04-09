namespace LoginApplication.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using LoginApplication.Models;

    public class AccountController : Controller
    {
        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registers the specified account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if(ModelState.IsValid)
            {
                account.Password = this.GeneratePassword(account.UserId);
                account.Date = DateTime.Now;

                using (UserDbContext db = new UserDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.UserId + " successfully registered  Use this "+ account.Password +" to login. Please be aware that it will only be valid for 30 seconds.";
            }
            return View();
        }

        //login
        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Logins the specified user account.
        /// </summary>
        /// <param name="userAccount">The user account.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserAccount userAccount)
        {
            using (UserDbContext db = new UserDbContext())
            {
                var usr = db.userAccount.Where(u => u.UserId == userAccount.UserId && u.Password == userAccount.Password).FirstOrDefault();
               
                // We get the password from the Database and decrypt ready for comparison.
                var decryptedString = StringCipher.Decrypt(usr.Password);

                // We extrapolate the time from the stored password.
                DateTime storedPassTime = DateTime.Parse(decryptedString.Substring(decryptedString.Length - 8));

                // We extrapolate the difference between current time and stored time to check if more than 30 seconds have passed.
                var difference = DateTime.Now - storedPassTime;

                if (usr.Used)
                {
                    Session["UserId"] = userAccount.UserId.ToString();
                    return View("LoggedIn", userAccount);
                }

                else if (difference.TotalSeconds > 30)
                {
                    ViewBag.Message = "Your password is no longer valid";
                }
                else if (userAccount != null)
                {
                    usr.Used = true;
                    db.SaveChanges();

                    Session["UserId"] = userAccount.UserId.ToString();
                    return View("LoggedIn", userAccount);
                }
            }

            return View(userAccount);
        }

        /// <summary>
        /// Loggeds the in.
        /// </summary>
        /// <returns></returns>
        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        /// <summary>
        /// Edits the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public ActionResult Edit(UserAccount user)
        {
            using (UserDbContext db = new UserDbContext())
            {
                user.Password = GeneratePassword(user.UserId);
                db.userAccount.Attach(user);
                db.Entry(user).Property(u => u.Password).IsModified = true;
                db.SaveChanges();
            }
            return View(user);
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