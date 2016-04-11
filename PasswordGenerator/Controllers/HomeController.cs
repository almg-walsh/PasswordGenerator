namespace PasswordGenerator.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>The homepage.</returns>
        public ActionResult Index()
        {
            return this.View();
        }
    }
}