namespace PasswordGenerator.Controllers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Controllers;

    [TestClass]
    public class AccountControllerTests
    {
        private static string userId = "Aidan";

        AccountController controller = new AccountController();

        [TestMethod]
        public void GeneratePasswordTest()
        {
            string password = controller.GeneratePassword(userId);
            Assert.IsNotNull(password);
        }

        [TestMethod]
        public void ValidatePassword()
        {
            string password = controller.GeneratePassword(userId);

            bool valid = controller.ValidatePassword(userId, password);

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void ValidatePasswordWithWrongId()
        {
            string password = controller.GeneratePassword(userId);

            var newUserId = "Bob";

            bool valid = controller.ValidatePassword(newUserId, password);

            Assert.IsFalse(valid);
        }
    }
}
