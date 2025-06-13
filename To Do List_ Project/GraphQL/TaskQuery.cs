using GraphQL.Types;
using GraphQL;
using To_Do_List__Project.DatabaseServices.Interfaces;


namespace To_Do_List__Project.GraphQL
{
    public class TaskQuery : ObjectGraphType
    {
        public TaskQuery(ITaskService taskService)
        {
            Field<ListGraphType<TaskType>>("tasks")
            .Argument<StringGraphType>("status", "active або completed або all")
            .Resolve(context =>
            {
                var status = context.GetArgument<string>("status");
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
                .Resolve(context =>
                {
                    var id = context.GetArgument<int>("id");
                    return taskService.GetTaskById(id);
                });
        }
    }
}