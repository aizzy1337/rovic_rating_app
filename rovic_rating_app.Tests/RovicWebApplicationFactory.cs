using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using rovic_rating_app.Data;

namespace rovic_rating_app.Tests
{
    internal class RovicWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<DataContext>));

                services.AddSqlite<DataContext>("DataSource=test.db;Cache=Shared");

                var dbContext = CreateDbContext(services);
                dbContext.Database.EnsureDeleted();
            });
        }

        private static DataContext CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            return dbContext;
        }
    }
}
