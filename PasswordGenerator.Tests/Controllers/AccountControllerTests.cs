namespace PasswordGenerator.Controllers.Tests
{
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;
    using PasswordGenerator.Controllers;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// The account controller tests.
    /// </summary>
    [TestClass]
    public class AccountControllerTests
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        public const string UserId = "Aidan";
        public static IUserAccountDbContext userAccountDbContext;
        public static IConfigurationProvider configurationProvider;
        
        /// <summary>
        /// The controller
        /// </summary>
        public readonly AccountController Controller = new AccountController(configurationProvider, userAccountDbContext);
       
        /// <summary>
        /// Generates the password test.
        /// </summary>
        [TestMethod]
        public void GeneratePasswordTest()
        {
            string password = this.Controller.GeneratePassword(UserId);
            Assert.IsNotNull(password);
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        [TestMethod]
        public void ValidatePassword()
        {
            configurationProvider = new ConfigurationProvider();

            var userAcccountContext = new UserAccountDbContext(configurationProvider);

            var accountController = new AccountController(configurationProvider, userAcccountContext);

            string password = Controller.GeneratePassword(UserId);

            bool valid = Controller.ValidatePassword(UserId, password);

            Assert.IsTrue(valid);
        }

        /// <summary>
        /// Validates the password with wrong identifier.
        /// </summary>
        [TestMethod]
        public void ValidatePasswordWithWrongId()
        {
            string password = this.Controller.GeneratePassword(UserId);

            const string NewUserId = "Bob";

            bool valid = this.Controller.ValidatePassword(NewUserId, password);

            Assert.IsFalse(valid);
        }

        /// <summary>
        /// The validate wrong password.
        /// </summary>
        [TestMethod]
        public void ValidateWrongPassword()
        {
            const string password = "asd35wer5we#'we#';fds==";

            bool valid = this.Controller.ValidatePassword("Aidan", password);

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void ValidateWrongPasswordAndNoUserId()
        {
            const string password = "asd35wer5we#'we#';fds==";

            bool valid = this.Controller.ValidatePassword(null, password);

            Assert.IsFalse(valid);
        }
    }
}
