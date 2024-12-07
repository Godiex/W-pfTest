using MediatR;
using Unity;

namespace Infrastructure.Extensions
{
    public static class ValidationPipelineRegistration
    {
        public static void RegisterValidationPipeline(IUnityContainer container)
        {
            container.RegisterType(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
