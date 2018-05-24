using System;

namespace Meziantou.AspNetCore.Hsts
{
    [Obsolete("Use the impletation in ASP.NET Core")]
    public class HttpStrictTransportSecurityOptions
    {
        public TimeSpan MaxAge { get; set; } = TimeSpan.FromDays(30);
        public bool IncludeSubDomains { get; set; } = true;
        public bool Preload { get; set; } = false;
        public bool EnableForLocalhost { get; set; } = false;
    }
}
