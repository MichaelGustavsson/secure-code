using api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace api.Data;

public class DataContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
  public DbSet<Product> Products { get; set; }
}
