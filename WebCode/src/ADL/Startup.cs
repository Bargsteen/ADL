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
            services.AddTransient<IAssignmentRepository, EFAssignmentRepository>();
            services.AddTransient<ILocationRepository, EFLocationRepository>();
            services.AddTransient<IAnswerRepository, EFAnswerRepository>();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            MigrateDatabase(app);

            if (env.IsDevelopment())
            {
                InitilizeDatabase(app);
            }

            app.UseSession();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseDeveloperExceptionPage();
            
        }

        // Magi!!!
        private void MigrateDatabase(IApplicationBuilder app)
        {
            // Initilize datbase
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                // Automatic migrate of datbase
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

                // Get context
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Automatic migrate of datbase
                context.Database.Migrate();
            }
        }

        private void InitilizeDatabase(IApplicationBuilder app)
        {
            // Initilize datbase
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                // Automatic migrate of datbase
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

                // Get context
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Add test data
                if (!context.Locations.Any())
                {
                    context.Locations.AddRange(new Location { Title = "Location1", Description = "Location1" }, new Location { Title = "Location2", Description = "Location2" });
                }

                // Save changes
                context.SaveChanges();
            }
        }
    }
}
