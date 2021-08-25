using System.Threading.Tasks;
using Dabble.Data.Abstractions;
using Dabble.Data.Abstractions.Entities;
using Dabble.Data.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Dabble.Web.Data
{
    internal static class DataSeederExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var _ = app.ApplicationServices.CreateScope())
            {
                var logger = _.ServiceProvider.GetRequiredService<ILogger<Startup>>();

                var db = _.ServiceProvider.GetRequiredService<IMongoDatabase>();
                bool dbInitialized = InitMongoDbAsync(db).GetAwaiter().GetResult();
                if (dbInitialized)
                    logger.LogInformation("Mongo database is initialized.");

                var userRepo = _.ServiceProvider.GetRequiredService<IUserRepository>();
                var statisticRepo = _.ServiceProvider.GetRequiredService<IStatisticRepository>();
                bool seeded = SeedData(statisticRepo).GetAwaiter().GetResult(); 
                bool seededStatistic = SeedData(statisticRepo).GetAwaiter().GetResult();
                logger.LogInformation($"User Database is{(seeded ? "" : " NOT")} seeded.");
                logger.LogInformation($"Statistic Database is{(seededStatistic ? "" : " NOT")} seeded.");
            }
        }

        private static async Task<bool> SeedData(IStatisticRepository statisticRepo)
        {
            if (await IsAlreadySeeded(statisticRepo))
            {
                return false;
            }

            Statistic[] testStatistics =
            {
                new Statistic
                {
                    Country= "Albania",
                    Year= "2000",
                    Area= 28748,
                    Population= 3401198
                },
                new Statistic
                {
                   Country= "Albania",
                   Year= "2001",
                   Area= 28748,
                   Population= 3073734
                },

                new Statistic
                {
                   Country= "Albania",
                   Year= "2002",
                   Area= 28748,
                   Population= 3093465
                },
                new Statistic
                {
                   Country= "Albania",
                    Year= "2003",
                    Area= 28748,
                    Population= 3111162
                }
            };

            foreach (var statistic in testStatistics)
            {
                await statisticRepo.AddAsync(statistic);
            }

            return true;
        }

        private static async Task<bool> SeedData(IUserRepository userRepo)
        {
            if (await IsAlreadySeeded(userRepo))
            {
                return false;
            }

            User[] testUsers =
            {
                new User
                {
                    DisplayId = "alice0",
                    FirstName = "Alice",
                    PassphraseHash = "secret_passphrase"
                },
                new User
                {
                    DisplayId = "bobby",
                    FirstName = "Bob",
                    LastName = "Boo",
                    PassphraseHash = "secret_passphrase2"
                },
            };

            foreach (var user in testUsers)
            {
                await userRepo.AddAsync(user);
            }

            return true;
        }

        private static async Task<bool> IsAlreadySeeded(IUserRepository userRepo)
        {
            bool userExists;
            try
            {
                await userRepo.GetByNameAsync("alICE0");
                userExists = true;
            }
            catch (EntityNotFoundException)
            {
                userExists = false;
            }

            return userExists;
        }

        private static async Task<bool> IsAlreadySeeded(IStatisticRepository statisticRepo)
        {
            bool statisticExists;
            try
            {
                await statisticRepo.GetByNameAndYearAsync("Albania", "2000");
                statisticExists = true;
            }
            catch (EntityNotFoundException)
            {
                statisticExists = false;
            }

            return statisticExists;
        }
        private static async Task<bool> InitMongoDbAsync(IMongoDatabase db)
        {
            var cursor = await db.ListCollectionNamesAsync();
            var collections = await cursor.ToListAsync();

            bool collectionsExist = collections.Count > 2;

            if (!collectionsExist)
            {
                await MongoInitializer.CreateSchemaAsync(db);
            }

            return !collectionsExist;
        }
    }
}
