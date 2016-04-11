namespace PasswordGenerator.Controllers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PasswordGenerator.Controllers;

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

        /// <summary>
        /// The controller
        /// </summary>
        public readonly AccountController Controller = new AccountController();

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
            string password = this.Controller.GeneratePassword(UserId);

            bool valid = this.Controller.ValidatePassword(UserId, password);

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
    }
}
