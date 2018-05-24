using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class ContentTypeOptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<ContentTypeOptions> _options;

        public ContentTypeOptionsMiddleware(RequestDelegate next, IOptions<ContentTypeOptions> options)
        {
            _next = next;
            _options = options;
        }

        public Task Invoke(HttpContext context)
        {
            var options = _options.Value;
            if (options.Enabled)
            {
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            }

            return _next(context);
        }
    }
}
