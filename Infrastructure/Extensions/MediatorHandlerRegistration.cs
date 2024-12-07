using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Infrastructure.Extensions
{
    public static class MediatorHandlerRegistration
    {
        public static void RegisterHandlers(IUnityContainer container)
        {
            container.RegisterType<IMediator, Mediator>();

            container.RegisterType<ServiceFactory>(new InjectionFactory(c =>
            {
                return new ServiceFactory(type => c.Resolve(type));
            }));

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            List<Type> handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly =>
            {
                try
                {
                    return assembly.FullName != null && assembly.FullName.IndexOf("Application", StringComparison.InvariantCulture) >= 0;
                }
                catch
                {
                    return false;
                }
            })
            .SelectMany(assembly =>
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    return ex.Types.Where(t => t != null);
                }
                catch
                {
                    return Enumerable.Empty<Type>();
                }
            })
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType &&
                (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                 i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))))
            .ToList();


            foreach (Type handlerType in handlerTypes)
            {
                Type interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType &&
                        (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                         i.GetGenericTypeDefinition() == typeof(IRequestHandler<>)));

                container.RegisterType(interfaceType, handlerType);
            }
        }
    }
}
