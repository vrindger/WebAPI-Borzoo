using System.Threading.Tasks;
using Dabble.Data.Abstractions.Entities;
using Dabble.GraphQL.Models;
using GraphQL.Types;

namespace Dabble.GraphQL
{
    /// <summary>
    /// Contains resolver functions for GraphQL operations
    /// </summary>
    public interface IQueryResolver
    {
        /// <summary>
        /// Creates a new statistic
        /// </summary>
        Task<StatisticDto> CreateStatisticAsync(ResolveFieldContext<object> context);

        /// <summary>
        /// Deletes a statistic 
        /// </summary>
        Task<bool> DeleteStatisticAsync(ResolveFieldContext<object> context);

        /// <summary>
        /// Gets a statistic 
        /// </summary>
        Task<StatisticDto> GetStatisticAsync(ResolveFieldContext<object> context);
        /// <summary>
        /// Creates a new user account
        /// </summary>
        Task<UserDto> CreateUserAsync(ResolveFieldContext<object> context);

        /// <summary>
        /// Creates a user login
        /// </summary>
        Task<UserDto> LoginAsync(ResolveFieldContext<object> context);

        /// <summary>
        /// Gets a user account
        /// </summary>
        Task<UserDto> GetUserAsync(ResolveFieldContext<object> context);

        /// <summary>
        /// Creates a new task list
        /// </summary>
        Task<TaskList> CreateTaskListAsync(ResolveFieldContext<object> context);

        /// <summary>
        /// Deletes a task list
        /// </summary>
        Task<bool> DeleteTaskListAsync(ResolveFieldContext<object> context);

        /// <summary>
        /// Resolves a single task list for user
        /// </summary>
        Task<TaskList> GetTaskListForUserAsync(ResolveFieldContext<UserDto> context);

        /// <summary>
        /// Gets all the task lists owned by a user
        /// </summary>
        Task<TaskList[]> GetAllTaskListsForUserAsync(ResolveFieldContext<UserDto> context);

        /// <summary>
        /// Creates a new task item
        /// </summary>
        Task<TaskItem> CreateTaskItemAsync(ResolveFieldContext<object> context);

        /// <summary>
        /// Gets all the task items in a list
        /// </summary>
        Task<TaskItem[]> GetTaskItemsForListAsync(ResolveFieldContext<TaskList> context);

        /// <summary>
        /// Deletes a task item from the list
        /// </summary>
        Task<bool> DeleteTaskItemAsync(ResolveFieldContext<object> context);
    }
}
