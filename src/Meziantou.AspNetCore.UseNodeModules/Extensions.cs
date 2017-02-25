using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace Meziantou.AspNetCore.UseNodeModules
{
    public static class Extensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app)
        {
            return UseNodeModules(app, null);
        }

        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            const string path = "node_modules";
            var finalDirectory = Path.Combine(env != null ? env.ContentRootPath : Directory.GetCurrentDirectory(), path);
            if (!Directory.Exists(finalDirectory))
            {
                Directory.CreateDirectory(finalDirectory);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(finalDirectory),
                RequestPath = new PathString("/" + path),
                ServeUnknownFileTypes = true
            });

            return app;
        }
    }
}
