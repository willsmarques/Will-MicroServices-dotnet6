using LojaShopping.Web.Services;
using LojaShopping.Web.Services.IService;
using Microsoft.AspNetCore.Authentication;

namespace LojaShopping.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient<IProductService, ProductService>(
                     c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]));
           
            builder.Services.AddHttpClient<ICartService, CartService>(
                    c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"]));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(opt => {

                opt.DefaultScheme = "Cookies";
                opt.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
            .AddOpenIdConnect("oidc",opt =>
            {
                opt.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
                opt.GetClaimsFromUserInfoEndpoint = true;
                opt.ClientId = "loja_shopping";
                opt.ClientSecret = "my_super_secret";
                opt.ResponseType = "code";
                opt.ClaimActions.MapJsonKey("role", "role", "role");
                opt.ClaimActions.MapJsonKey("sub", "sub", "sub");
                opt.TokenValidationParameters.NameClaimType = "name";
                opt.TokenValidationParameters.RoleClaimType = "role";
                opt.Scope.Add("loja_shopping");
                opt.SaveTokens = true;
            });


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}