using System;
using System.Configuration;
using System.Windows.Controls;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Unity;
using Unity.Injection;

namespace WpfApp
{
    public static class DependencyInjectionConfig
    {
        public static IUnityContainer RegisterDependencies()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            IUnityContainer container = new UnityContainer();
            container.RegisterType<AppDbContext>();
            container.RegisterType<IDbConnectionFactory, SqlConnectionFactory>(new InjectionConstructor(connectionString));
            AutomaticRegistration.RegisterRepositories(container);
            AutomaticRegistration.RegisterDomainServices(container);
            AutomaticRegistration.RegisterHandlers(container);
            AutomaticRegistration.RegisterViewModel(container);
            return container;
        }
    }
}
