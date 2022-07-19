using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;

namespace CatalogService.Api.Extensions
{
    public static class HostExtension
    {
        public static IWebHost MigrationDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> sender)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var logger = service.GetRequiredService<ILogger<TContext>>();
                var context = service.GetService<TContext>();
                try
                {
                    logger.LogInformation($"Migration database associated with context {typeof(TContext).Name}");
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(new TimeSpan[]
                        {
                            TimeSpan.FromSeconds(1),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(8)
                        });
                    retry.Execute(() => InvokeSeeder(sender, context, service));
                    logger.LogInformation($"Migration database associated with context {typeof(TContext).Name}");
                }
                catch (Exception)
                {
                    logger.LogInformation($"an error occured while migration the database used on context  {typeof(TContext).Name}");
                }
                return host;
            }
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> sender, TContext context, IServiceProvider provider)
            where TContext : DbContext
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
            sender(context, provider);
        }
    }
}