using System;
using System.Collections.Generic;

namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class ContentSecurityPolicyCollectionBuilder
    {
        public ICollection<string> Values { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public ContentSecurityPolicyCollectionBuilder AddSelf()
        {
            Values.Add(ContentSecurityPolicyBuilder.Self);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder AddUnsafeInline()
        {
            Values.Add(ContentSecurityPolicyBuilder.UnsafeInline);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder AddUnsafeEval()
        {
            Values.Add(ContentSecurityPolicyBuilder.UnsafeEval);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder AddNone()
        {
            Values.Add(ContentSecurityPolicyBuilder.None);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder AddStrictDynamic()
        {
            Values.Add(ContentSecurityPolicyBuilder.StrictDynamic);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder Add(string uri)
        {
            Values.Add(uri);
            return this;
        }
    }
}