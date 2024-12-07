using Infrastructure.Data;
using Infrastructure.Extensions;
using Unity;
using Unity.Injection;
using System.Configuration;

namespace WpfApp
{
    public class DependencyInjectionConfig
    {
        public static IUnityContainer RegisterDependencies()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            IUnityContainer container = new UnityContainer();
            container.RegisterType<AppDbContext>();
            container.RegisterType<IDbConnectionFactory, SqlConnectionFactory>(new InjectionConstructor(connectionString));
            ValidatorRegistration.RegisterValidators(container);
            MediatorHandlerRegistration.RegisterHandlers(container);
            ValidationPipelineRegistration.RegisterValidationPipeline(container);
            AutomaticRegistration.RegisterRepositories(container);
            AutomaticRegistration.RegisterDomainServices(container);
            return container;
        }
    }
}
