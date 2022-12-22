using Microsoft.EntityFrameworkCore;
using passnager_api;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Profile> Profile { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
}