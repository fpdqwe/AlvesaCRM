using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.Product;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
namespace DAL
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<ProductModel> ProductModels { get; set; }
		public DbSet<TechSpec> TechSpecs { get; set; }
		public DbSet<ProductColor> ProductColors { get; set; }
		public DbSet<ProductSize> ProductSizes { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<IndivisibleOperation> IndivisibleOperations { get; set; }

		
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
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
			modelBuilder.Entity<TechSpec>()
				.HasMany(c => c.Colors)
				.WithOne(c => c.TechSpec)
				.HasForeignKey(c => c.TechSpecId);
			modelBuilder.Entity<ProductColor>()
				.HasMany(s => s.Sizes)
				.WithOne(s => s.Color)
				.HasForeignKey(s => s.ColorId);
			modelBuilder.Entity<Company>()
				.HasMany(x => x.Products)
				.WithOne(x => x.Company)
				.HasForeignKey(x => x.CompanyId);
			modelBuilder.Entity<Company>()
				.HasMany(x => x.Employee)
				.WithOne(x => x.Company)
				.HasForeignKey(x => x.CompanyId);

			modelBuilder.Entity<IndivisibleOperation>()
				.HasOne(x => x.Company)
				.WithMany(x => x.IndivisibleOperations)
				.HasForeignKey(x => x.CompanyId);
			modelBuilder.Entity<UnitedOperation>()
				.HasMany(x => x.Operations)
				.WithMany(x => x.Parents);
		}
	}
}
