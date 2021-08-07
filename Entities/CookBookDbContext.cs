using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cookBook.Entities.DifficultyProperties;

namespace cookBook.Entities
{
    public class CookBookDbContext : DbContext
    {
        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=CookBookDb;Trusted_Connection=True;";

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Recipe>()
                .Property(r => r.PrepareTime)
                .IsRequired();
            modelBuilder.Entity<Recipe>()
                .Property(r => r.SummaryTime)
                .IsRequired();

            modelBuilder.Entity<Step>()
                .Property(r => r.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Ingredient>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Ingredient>()
                .Property(r => r.Amount)
                .IsRequired();
            modelBuilder.Entity<Ingredient>()
                .Property(r => r.Unit)
                .IsRequired()
                .HasMaxLength(50);



            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(x => new {x.RecipeId, x.IngredientId});



        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
