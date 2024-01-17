using Microsoft.EntityFrameworkCore;

namespace LojaShopping.CupomAPI.Model.Context;

public class MySQLContext : DbContext
{
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

    public DbSet<Cupom> Cupoms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Cupom>().HasData(new Cupom
        {
            Id = 1,
            CodCupom = "ERUDIO_2022_10",
            DescontoTotal = 10
        });
        modelBuilder.Entity<Cupom>().HasData(new Cupom
        {
            Id = 2,
            CodCupom = "ERUDIO_2022_15",
            DescontoTotal = 15
        });
    }
}
