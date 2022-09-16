using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS_TEP_AB.BLL;
using EMS_TEP_AB.Common;
using EMS_TEP_AB.DAL;
using EMS_TEP_AB.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EMS_TEP_AB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<IConfiguration>(Configuration);
            DataAccess.connectionString = Configuration.GetConnectionString("defaultConnection");           
            services.AddScoped<ISecurity, BllSecurity>();
            services.AddScoped<IAttendance, BllAttendance>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
