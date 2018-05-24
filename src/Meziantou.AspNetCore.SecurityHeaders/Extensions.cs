using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Meziantou.AspNetCore.SecurityHeaders
{
    public static class Extensions
    {
        public static IServiceCollection AddXssProtectionHeader(this IServiceCollection services, Action<XssProtectionOptions> configureOptions)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);
            return services;
        }

        public static IServiceCollection AddFrameOptionsHeader(this IServiceCollection services, Action<FrameOptions> configureOptions)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);
            return services;
        }

        public static IServiceCollection AddContentTypeOptionsHeader(this IServiceCollection services, Action<ContentTypeOptions> configureOptions)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);
            return services;
        }

        public static IServiceCollection AddReferrerPolicyHeader(this IServiceCollection services, Action<ContentTypeOptions> configureOptions)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);
            return services;
        }

        public static IServiceCollection AddContentSecurityPolicyHeader(this IServiceCollection services, Action<ContentSecurityPolicyOptions> configureOptions)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);
            return services;
        }

        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseMiddleware<XssProtectionOptions>();
            app.UseMiddleware<FrameOptions>();
            app.UseMiddleware<ContentTypeOptionsMiddleware>();
            app.UseMiddleware<ReferrerPolicyMiddleware>();
            app.UseMiddleware<ContentSecurityPolicyMiddleware>();
            return app;
        }
    }

    public class ContentSecurityPolicyOptions
    {
        public string Value { get; set; }
    }

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

    public class ContentSecurityPolicyBuilder
    {
        public ContentSecurityPolicyCollectionBuilder DefaultSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ScriptSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ImageSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder StylesheetSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder FontSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder FrameSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ManifestSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ConnectSources { get; } = new ContentSecurityPolicyCollectionBuilder();
        public ContentSecurityPolicyCollectionBuilder ReportUris { get; } = new ContentSecurityPolicyCollectionBuilder();
        public bool UpgradeInsecureRequests { get; set; }
        public bool DisownOpener { get; set; }

        public ContentSecurityPolicyBuilder WithDefaultSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(DefaultSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithScriptSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ScriptSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithImageSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ImageSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithStylesheetSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(StylesheetSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithFontSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(FontSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithFrameSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(FrameSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithManifestSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ManifestSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithConnectSources(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ConnectSources);
            return this;
        }

        public ContentSecurityPolicyBuilder WithReportUris(Action<ContentSecurityPolicyCollectionBuilder> configure)
        {
            configure(ReportUris);
            return this;
        }

        public ContentSecurityPolicyBuilder WithUpgradeInsecureRequests(bool value)
        {
            UpgradeInsecureRequests = value;
            return this;
        }

        public ContentSecurityPolicyBuilder WithDisownOpener(bool value)
        {
            DisownOpener = value;
            return this;
        }

        public string Build()
        {
            var sb = new StringBuilder();
            Append("default-src", DefaultSources);
            Append("script-src", ScriptSources);
            Append("img-src", ImageSources);
            Append("style-src", StylesheetSources);
            Append("font-src", FontSources);
            Append("frame-src", FrameSources);
            Append("connect-src", ConnectSources);
            Append("report-uri", ReportUris);
            Append("manifest-src", ManifestSources);
            if (UpgradeInsecureRequests)
            {
                sb.Append("upgrade-insecure-requests;");
            }

            if (DisownOpener)
            {
                sb.Append("disown-opener");
            }

            return sb.ToString();

            void Append(string directive, ContentSecurityPolicyCollectionBuilder values)
            {
                if (values.Values.Any())
                {
                    sb.Append(directive);
                    foreach (var value in values.Values)
                    {
                        sb.Append(' ').Append(value);
                    }

                    sb.Append(';');
                }
            }
        }
    }

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