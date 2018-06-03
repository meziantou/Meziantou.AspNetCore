using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;

namespace Meziantou.AspNetCore.UseNodeModules.Tests
{
    [TestClass]
    public class ExtensionsTests
    {
        private class TestStartup
        {
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {
                app.UseNodeModules(env);
            }
        }

        private class TempDirectory : IDisposable
        {
            public string Path { get; }

            public TempDirectory()
            {
                Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(Path);
            }

            public void Dispose()
            {
                if (Directory.Exists(Path))
                {
                    Directory.Delete(Path, recursive: true);
                }
            }
        }

        [TestMethod]
        public async Task GetExistingFileInNodeModulse()
        {
            using (var tempDir = new TempDirectory())
            {
                Directory.CreateDirectory(Path.Combine(tempDir.Path, "node_modules"));
                File.WriteAllText(Path.Combine(tempDir.Path, "node_modules", "sample.txt"), "this is a sample file");

                var builder = new WebHostBuilder();
                builder.UseContentRoot(tempDir.Path);
                builder.UseStartup<TestStartup>();
                var server = new TestServer(builder);
                var client = server.CreateClient();

                var result = await client.GetStringAsync("/node_modules/sample.txt");

                Assert.AreEqual("this is a sample file", result);
            }
        }
    }
}
