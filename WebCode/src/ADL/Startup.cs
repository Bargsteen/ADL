using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ADL.Models;
using ADL.Models.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ADL
{
    public class Startup
    {
        readonly IConfigurationRoot _configuration;
        //environment is used for choosing db based on the environment (development/production)
        IHostingEnvironment _environment;

        public Startup(IHostingEnvironment env)
        {
            _environment = env;

            _configuration = new ConfigurationBuilder()
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
                    _configuration["Data:ADL:ConnectionString"]));       

            services.AddIdentity<Person, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 4;
                opts.Password.RequireDigit = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<ISchoolRepository, EfSchoolRepository>();
            services.AddTransient<IAssignmentSetRepository, EfAssignmentSetRepository>();
            services.AddTransient<ILocationRepository, EfLocationRepository>();
            services.AddTransient<IAnswerRepository, EfAnswerRepository>();
            services.AddTransient<IClassRepository, EfClassRepository>();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Logging
        
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //MigrateDatabase(app);

            app.UseSession();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();
            app.UseDeveloperExceptionPage();    

            AdminAndRolesSeedData.CreateTeacherStudentAndAdminRoles(app.ApplicationServices, _configuration).Wait();
            AdminAndRolesSeedData.CreateAdminAccount(app.ApplicationServices, _configuration).Wait();
        }
    }
}
