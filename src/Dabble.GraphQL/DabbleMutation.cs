using Dabble.GraphQL.Types;
using GraphQL.Types;

namespace Dabble.GraphQL
{
    /// <summary>
    /// Represents root of the graph for mutation operations
    /// </summary>
    public class DabbleMutation : ObjectGraphType
    {
        /// <inheritdoc />
        public DabbleMutation(IQueryResolver resolver)
        {
            Name = nameof(DabbleMutation);
            Description = "Root of the graph for mutation operations";

            Field<StatisticType>(
                "createStatistic",
                "Create a new Statistic account",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StatisticInputType>> { Name = "statistic" }
                ),
                resolver.CreateStatisticAsync
            );

            Field<NonNullGraphType<BooleanGraphType>>(
                "deleteStatistic",
                "Delete a  Statistic owned by the user",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StatisticInputType>> { Name = "statistic" }
                ),
                resolver.DeleteStatisticAsync
            );

            Field<UserType>(
                "createUser",
                "Create a new user account",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }
                ),
                resolver.CreateUserAsync
            );

            Field<UserType>(
                "login",
                "Login to a user account",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "login" }
                ),
                resolver.LoginAsync
            );

            Field<TaskListType>(
                "createList",
                "Create a new task list.",
                // ToDo use auth tokens and current logged in user will be the
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "owner",
                        Description = "Username of the list owner",
                    },
                    new QueryArgument<NonNullGraphType<TaskListInputType>>
                    {
                        Name = "list",
                        Description = "Parameters for creating a new task list",
                    }
                ),
                resolver.CreateTaskListAsync
            );

            Field<NonNullGraphType<BooleanGraphType>>(
                "deleteList",
                "Delete a task list owned by the user",
                // ToDo use auth tokens and current logged in user will be the
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "owner",
                        Description = "Username of the list owner",
                    },
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "list",
                        Description = "Name of the list to be deleted",
                    }
                ),
                resolver.DeleteTaskListAsync
            );

            Field<TaskItemType>(
                "createTask",
                "Create a new task in the list",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "ownerId",
                        Description = "Username of the list owner"
                    },
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "listId",
                        Description = "Unique name of the task list"
                    },
                    new QueryArgument<NonNullGraphType<TaskItemInputType>>
                    {
                        Name = "task",
                        Description = "Parameters for creating a new task item",
                    }
                ),
                resolver.CreateTaskItemAsync
            );

            Field<NonNullGraphType<BooleanGraphType>>(
                "deleteTask",
                "Delete a task item for the list",
                // ToDo use auth tokens and current logged in user will be the
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "ownerId",
                        Description = "Username of the list owner",
                    },
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "listId",
                        Description = "Unique name of the task list"
                    },
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "taskId",
                        Description = "Unique name of the task item to be deleted"
                    }
                ),
                resolver.DeleteTaskItemAsync
            );
        }
    }
}
