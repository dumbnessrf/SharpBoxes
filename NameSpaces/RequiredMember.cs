using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
    public class RequiredMemberAttribute : Attribute { }

    public class CompilerFeatureRequiredAttribute : Attribute
    {
        public CompilerFeatureRequiredAttribute(string name) { }
    }

    [System.AttributeUsage(
        System.AttributeTargets.Constructor,
        AllowMultiple = false,
        Inherited = false
    )]
    public sealed class SetsRequiredMembersAttribute : Attribute { }
}
