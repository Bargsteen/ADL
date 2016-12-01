using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ADL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ADL
{
    public class Startup
    {
        IConfigurationRoot configuration;
        //environment is used for choosing db based on the environment (development/production)
        IHostingEnvironment environment;

        public Startup(IHostingEnvironment env)
        {
            environment = env;

            configuration = new ConfigurationBuilder()
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

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlite(
                    configuration["Data:ADL:ConnectionString"]));       

            services.AddIdentity<Person, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<ISchoolRepository, EFSchoolRepository>();
            services.AddTransient<IAssignmentSetRepository, EFAssignmentSetRepository>();
            services.AddTransient<ILocationRepository, EFLocationRepository>();
            services.AddTransient<IAnswerRepository, EFAnswerRepository>();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Logging
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //MigrateDatabase(app);

            app.UseSession();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();
            app.UseDeveloperExceptionPage();    

            ApplicationDbContext.CreateAdminAccount(app.ApplicationServices, configuration).Wait();      
        }
    }
}
