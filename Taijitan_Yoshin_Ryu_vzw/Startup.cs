﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taijitan_Yoshin_Ryu_vzw.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Taijitan_Yoshin_Ryu_vzw.Models.Domain;
using Taijitan_Yoshin_Ryu_vzw.Data.Repositories;

namespace Taijitan_Yoshin_Ryu_vzw {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization(options => {
                options.AddPolicy("Lid", policy => policy.RequireClaim(ClaimTypes.Role, "lid"));
                options.AddPolicy("Lesgever", policy => policy.RequireClaim(ClaimTypes.Role, "lesgever"));
            });

            services.AddScoped<IGebruikerRepository, GebruikerRepository>();
            services.AddScoped<IFormuleRepository, FormuleRepository>();
            services.AddSession();

            services.AddMvc()
                .AddRazorPagesOptions(options => { options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/Login"); })
                .AddRazorPagesOptions(options => { options.Conventions.AddAreaPageRoute("Identity", "/Account/Manage/Index", "Gebruiker/Edit"); })
                .AddRazorPagesOptions(options => { options.Conventions.AddAreaPageRoute("Identity", "/Account/Manage/ChangePassword", "Gebruiker/EditPassword"); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<DataInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataInitializer dataInitializer) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            dataInitializer.InitializeData().Wait(-1);
        }
    }
}