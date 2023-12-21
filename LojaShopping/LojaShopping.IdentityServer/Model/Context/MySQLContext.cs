using LojaShopping.IdentityServer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaShopping.IdentityServer.Model.Context;

public class MySQLContext : IdentityDbContext<ApplicationUser>
{
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }
}

