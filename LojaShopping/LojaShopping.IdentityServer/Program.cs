using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Services;
using LojaShopping.IdentityServer.Services;
using LojaShopping.IdentityServer.Configuration;
using LojaShopping.IdentityServer.Initializer;
using LojaShopping.IdentityServer.Model;
using LojaShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LojaShopping.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];

            builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
                connection,
                new MySqlServerVersion(new Version(8, 0, 33))));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MySQLContext>()
                .AddDefaultTokenProviders();

            var b = builder.Services.AddIdentityServer(opt =>
                   {
                       opt.Events.RaiseErrorEvents = true;
                       opt.Events.RaiseInformationEvents = true;
                       opt.Events.RaiseFailureEvents = true;
                       opt.Events.RaiseSuccessEvents = true;
                       opt.EmitStaticAudienceClaim = true;
                   }).AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                     .AddInMemoryApiScopes(IdentityConfiguration.ApiScope)
                     .AddInMemoryClients(IdentityConfiguration.Cliente)
                     .AddAspNetIdentity<ApplicationUser>();

            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IProfileService, ProfileService>();

            b.AddDeveloperSigningCredential();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            var dbInitializeService = app.Services.CreateScope().ServiceProvider.GetService<IDbInitializer>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            dbInitializeService.Initialize();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}