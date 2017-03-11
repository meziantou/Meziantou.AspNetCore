using Microsoft.AspNetCore.Rewrite;
using System.Net;

namespace Meziantou.AspNetCore.Rewrite.Rules
{
    public static class Extensions
    {
        public static RewriteOptions AddRedirectToWww(this RewriteOptions rewriteOptions)
        {
            RedirectWwwRule redirectWwwRule = new RedirectWwwRule() { StatusCode = (int)HttpStatusCode.Redirect };
            return rewriteOptions.Add(redirectWwwRule);
        }

        public static RewriteOptions AddRedirectToWwwPermanent(this RewriteOptions rewriteOptions)
        {
            RedirectWwwRule redirectWwwRule = new RedirectWwwRule() { StatusCode = (int)HttpStatusCode.MovedPermanently };
            return rewriteOptions.Add(redirectWwwRule);
        }
    }
}
