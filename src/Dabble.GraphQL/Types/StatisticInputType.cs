using Dabble.GraphQL.Models;
using GraphQL.Types;

namespace Dabble.GraphQL.Types
{
    /// <summary>
    /// Represents the Statistic creation input type
    /// </summary>
    public class StatisticInputType : InputObjectGraphType<StatisticCreationDto>
    {
        /// <inheritdoc />
        public StatisticInputType()
        {
            Name = "StatisticInput";
            Description = "Input for creating a new Statistic account";

            Field(_ => _.Country).Description("The desired Statistic Country.");
            Field(_ => _.Population).Description("Total Population of the country");
            Field(_ => _.Year).Description("Year for statistic");
            Field(_ => _.Area).Description("Area in square kilometers");
        }
    }
}
