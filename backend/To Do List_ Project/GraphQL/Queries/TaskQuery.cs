using GraphQL.Types;
using GraphQL;
using To_Do_List__Project.DatabaseServices.Interfaces;
using To_Do_List__Project.Database.SQLRepositories;
using To_Do_List__Project.Database.XMLRepositories;

namespace To_Do_List__Project.GraphQL.Queries
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

                    ITaskService taskService = source switch
                    {
                        "xml" => services!.GetRequiredService<XMLTaskRepository>(),
                        _ => services!.GetRequiredService<SQLTaskRepository>()
                    };

                    return status switch
                    {
                        "active" => taskService.GetActiveTasks(),
                        "completed" => taskService.GetCompletedTasks(),
                        _ => taskService.GetAllTasks()
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

                    ITaskService taskService = source switch
                    {
                        "xml" => services!.GetRequiredService<XMLTaskRepository>(),
                        _ => services!.GetRequiredService<SQLTaskRepository>()
                    };

                    return taskService.GetTaskById(id);
                });
        }
    }
}
