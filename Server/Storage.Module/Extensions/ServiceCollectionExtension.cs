using HostLibrary.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Module.Repositories;
using Storage.Module.Repositories.Base;
using Storage.Module.Repositories.Base.Interfaces;
using Storage.Module.Repositories.Interfaces;
using Storage.Module.Services;
using Storage.Module.Services.Interfaces;

namespace Storage.Module.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddStorage(this IServiceCollection services, IConfiguration configuration)
        {
            DbSettings dbSettings = DbSettings.Initialize(configuration);

            if (dbSettings.DbProviderType == DbProviderType.SqlServer)
            {
                services.AddDbContext<DataContext>(
                    opt => opt.UseSqlServer(
                        dbSettings.ConnectionString,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(dbSettings.RetryAttemts);
                        }
                    )
                );
            }
            else if (dbSettings.DbProviderType == DbProviderType.PostgreSql)
            {
                services.AddDbContext<DataContext>(
                    opt => opt.UseNpgsql(
                        dbSettings.ConnectionString,
                        npgsqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(dbSettings.RetryAttemts);
                        }
                    )
                );
            }
        }

        public static void AddStorageServices(this IServiceCollection services)
        {
            services.AddTransient<IDbSettingsService, DbSettingsService>();
            services.AddTransient<IInitialCreateService, InitialCreateService>();
        }

        public static void AddStorageRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository, BaseRepository>();

            services.AddScoped<IChainRepository, ChainRepository>();
            services.AddScoped<IExchangeChainRepository, ExchangeChainRepository>();
            services.AddScoped<IExchangeRepository, ExchangeRepository>();
            services.AddScoped<INotificationSoundRepository, NotificationSoundRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();
            services.AddScoped<ITickerRepository, TickerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}