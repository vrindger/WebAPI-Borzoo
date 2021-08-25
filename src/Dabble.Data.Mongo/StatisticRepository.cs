using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Dabble.Data.Abstractions;
using Dabble.Data.Abstractions.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dabble.Data.Mongo
{
    /// <inheritdoc />
    public class StatisticRepository : IStatisticRepository
    {
        private readonly IMongoCollection<Statistic> _collection;

        private FilterDefinitionBuilder<Statistic> Filter => Builders<Statistic>.Filter;

        /// <inheritdoc />
        public StatisticRepository(
            IMongoCollection<Statistic> collection
        )
        {
            _collection = collection;
        }

        /// <inheritdoc />
        public async Task<Statistic> GetByNameAndYearAsync(
            string Country,
            string Year,
            CancellationToken cancellationToken = default
        )
        {
            bool countryExists = !string.IsNullOrEmpty(Country);
            bool yearExists = !string.IsNullOrEmpty(Year);
            Statistic entity=null;
            if (!yearExists && !countryExists)
            {
                throw new EntityNotFoundException(Country);
            }
            else if (yearExists && countryExists) {
                entity = await _collection
                    .Find(Filter.And(Filter.Eq("Country", Country), Filter.Eq("Year", Year)))
                    .SingleOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            else if (yearExists) {
                entity = await _collection
                    .Find( Filter.Eq("Year", Year))
                    .SingleOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            else if (countryExists) {
                entity = await _collection
                    .Find(Filter.Eq("Country", Country))
                    .SingleOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            
            if (entity is null)
            {
                throw new EntityNotFoundException(Country);
            }

            return entity;
        }

        /// <inheritdoc />
        public async Task AddAsync(
            Statistic entity,
            CancellationToken cancellationToken = default
        )
        {

            try
            {
                await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (MongoWriteException e) when (
                e.WriteError.Category == ServerErrorCategory.DuplicateKey &&
                e.WriteError.Message.Contains(" index: username ")
            )
            {
                throw new DuplicateKeyException(nameof(Statistic.Country));
            }
        }

        /// <inheritdoc />
        public async Task<Statistic> GetByIdAsync(
            string id,
            CancellationToken cancellationToken = default
        )
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new EntityNotFoundException(id);
            }

            Statistic entity = await _collection
                .Find(Filter.Eq("_id", objectId))
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new EntityNotFoundException(id);
            }

            return entity;
        }

        /// <inheritdoc />
        public async Task<Statistic> GetByNameAsync(
            string country,
            CancellationToken cancellationToken = default
        )
        {
            country = Regex.Escape(country);
            var filter = Filter.Regex(u => u.Id, new BsonRegularExpression($"^{country}$", "i"));

            Statistic entity = await _collection
                .Find(filter)
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new EntityNotFoundException("User Name", country);
            }

            return entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(
            string id,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _collection
                .DeleteOneAsync(Filter.Eq("_id", ObjectId.Parse(id)), cancellationToken)
                .ConfigureAwait(false);

            if (result.DeletedCount == 0)
            {
                throw new EntityNotFoundException(nameof(Statistic.Id));
            }
        }

        /// <inheritdoc />
        public async Task DeleteStatisticAsync(
            string Country,
            string Year,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _collection
                .DeleteOneAsync(Filter.And(Filter.Eq("Country", Country), Filter.Eq("Year", Year)), cancellationToken)
                .ConfigureAwait(false);

            if (result.DeletedCount == 0)
            {
                throw new EntityNotFoundException(nameof(Statistic.Id));
            }
        }
    }
}
