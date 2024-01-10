using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tailoring.Entities;

namespace Tailoring.Data;

public class TailoringContext:DbContext
{
    public TailoringContext(DbContextOptions<TailoringContext> options):base(options){

    }
    
    public DbSet<Post> Posts  => Set<Post>();

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }*/
}