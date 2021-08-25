using Dabble.GraphQL.Types;
using GraphQL.Types;

namespace Dabble.GraphQL
{
    /// <summary>
    /// Represents root of the graph for query operations
    /// </summary>
    public class DabbleQuery : ObjectGraphType
    {
        /// <inheritdoc />
        public DabbleQuery(IQueryResolver resolver)
        {
            Name = nameof(DabbleQuery);

            Field<UserType>("user",
                "Get user account information by id",
                new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" }),
                resolver.GetUserAsync
            );

            Field<StatisticType>("statistic",
                "Get statistic information by country and year",
                new QueryArguments(
                    new QueryArgument<StringGraphType>
                    {
                        Name = "country",
                        Description = "Country of the Statistic ",
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "year",
                        Description = "Year of the Statistic",
                    }
                ),
                resolver.GetStatisticAsync
            );
        }
    }
}
