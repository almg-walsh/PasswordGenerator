namespace PasswordGenerator.Controllers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using PasswordGenerator.Migrations;
    using PasswordGenerator.Models;

    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IUserAccountDbContext _userAccountDbContext;

        public AccountController(IConfigurationProvider configurationProvider, IUserAccountDbContext userAccountDbContext)
        {
            _configurationProvider = configurationProvider;
            _userAccountDbContext = userAccountDbContext;
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <param name="userAccount">
        /// The user Account.
        /// </param>
        /// <returns>
        /// Return the Register view.
        /// </returns>
        public ActionResult Register()
        {
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
               
                    var usr = _userAccountDbContext.UserAccount.FirstOrDefault(u => u.UserId == userId);
                    var account = new UserAccount();
                    
                    if (usr == null)
                    {
                        account.UserId = userId;
                        account.Password = this.GeneratePassword(userId);
                        account.Date = DateTime.Now;
                        _userAccountDbContext.UserAccount.Add(account);
                        _userAccountDbContext.Commit();
                        ViewBag.Message = userId + " successfully registered. Please be aware that it will only be valid for 30 seconds.";
                        
                        return this.View(account);
                    }

                    account = usr;
                    this.ViewBag.Message = "Existing User";
                    return this.View(account);
                
            }

            return this.View();
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Validate()
        {
            return this.View();
        }

        /// <summary>
        /// Validates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>The view.</returns>
        [HttpPost]
        public ActionResult Validate(string userId, string password)
        {
                var usr = _userAccountDbContext.UserAccount.FirstOrDefault(u => u.UserId == userId);
                var account = usr;

                var valid = this.ValidatePassword(userId, password);

                if (valid)
                {
                    if (account != null)
                    {
                        account.ValidPassword = true;
                        ViewBag.Message = "Your Password is still valid.";
                        return this.View(account);
                    }
                }

                ViewBag.Message = "Your Password has expired, to generate a new one please click ";

                return this.View(account);
            
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
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
            {
                return false;
            }
                var usr = _userAccountDbContext.UserAccount.ToList().FirstOrDefault(u => string.Equals(u.UserId, userId, StringComparison.InvariantCultureIgnoreCase));
                var account = usr;

                if(usr != null)
                {
                    if (usr.Password != password)
                    {
                        account.ValidPassword = false;
                    }
                
                    // We get the password from the Database and decrypt ready for comparison.
                    var decryptedString = password.Decrypt();

                    // We extrapolate the time from the stored password.
                    var storedPassTime = DateTime.Parse(decryptedString.Substring(decryptedString.Length - 8));

                    bool validPassword = decryptedString.Contains(userId);

                    // We extrapolate the difference between current time and stored time to check if more than 30 seconds have passed.
                    var difference = DateTime.Now - storedPassTime;
                    if (validPassword)
                    {
                        return !(difference.TotalSeconds > 30);
                    }
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
           
                var usr = _userAccountDbContext.UserAccount.FirstOrDefault(u => u.UserId == userId);
                if (usr != null)
                {
                    usr.Password = this.GeneratePassword(userId);
                }

                _userAccountDbContext.Commit();
                account = usr;
            

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