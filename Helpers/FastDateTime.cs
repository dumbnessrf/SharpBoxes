using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.Helpers;

public static class FastDateTime
{
    static TimeSpan LocalUtcOffset;
    public static DateTime Now
    {
        get { return DateTime.UtcNow + LocalUtcOffset; }
    }

    static FastDateTime()
    {
        LocalUtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
        var a = FastDateTime.Now;
    }
}
