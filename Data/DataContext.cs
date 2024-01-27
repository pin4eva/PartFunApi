using Microsoft.EntityFrameworkCore;
using PartyFunApi.Model;

namespace PartyFunApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Product>()
                .HasOne(e => e.AddedBy);

    modelBuilder.Entity<Product>()
                .HasOne(e => e.UpdatedBy);
  }

  public required DbSet<User> Users { get; set; }
  public required DbSet<ProductCategory> ProductCategories { get; set; }
  public required DbSet<Product> Products { get; set; }
  public required DbSet<Category> Categories { get; set; }
  public required DbSet<ProductGroup> ProductGroups { get; set; }
}
