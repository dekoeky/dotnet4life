using Microsoft.EntityFrameworkCore;
using WebApplication1.Database.Models;

namespace WebApplication1.Database;

internal class MyDb : DbContext
{
    public DbSet<Row> Rows { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        // TODO: Configure DbContext
        //optionsBuilder.UseInMemory();
        //optionsBuilder.UseSqlLite();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TODO: Configure test data
        //modelBuilder.Entity<Row>().HasData();
    }
}
