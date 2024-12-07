using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Infrastructure.Extensions
{
    public static class AutomaticRegistration
    {
        public static void RegisterRepositories(IUnityContainer container)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            IEnumerable<Type> repositoryTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes(typeof(RepositoryAttribute), false).Any());

            foreach (var type in repositoryTypes)
            {
                Type interfaceType = type.GetInterfaces().FirstOrDefault();
                if (interfaceType != null)
                {
                    container.RegisterType(interfaceType, type);
                }
            }
        }

        public static void RegisterDomainServices(IUnityContainer container)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            IEnumerable<Type> serviceTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes(typeof(DomainServiceAttribute), false).Any());

            foreach (var type in serviceTypes)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault();
                if (interfaceType != null)
                {
                    container.RegisterType(interfaceType, type);
                }
            }
        }
    }
}
