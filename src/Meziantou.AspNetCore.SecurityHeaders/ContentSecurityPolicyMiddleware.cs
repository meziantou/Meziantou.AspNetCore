using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class ContentSecurityPolicyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<ContentSecurityPolicyOptions> _options;

        public ContentSecurityPolicyMiddleware(RequestDelegate next, IOptions<ContentSecurityPolicyOptions> options)
        {
            _next = next;
            _options = options;
        }

        public Task Invoke(HttpContext context)
        {
            var options = _options.Value;
            if (options.Value != null)
            {
                context.Response.Headers["Content-Security-Policy"] = options.Value;
            }

            return _next(context);
        }
    }
}