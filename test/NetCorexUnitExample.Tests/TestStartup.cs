using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace xUnitTestDemo.Tests
{
    public class TestStartup
    {
        public IConfiguration Configuration { get; }

        public TestStartup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services) => services.AddMvc();
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) => app.UseMvc();
    }
}
