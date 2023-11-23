using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Services;
using BookShop.Models.Repository;
using Cart_Exam.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Nazar1988.Areas.Identity.Data.Services;
using Nazar1988.Areas.MyMaster.Controllers;
using Nazar1988.Class;
using Nazar1988.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Nazar1988
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Qurtz
            services.AddSingleton<IJobFactory,SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<RemoveCartJob>();
            
            services.AddSingleton<RemoveCartJob>();
            
            services.AddSingleton(new JobSchedule(jobType:typeof(RemoveCartJob),cronExpression:
                "0 0 0/23 * * ?"
                ));
            services.AddHostedService<QuartzHostedService>();
            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddDbContext<NazarDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Nazar1988ContextConnection")));
            
            
            
            services.AddTransient<NazarDbContext>();

            services.AddTransient<BooksRepository>();
            
            services.AddTransient<_WalletService,WalletService>();

            services.AddScoped<IEmailSender, EmailSender>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AccessToUsersManager", policy => policy.RequireRole("مدیر سایت"));
              
            });


            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            services.AddMvc(options =>
            {
                var F = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var L = F.Create("ModelBindingMessages", "Nazar1988");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                 (x) => L["انتخاب یکی از موارد لیست الزامی است."]);

            });
            services.ConfigureApplicationCookie(option => {
                option.LoginPath = "/Account/SignIn";
                
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name:"areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}