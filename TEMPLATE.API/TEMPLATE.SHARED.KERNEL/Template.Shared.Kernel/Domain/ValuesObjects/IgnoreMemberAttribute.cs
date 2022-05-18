using System;

namespace Template.Shared.Kernel.Domain.ValuesObjects
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}
