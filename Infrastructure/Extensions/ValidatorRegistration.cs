using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Infrastructure.Extensions
{
    public static class ValidatorRegistration
    {
        public static void RegisterValidators(IUnityContainer container)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();



            List<Type> validatorTypes = AppDomain.CurrentDomain.GetAssemblies()
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
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
            .ToList();


            foreach (Type validatorType in validatorTypes)
            {
                Type interfaceType = validatorType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));

                container.RegisterType(interfaceType, validatorType);
            }
        }
    }
}
