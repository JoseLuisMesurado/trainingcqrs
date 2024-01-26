using Microsoft.EntityFrameworkCore;
using Training.Core.Contexts;
using Training.Core.SqlRepositories;
using Training.Infra.Contexts;
using Training.Infra.SqlRepositories;
using Training.NG.EFCommon;
using Training.NG.KafkaHelper;
using Training.Core;
using Training.Infra;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfraExtensions
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, EFConfig eFConfiguration)
        {
            Console.WriteLine(eFConfiguration);
            try
            {
                switch (eFConfiguration.DatabaseProviderType)
                {
                    case DatabaseType.SqlServer:
                        services.AddDbContext<ITrainigContext, TrainigContext>(options =>
                                    options.UseSqlServer(eFConfiguration.ConnectionString,
                                    optionsSql => optionsSql.MigrationsAssembly(eFConfiguration.MigrationAssembly)
                                    ));
                        //.UseLazyLoadingProxies());
                        break;
                    //case DatabaseProviderType.MySql:
                    //    services.AddDbContext<IIdentityContext, IdentityContext>(options =>
                    //                options.UseMySql(connectionString,
                    //                ServerVersion.AutoDetect(connectionString),
                    //                optionsSql => optionsSql.MigrationsAssembly("IOL.IDS4.UserManagement.MySql")
                    //                ).UseLazyLoadingProxies());
                    //    break;
                    default:
                        throw new ArgumentOutOfRangeException(DatabaseType.SqlServer, $@"The value needs to be one of {string.Join(", ", DatabaseType.SqlServer)}.");
                }
                services.InjectSQLServerRepositories();
                return services;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static IServiceCollection AddKafkaConfiguration(this IServiceCollection services, KafkaConfig kafkaFConfiguration)
        {
            services.AddSingleton(kafkaFConfiguration);
            services.AddSingleton<KafkaClientHandle>();
            services.AddSingleton<KafkaProducer<string, string>>();
            return services;
        }
        
        public static IServiceCollection InjectSQLServerRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPermissionRepository ,PermissionRepository>();
            services.AddScoped<IPermissionTypeRepository ,PermissionTypeRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
