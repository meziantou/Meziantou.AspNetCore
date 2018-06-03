using System;
using System.Collections.Generic;

namespace Meziantou.AspNetCore.SecurityHeaders
{
    public class ContentSecurityPolicyCollectionBuilder
    {
        private const string Self = "'self'";
        private const string UnsafeInline = "'unsafe-inline'";
        private const string UnsafeEval = "'unsafe-eval'";
        private const string None = "'none'";
        private const string StrictDynamic = "'strict-dynamic'";

        public ICollection<string> Values { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public ContentSecurityPolicyCollectionBuilder AddSelf()
        {
            Values.Add(Self);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder AddUnsafeInline()
        {
            Values.Add(UnsafeInline);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder AddUnsafeEval()
        {
            Values.Add(UnsafeEval);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder AddNone()
        {
            Values.Add(None);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder AddStrictDynamic()
        {
            Values.Add(StrictDynamic);
            return this;
        }

        public ContentSecurityPolicyCollectionBuilder Add(string uri)
        {
            Values.Add(uri);
            return this;
        }
    }
}