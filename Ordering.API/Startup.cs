using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;

using Ordering.API.Infrastructure.AutofacModules;

namespace Ordering.API
{
    public class Startup
    {
        /// <summary>
        /// The configuration property of our application
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add any services required
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Create a new instance of the Autofac Container
            // This will become the new DI Provider
            var container = new ContainerBuilder();
            // Start populating the services added
            // previously into this container
            container.Populate(services);

            // Register all the Autofac Custom Modules of the Application.
            container.RegisterModule(new ApplicationModule());
            container.RegisterModule(new MediatorModule());

            // Build the autofac container and 
            // and return it as the service provider
            return new AutofacServiceProvider(container.Build());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the application
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
