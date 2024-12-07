using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Infrastructure.Extensions
{
    public static class MediatorHandlerRegistration
    {
        public static void RegisterHandlers(IUnityContainer container)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            List<Type> handlerTypes = assemblies
                .SelectMany(a => a.GetTypes())
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
