using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordGenerator.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PasswordGenerator.Controllers.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        private static string userId = "Aidan";

        [TestMethod]
        public void GeneratePasswordTest()
        {
            AccountController controller = new AccountController();

            string password = controller.GeneratePassword(userId);

            Assert.IsNotNull(password);
        }
    }
}
