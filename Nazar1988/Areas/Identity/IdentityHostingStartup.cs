using System;
using Nazar1988.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookShop.Areas.Identity.Data;

[assembly: HostingStartup(typeof(Nazar1988.Areas.Identity.IdentityHostingStartup))]
namespace Nazar1988.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

                services.Configure<IdentityOptions>(options =>
                {
                    //Configure Password
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;

                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = false;
                    
                    
                });
                services.AddDbContext<Nazar1988Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Nazar1988ContextConnection")));

                //services.AddDefaultIdentity<Nazar1988User>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<Nazar1988Context>();

                services.AddIdentity<Nazar1988User, ApplicationRole>()
                     .AddDefaultUI()
                     .AddEntityFrameworkStores<Nazar1988Context>()
                     .AddErrorDescriber<ApplicationIdentityErrorDescriber>()
                     .AddRoles<ApplicationRole>()
                     .AddDefaultTokenProviders();


            });
           
        }
    }
}