using Microsoft.EntityFrameworkCore;
using Tailoring.Repository;

namespace Tailoring.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<TailoringContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString=configuration.GetConnectionString("TailoringContext");
        services.AddSqlServer<TailoringContext>(connString)
            .AddScoped<IPostsRepository,EntityFrameworkRepository>();
        return services;

    }
}