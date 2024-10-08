﻿using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.Product;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
namespace DAL
{
	public class ApplicationDbContext : DbContext
	{
		public static User CurrentUser { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<ProductModel> ProductModels { get; set; }
		public DbSet<TechSpec> TechSpecs { get; set; }
		public DbSet<ProductColor> ProductColors { get; set; }
		public DbSet<ProductSize> ProductSizes { get; set; }

		
        public ApplicationDbContext(string nameorConnectionString)
        {
			Database.SetConnectionString(nameorConnectionString);
            Database.EnsureCreated();
        }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            optionsBuilder
                .LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductModel>()
				.HasMany(t => t.TechSpecs)
				.WithOne(t => t.ProductModel)
				.HasForeignKey(t => t.ModelId);
			modelBuilder.Entity<ProductModel>()
				.HasMany(u => u.Customers);	
			modelBuilder.Entity<TechSpec>()
				.HasMany(c => c.Colors)
				.WithOne(c => c.TechSpec)
				.HasForeignKey(c => c.TechSpecId);
			modelBuilder.Entity<ProductColor>()
				.HasMany(s => s.Sizes)
				.WithOne(s => s.Color)
				.HasForeignKey(s => s.ColorId);
			
		}
	}
}
