using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.MVC.Configuration;
using Store.MVC.WebServiceAccess;
using Store.MVC.WebServiceAccess.Base;
using Store.MVC.Authentication;
using Store.MVC.Filters;
using Store.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Store.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => Configuration);
            services.AddSingleton<IWebServiceLocator, WebServiceLocator>();
            services.AddSingleton<IWebApiCalls, WebApiCalls>();
            services.AddSingleton<IAuthHelper, AuthHelper>();
            services.AddMvc(config => {
                config.Filters.Add(
                    new AuthActionFilter(services.BuildServiceProvider().GetService<IAuthHelper>()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Products/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Products}/{action=Index}/{id?}");
            });
        }
    }
}
