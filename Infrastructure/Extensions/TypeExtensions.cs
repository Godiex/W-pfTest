using System;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType == null || genericType == null)
            {
                return false;
            }

            return givenType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType) ||
                   (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType) ||
                   (givenType.BaseType != null && givenType.BaseType.IsAssignableToGenericType(genericType));
        }
    }
}
