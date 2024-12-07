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

            List<Type> validatorTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableToGenericType(typeof(IValidator<>)))
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                var interfaceType = validatorType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));

                container.RegisterType(interfaceType, validatorType);
            }
        }
    }
}
