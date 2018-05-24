using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class FrameOptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<FrameOptions> _options;

        public FrameOptionsMiddleware(RequestDelegate next, IOptions<FrameOptions> options)
        {
            _next = next;
            _options = options;
        }

        public Task Invoke(HttpContext context)
        {
            var options = _options.Value;
            string headerValue;
            switch (options.Value)
            {
                case FrameOptionsValue.Deny:
                    headerValue = "DENY";
                    break;
                case FrameOptionsValue.SameOrigin:
                    headerValue = "SAMEORIGIN";
                    break;
                case FrameOptionsValue.Allow:
                    headerValue = "ALLOW " + options.AllowedUri;
                    break;
            }

            context.Response.Headers["X-Frame-Options"] = headerValue;
            return _next(context);
        }
    }
}
