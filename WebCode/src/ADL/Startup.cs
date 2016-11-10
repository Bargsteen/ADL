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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var connectionString = @"Server=tcp:adlearning.database.windows.net,1433;Initial Catalog=assignments;Persist Security Info=False;User ID={adladmin};Password={wqpLMCBE+4G4};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Filename=./ADL.db"));
            services.AddTransient<IAssignmentRepository, EFAssignmentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext db)
        {
            db.Database.Migrate();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.UseDeveloperExceptionPage();
            
        }
    }
}
