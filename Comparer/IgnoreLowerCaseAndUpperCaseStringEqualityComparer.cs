using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.Comparer;

/// <summary>
/// <example>
/// <code>
/// strs.Contains(
///     "a",
///     new IgnoreLowerCaseAndUpperCaseStringEqualityComparer()
/// )
/// </code>
/// </example>
/// </summary>
public class IgnoreLowerCaseAndUpperCaseStringEqualityComparer : IEqualityComparer<string>
{
    public bool Equals(string x, string y)
    {
        if (x.ToLower() == y.ToLower() || x.ToUpper() == y.ToUpper())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetHashCode(string obj)
    {
        return this.GetHashCode(obj.ToLower());
    }
}
