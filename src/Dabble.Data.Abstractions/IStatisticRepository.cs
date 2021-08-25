using System.Threading;
using System.Threading.Tasks;
using Dabble.Data.Abstractions.Entities;

namespace Dabble.Data.Abstractions
{
    /// <summary>
    /// A repository for the Statistic entities
    /// </summary>
    public interface IStatisticRepository : IEntityRepository<Statistic>
    {
        /// <summary>
        /// Gets a Statistic by its unique Country and Year
        /// </summary>
        /// <param name="Country">Country name to search for</param>
        /// <param name="Year">Year to search for</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param> 
        Task<Statistic> GetByNameAndYearAsync(
            string Country,
            string Year,
            CancellationToken cancellationToken = default
        );


        /// <summary>
        /// Deletes a Statistic by its unique Country and Year
        /// </summary>
        public Task DeleteStatisticAsync(
           string Country,
           string Year,
           CancellationToken cancellationToken = default
       );



    }
}
