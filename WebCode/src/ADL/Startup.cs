using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ADL.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ADL
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        //environment is used for choosing db based on the environment (development/production)
        IHostingEnvironment environment;

        public Startup(IHostingEnvironment env)
        {
            environment = env;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            /*if(environment.IsProduction())
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:ADL:ConnectionString"]));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Filename=./ADL.db"));
            }*/
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration["Data:ADL:ConnectionString"]));

            /*services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite(
                    Configuration["Data:ADLIdentity:ConnectionString"]));*/
                    
            services.AddTransient<IAssignmentRepository, EFAssignmentRepository>();
            services.AddTransient<ILocationRepository, EFLocationRepository>();
            services.AddMemoryCache();
            services.AddSession();

             //services.AddIdentity<IdentityUser, IdentityRole>()
               // .AddEntityFrameworkStores<AppIdentityDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext db)
        {
            db.Database.Migrate();
            app.UseSession();
            //app.UseIdentity();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseDeveloperExceptionPage();
           // IdentitySeedData.EnsurePopulated(app);
            
        }
    }
}
