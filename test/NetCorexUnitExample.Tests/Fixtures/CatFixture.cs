using System;
using System.Net.Http;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace xUnitTestDemo.Tests.Fixtures
{
    public class BaseFixture<TStartup> : IDisposable where TStartup : class
    {
        public HttpClient Client { get; }
        public TestServer Server { get; }

        public BaseFixture()
        {
            var builder = WebHost
                .CreateDefaultBuilder()
                .UseStartup<TStartup>()
                //Without this configuration may generate exceptions on consume dbcontext service
                .UseDefaultServiceProvider(options => options.ValidateScopes = false);

            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}
