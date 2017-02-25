using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;
using System;
using System.Net;

namespace Meziantou.AspNetCore.Rewrite.Rules
{
    public class RedirectWwwRule: Microsoft.AspNetCore.Rewrite.IRule
    {
        public int StatusCode { get; } = (int)HttpStatusCode.MovedPermanently;
        public bool ExcludeLocalhost { get; set; } = true;

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            var host = request.Host;
            if (host.Host.StartsWith("www", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            if (ExcludeLocalhost && string.Equals(host.Host, "localhost", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            string newPath = request.Scheme + "://www." + host.Value + request.PathBase + request.Path + request.QueryString;

            var response = context.HttpContext.Response;
            response.StatusCode = StatusCode;
            response.Headers[HeaderNames.Location] = newPath;
            context.Result = RuleResult.EndResponse; // Do not continue processing the request        
        }
    }
}
