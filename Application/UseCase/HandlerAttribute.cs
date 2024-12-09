using System;

namespace Application.UseCase
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class HandlerAttribute : Attribute
    {
    }
}
