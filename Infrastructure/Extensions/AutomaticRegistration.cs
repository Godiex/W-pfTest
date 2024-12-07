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

            IEnumerable<Type> _repositories = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly =>
            {
                try
                {
                    return assembly.FullName != null && assembly.FullName.Contains("Infrastructure");
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
            .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(RepositoryAttribute)))
            .ToList();

            foreach (Type type in _repositories)
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

            IEnumerable<Type> _services = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly =>
            {
                try
                {
                    return assembly.FullName != null && assembly.FullName.IndexOf("Infrastructure", StringComparison.InvariantCulture) >= 0;
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
            .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(RepositoryAttribute)))
            .ToList();


            foreach (var type in _services)
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
