using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;

namespace TicTacToeWEB.Models.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public Context()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>()
                    .HasOne(a => a.Field)
                    .WithOne(b => b.Session)
                    .OnDelete(DeleteBehavior.Cascade);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //on Depoy use ConnectionString from appSettings of .cvv file 
        optionsBuilder.UseSqlite("Filename=TicTacToeDB.db");
        base.OnConfiguring(optionsBuilder);
    }


    public DbSet<Session> Session { get; set; }
    public DbSet<Field> Field { get; set; }


}



