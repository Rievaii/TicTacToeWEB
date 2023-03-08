﻿using Microsoft.EntityFrameworkCore;
using TicTacToeWEB.Models;
namespace TicTacToeWEB
{
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
                        .WithOne(b => b.Session);
                        

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
}


