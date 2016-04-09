using LoginApplication.Controllers;
using System.Web.Mvc;

namespace LoginApplication
{
    [TestMethod]
    public class LoginApplicationTests
    {
        [Test]
        public void TestDetailsView()
        {
            var controller = new AccountController();
            var result = controller.Login() as ViewResult;
            Assert.AreEqual("Login", result.ViewName);

        }
    }
}
