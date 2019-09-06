using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlakeBot.Web.Api.Configuration;
using BlakeBot.Web.Api.Services;
using GlobalX.ChatBots.WebexTeams;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlakeBot.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.ConfigureWebexTeamsBot(Configuration);
            services.Configure<BotSettings>(Configuration);

            var builder = new ContainerBuilder();

            builder.Populate(services);

            builder.RegisterType<PhraseMuddler>().AsImplementedInterfaces();
            builder.RegisterType<WordMuddler>().AsImplementedInterfaces();

            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.ApplicationServices.GetService<IWebhookHelper>().Webhooks.RegisterWebhooksAsync();
        }
    }
}
