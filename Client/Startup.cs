using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Identity;
using DomainServices;
using Infrastructure.Cook;
using Infrastructure.Client;
using Domain;
using Domain.Extensions;
using Domain.Extensions.Session;

namespace Client
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<CookDbContext>(options =>
                     options.UseSqlServer(Configuration["Cook:ConnectionString"]));

            services.AddDbContext<ClientDbContext>(options =>
                    options.UseSqlServer(Configuration["Client:ConnectionString"]));

            services.AddDbContext<LoginDbContext>(options =>
                     options.UseSqlServer(Configuration["Login:Identity"]));

            services.AddIdentity<AbstractUser, IdentityRole>()
          .AddEntityFrameworkStores<LoginDbContext>();

            services.AddTransient<IAbstractUser, EFAbstractUser>();
            services.AddTransient<IClientService, EFClientService>();
            services.AddTransient<IMealService, EFMealService>();
            services.AddTransient<IDishService, EFDishService>();
            services.AddTransient<IOrderService, EFOrderService>();


            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
