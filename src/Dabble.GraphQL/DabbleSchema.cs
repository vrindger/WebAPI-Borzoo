using GraphQL;
using GraphQL.Types;

namespace Dabble.GraphQL
{
    /// <summary>
    /// Represents the Dabble GraphQL schema
    /// </summary>
    public class DabbleSchema : Schema
    {
        /// <inheritdoc />
        public DabbleSchema(FuncDependencyResolver resolver)
        {
            DependencyResolver = resolver;
            Query = resolver.Resolve<DabbleQuery>();
            Mutation = resolver.Resolve<DabbleMutation>();
        }
    }
}
