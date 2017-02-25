using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace Meziantou.AspNetCore.LetsEncrypt
{
    public static class Extensions
    {
        public static IApplicationBuilder UseLetsEncrypt(this IApplicationBuilder app)
        {
            return UseLetsEncrypt(app, null);
        }

        public static IApplicationBuilder UseLetsEncrypt(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            const string path = ".well-known/acme-challenge";
            var finalDirectory = Path.Combine(env != null ? env.ContentRootPath : Directory.GetCurrentDirectory(), path);
            if (!Directory.Exists(finalDirectory))
            {
                Directory.CreateDirectory(finalDirectory);
            }
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(finalDirectory),
                RequestPath = new PathString("/" + path),
                ServeUnknownFileTypes = true // serve extensionless file
            });

            return app;
        }
    }
}
