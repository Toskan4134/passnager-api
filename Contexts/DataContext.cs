using Microsoft.EntityFrameworkCore;
using passnager_api;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<ProfileEntity> Profile { get; set; }
}