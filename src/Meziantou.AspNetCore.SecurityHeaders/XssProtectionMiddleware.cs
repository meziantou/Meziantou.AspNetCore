using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class XssProtectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<XssProtectionOptions> _options;

        public XssProtectionMiddleware(RequestDelegate next, IOptions<XssProtectionOptions> options)
        {
            _next = next;
            _options = options;
        }

        public Task Invoke(HttpContext context)
        {
            var options = _options.Value;
            if (options.Enabled)
            {
                string value = "1";
                if (options.Block)
                {
                    value += "; mode=block";
                }

                if (options.ReportUri != null)
                {
                    value += "; report=" + options.ReportUri;
                }

                context.Response.Headers["X-XSS-Protection"] = value;
            }

            return _next(context);
        }
    }
}
