using Microsoft.EntityFrameworkCore;
using Training.Core.Contexts;
using Training.Core.SqlRepositories;
using Training.Infra.Contexts;
using Training.Infra.SqlRepositories;
using Training.NG.EFCommon;
using Training.NG.KafkaHelper;
using Training.Core;
using Training.Infra;
using Training.NG.ElasticSearchHelper;
using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using Training.Core.Entities;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

        public static IServiceCollection AddElasticSearch(this IServiceCollection services, ElasticSearchConfig elasticSearchConfig)
        {
            var pool = new SingleNodeConnectionPool(new Uri(elasticSearchConfig.Url));

            var settings= new ConnectionSettings(pool,(builtInSerializer, connectionSettings) =>
                new JsonNetSerializer(builtInSerializer, connectionSettings, () => new Newtonsoft.Json.JsonSerializerSettings{
                    ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }))
                .DefaultFieldNameInferrer(p=>p)
                .PrettyJson();

            AddDefaultMapping(settings);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
            CreateIndex(client, elasticSearchConfig.DefaultIndex);
            services.AddElasticSearchIndexRepositories();
            return services;
        }

        public static IServiceCollection AddElasticSearchIndexRepositories(this IServiceCollection services){

            services.TryAddSingleton<IPermissionElasticRepository>(sp=>{
                var client = sp.GetRequiredService<IElasticClient>();
                return new PermissionElasticRepository(client,"training-index");
            });
            return services;
        }
        private static void CreateIndex(ElasticClient client, string defaultIndex)
        {
            client.Indices.Create(defaultIndex, index =>
                index.Map<Permission<Guid>>(m=>m.AutoMap())
            );
        }

        private static void AddDefaultMapping(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<Permission<Guid>>(m=>m).PrettyJson(false);
        }
    }
}
