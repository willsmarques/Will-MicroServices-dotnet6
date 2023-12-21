using IdentityModel;
using LojaShopping.IdentityServer.Configuration;
using LojaShopping.IdentityServer.Model;
using LojaShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LojaShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySQLContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(MySQLContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> role)
        {
            _context = context;
            _userManager = userManager;
            _role = role;
        }

        public void Initialize()
        {
            if (_role.FindByIdAsync(IdentityConfiguration.Admin).Result != null)
            {
                return;
            }
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "william-admin",
                Email = "william-admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (71) 12345-6789",
                PrimeiroNome = "william",
                UltimoNome = "admin"
            };

            _userManager.CreateAsync(admin,"William123$").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.PrimeiroNome}{admin.UltimoNome}"),
                new Claim(JwtClaimTypes.GivenName, admin.PrimeiroNome),
                new Claim(JwtClaimTypes.FamilyName, admin.UltimoNome),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;
            
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "william-client",
                Email = "william-client@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (71) 12345-6789",
                PrimeiroNome = "william",
                UltimoNome = "Client"
            };

            _userManager.CreateAsync(client,"William123$").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

            var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.PrimeiroNome}{client.UltimoNome}"),
                new Claim(JwtClaimTypes.GivenName, client.PrimeiroNome),
                new Claim(JwtClaimTypes.FamilyName, client.UltimoNome),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;
        }
    }
}
