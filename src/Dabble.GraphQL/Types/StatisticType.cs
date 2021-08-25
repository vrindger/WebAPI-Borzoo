using Dabble.GraphQL.Models;
using GraphQL.Types;

namespace Dabble.GraphQL.Types
{
    /// <summary>
    /// Represents Statistic type
    /// </summary>
    public class StatisticType : ObjectGraphType<StatisticDto>
    {
        /// <inheritdoc />
        public StatisticType(IQueryResolver queryResolver)
        {
            Name = "Statistic";
            Description = "A Dabble Statistic account";

            //Field(_ => _.Id)
            //    .Description("Statistic Id");

            Field(_ => _.Country)
                .Description("Country");

            Field(_ => _.Year)
                .Description("Year");

            Field(_ => _.Area)
                .Description("Area");

            Field(_ => _.Population)
                .Description("Population");

        }
    }
}
