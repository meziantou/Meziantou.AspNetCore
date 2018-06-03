using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Meziantou.AspNetCore.Hsts
{
    [Obsolete("Use the impletation in ASP.NET Core")]
    public class HttpStrictTransportSecurityMiddleware
    {
        private const string HeaderName = "Strict-Transport-Security";

        private readonly RequestDelegate _next;
        private readonly HttpStrictTransportSecurityOptions _options;
        private readonly ILogger<HttpStrictTransportSecurityMiddleware> _logger;

        public HttpStrictTransportSecurityMiddleware(RequestDelegate next, HttpStrictTransportSecurityOptions options, ILogger<HttpStrictTransportSecurityMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.IsHttps)
            {
                _logger.LogDebug("HSTS response header is not set because the scheme is not https.");
                return _next(context);
            }

            if (!_options.EnableForLocalhost && IsLocalhost(context))
            {
                _logger.LogDebug("HSTS response header is disabled for localhost.");
                return _next(context);
            }

            var headerValue = GetHeaderValue();
            _logger.LogDebug("Adding HSTS response header: {HeaderValue}.", headerValue);
            context.Response.Headers[HeaderName] = headerValue;

            return _next(context);
        }

        private string GetHeaderValue()
        {
            // Strict-Transport-Security "max-age=31536000"
            // Strict-Transport-Security "max-age=31536000; includeSubDomains; preload"

            var headerValue = "max-age: " + (int)_options.MaxAge.TotalSeconds;
            if (_options.IncludeSubDomains)
            {
                headerValue += "; includeSubDomains";
            }

            if (_options.Preload)
            {
                headerValue += "; preload";
            }

            return headerValue;
        }

        private bool IsLocalhost(HttpContext context)
        {
            return string.Equals(context.Request.Host.Host, "localhost", StringComparison.OrdinalIgnoreCase);
        }
    }
}
