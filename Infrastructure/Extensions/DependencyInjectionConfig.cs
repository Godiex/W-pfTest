using Infrastructure.Extensions;
using Unity;

namespace WpfApp
{
    public class DependencyInjectionConfig
    {
        public static IUnityContainer RegisterDependencies()
        {
            IUnityContainer container = new UnityContainer();
            ValidatorRegistration.RegisterValidators(container);
            MediatorHandlerRegistration.RegisterHandlers(container);
            ValidationPipelineRegistration.RegisterValidationPipeline(container);
            AutomaticRegistration.RegisterRepositories(container);
            AutomaticRegistration.RegisterDomainServices(container);
            return container;
        }
    }
}
