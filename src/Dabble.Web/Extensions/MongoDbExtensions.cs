using System;
using Dabble.Data.Abstractions;
using Dabble.Data.Abstractions.Entities;
using Dabble.Data.Mongo;
using Dabble.Web.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using UserEntity = Dabble.Data.Abstractions.Entities.User;
using StatisticEntity = Dabble.Data.Abstractions.Entities.Statistic;

namespace Dabble.Web.Extensions
{
    internal static class MongoDbExtensions
    {
        /// <summary>
        /// Adds MongoDB services to the app's service collection
        /// </summary>
        public static void AddMongoDb(
            this IServiceCollection services,
            IConfigurationSection dataSection
        )
        {
            string connectionString = dataSection.GetValue<string>(nameof(MongoOptions.ConnectionString));
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($@"Invalid MongoDB connection string: ""{connectionString}"".");
            }

            services.Configure<MongoOptions>(dataSection);

            string dbName = new ConnectionString(connectionString).DatabaseName;
            services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient(connectionString));
            services.AddTransient<IMongoDatabase>(provider =>
                provider.GetRequiredService<IMongoClient>().GetDatabase(dbName)
            );

            services.AddTransient<IMongoCollection<UserEntity>>(_ =>
                _.GetRequiredService<IMongoDatabase>()
                    .GetCollection<UserEntity>("users")
            );
            services.AddTransient<IMongoCollection<StatisticEntity>>(_ =>
                _.GetRequiredService<IMongoDatabase>()
                    .GetCollection<StatisticEntity>("Statistics")
            );
            services.AddTransient<IMongoCollection<TaskList>>(_ =>
                _.GetRequiredService<IMongoDatabase>()
                    .GetCollection<TaskList>("task-lists")
            );
            services.AddTransient<IMongoCollection<TaskItem>>(_ =>
                _.GetRequiredService<IMongoDatabase>()
                    .GetCollection<TaskItem>("task-items")
            );

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStatisticRepository, StatisticRepository>();
            services.AddTransient<ITaskListRepository, TaskListRepository>();
            services.AddTransient<ITaskItemRepository, TaskItemRepository>();

            MongoInitializer.RegisterClassMaps();
        }
    }
}
