using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace BlakeBot.Web.Api
{
    public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>();
        }
    }
}
