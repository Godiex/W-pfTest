using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.UseCase;
using Domain.Services;
using Unity;

namespace Infrastructure.Extensions
{
    public static class AutomaticRegistration
    {
        public static void RegisterRepositories(IUnityContainer container)
        {
            IEnumerable<Type> repositories = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly =>
            {
                try
                {
                    return assembly.FullName.Contains("Infrastructure");
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

            foreach (Type type in repositories)
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
            IEnumerable<Type> services = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly =>
            {
                try
                {
                    return assembly.FullName.IndexOf("Domain", StringComparison.InvariantCulture) >= 0;
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
            .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)))
            .ToList();


            foreach (Type type in services)
            {
                Type interfaceType = type.GetInterfaces().FirstOrDefault();
                if (interfaceType != null)
                {
                    container.RegisterType(interfaceType, type);
                }
            }
        }
        
        public static void RegisterHandlers(IUnityContainer container)
        {
            IEnumerable<Type> services = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly =>
                {
                    try
                    {
                        return assembly.FullName.IndexOf("Application", StringComparison.InvariantCulture) >= 0;
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
                .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(HandlerAttribute)))
                .ToList();


            foreach (Type type in services)
            {
                Type interfaceType = type.GetInterfaces().FirstOrDefault();
                if (interfaceType != null)
                {
                    container.RegisterType(interfaceType, type);
                }
            }
        }
        
        public static void RegisterViewModel(IUnityContainer container)
        {
            IEnumerable<Type> services = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly =>
                {
                    try
                    {
                        return assembly.FullName.IndexOf("WpfApp", StringComparison.InvariantCulture) >= 0;
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
                .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(ViewModelAttribute)))
                .ToList();


            foreach (Type type in services)
            {
                Type interfaceType = type.GetInterfaces().FirstOrDefault();
                if (interfaceType != null)
                {
                    container.RegisterType(interfaceType, type);
                }
            }
        }
    }
}
