namespace PasswordGenerator
{
    using PasswordGenerator.Controllers;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Mvc;
    using System.Web.Mvc;
    using Models;
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IController, HomeController>("Home");
            container.RegisterType<IController, AccountController>("Account");

            container.RegisterType<IConfigurationProvider, ConfigurationProvider>();
            container.RegisterType<IUserAccountDbContext, UserAccountDbContext>();

            return container;
        }
    }
}
