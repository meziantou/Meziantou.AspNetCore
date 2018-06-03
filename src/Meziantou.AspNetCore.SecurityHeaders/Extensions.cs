using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
}