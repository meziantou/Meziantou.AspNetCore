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
        
        [TestMethod]
        public async Task GetExistingFileInNodeModulse()
        {
            var builder = new WebHostBuilder();
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            builder.UseStartup<TestStartup>();
            var server = new TestServer(builder);
            var client = server.CreateClient();

            var result = await client.GetStringAsync("/node_modules/sample.txt");

            Assert.AreEqual("this is a sample file", result);
        }
    }
}
