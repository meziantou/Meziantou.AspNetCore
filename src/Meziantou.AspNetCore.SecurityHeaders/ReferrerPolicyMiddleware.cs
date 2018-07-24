using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class ReferrerPolicyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<ReferrerPolicyOptions> _options;

        public ReferrerPolicyMiddleware(RequestDelegate next, IOptions<ReferrerPolicyOptions> options)
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
                case ReferrerPolicy.NoReferrer:
                    headerValue = "no-referrer";
                    break;
                case ReferrerPolicy.NoReferrerWhenDowngrade:
                    headerValue = "no-referrer-when-downgrade";
                    break;
                case ReferrerPolicy.Origin:
                    headerValue = "origin";
                    break;
                case ReferrerPolicy.OriginWhenCrossOrigin:
                    headerValue = "origin-when-cross-origin";
                    break;
                case ReferrerPolicy.SameOrigin:
                    headerValue = "same-origin";
                    break;
                case ReferrerPolicy.StrictOrigin:
                    headerValue = "strict-origin";
                    break;
                case ReferrerPolicy.StrictOriginWhenCrossOrigin:
                    headerValue = "strict-origin-when-cross-origin";
                    break;
                case ReferrerPolicy.UnsafeUrl:
                    headerValue = "unsafe-url";
                    break;

                default:
                    return _next(context);
            }

            context.Response.Headers["Referrer-Policy"] = headerValue;
            return _next(context);
        }
    }
}
