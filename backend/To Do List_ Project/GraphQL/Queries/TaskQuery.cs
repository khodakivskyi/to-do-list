using GraphQL.Types;
using GraphQL;
using todo.Repositories.Interfaces;
using todo.Repositories.SQLRepositories;
using todo.Repositories.XMLRepositories;

namespace todo.GraphQL.Queries
{
    public class TaskQuery : ObjectGraphType
    {
        public TaskQuery()
        {
            Field<ListGraphType<TaskType>>("tasks")
                .Argument<StringGraphType>("status", "active або completed або all")
                .Argument<StringGraphType>("source", "sql або xml")
                .Resolve(context =>
                {
                    var status = context.GetArgument<string>("status")?.ToLower();
                    var source = context.GetArgument<string>("source")?.ToLower();

                    var services = context.RequestServices;

                    ITaskRepository taskService = source switch
                    {
                        "xml" => services!.GetRequiredService<Repositories.XMLRepositories.XmlTaskRepository>(),
                        _ => services!.GetRequiredService<Repositories.SQLRepositories.SqlTaskRepository>()
                    };

                    return status switch
                    {
                        "active" => taskService.GetActiveTasksAsync(),
                        "completed" => taskService.GetCompletedTasksAsync(),
                        _ => taskService.GetAllTasksAsync()
                    };
                });

            Field<TaskType>("task")
                .Description("Отримати завдання за ID")
                .Argument<NonNullGraphType<IntGraphType>>("id", "ID завдання")
                .Argument<StringGraphType>("source", "sql або xml")
                .Resolve(context =>
                {
                    var id = context.GetArgument<int>("id");
                    var source = context.GetArgument<string>("source")?.ToLower();

                    var services = context.RequestServices;

                    ITaskRepository taskService = source switch
                    {
                        "xml" => services!.GetRequiredService<Repositories.XMLRepositories.XmlTaskRepository>(),
                        _ => services!.GetRequiredService<Repositories.SQLRepositories.SqlTaskRepository>()
                    };

                    return taskService.GetTaskByIdAsync(id);
                });
        }
    }
}
