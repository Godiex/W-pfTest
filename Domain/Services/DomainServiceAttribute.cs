using System;

namespace Domain.Services
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DomainServiceAttribute : Attribute
    {
    }
}
