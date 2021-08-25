using Dabble.GraphQL;
using Dabble.GraphQL.Types;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Dabble.Web.Extensions
{
    internal static class GraphQlExtensions
    {
        /// <summary>
        /// Adds GraphQL services to the app's service collection
        /// </summary>
        public static void AddGraphQl(this IServiceCollection services)
        {
            services.AddTransient<IQueryResolver, QueryResolver>();

            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddSingleton<UserType>();
            services.AddSingleton<UserInputType>();
            services.AddSingleton<StatisticType>();
            services.AddSingleton<StatisticInputType>();
            services.AddSingleton<LoginInputType>();
            services.AddSingleton<TaskListType>();
            services.AddSingleton<TaskListInputType>();
            services.AddSingleton<TaskItemType>();
            services.AddSingleton<TaskItemInputType>();

            // ToDo use singletons
            services.AddScoped<DabbleQuery>();
            services.AddScoped<DabbleMutation>();
            services.AddScoped<ISchema, DabbleSchema>(_ => new DabbleSchema(
                new FuncDependencyResolver(_.GetRequiredService)
            ));

            services.AddGraphQl(schema =>
            {
                schema.SetQueryType<DabbleQuery>();
                schema.SetMutationType<DabbleMutation>();
            });
        }
    }
}
