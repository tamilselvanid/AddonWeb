using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AddonWeb.Data;
using AddonWeb.Models;
using AddonWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AddonWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddDirectoryBrowser();
            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IUrls, Urls>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            //app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse =
        r =>
        {
            string path = r.File.PhysicalPath;
            if (path.EndsWith(".css") || path.EndsWith(".js") || path.EndsWith(".gif") || path.EndsWith(".jpg") || path.EndsWith(".png") || path.EndsWith(".svg"))
            {
                TimeSpan maxAge = new TimeSpan(7, 0, 0, 0);
                r.Context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
            }
        }
            });


            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            

            app.UseMvc(

            routes =>
            {
                routes.MapRoute(
                 "SeoRoute",
                 "{*permalink}",
                defaults: new { controller = "Software", action = "Index" },
                constraints: new { permalink = new CmsUrlConstraint(new Urls()) });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            }
             
            );
        }
    }
}
public class CmsUrlConstraint : IRouteConstraint
{
    private readonly IUrls _urls;
    public CmsUrlConstraint(IUrls urls)
    {
        _urls = urls;
    }
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        var db = _urls.getUrls;
        if (values[routeKey] != null)
        {
            var permalink = values[routeKey].ToString().ToLower();
            var page = db.Where(p => p.Url == permalink).FirstOrDefault();
            if (page != null)
            {
                httpContext.Items["cmspage"] = page;
                return true;
            }
            return false;

          
        }
        return false;
    }

    
}