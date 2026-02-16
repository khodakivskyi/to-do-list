using GraphQL;
using GraphQL.Types;
using todo.Services.Interfaces;

namespace todo.GraphQL.Queries
{
    public class TaskQuery : ObjectGraphType
    {
        public TaskQuery(ITaskService taskService)
        {
            Field<ListGraphType<TaskType>>("tasksByCompletionStatus")
                .Argument<IntGraphType>("statusTypeId")
                .Argument<IntGraphType>("storageTypeId")
                .ResolveAsync(async context =>
                {
                    var statusTypeId = context.GetArgument<int>("statusTypeId");
                    var storageTypeId = context.GetArgument<int>("storageTypeId");

                    return await taskService.GetTasksByCompletionStatusAsync(statusTypeId, storageTypeId);
                });

            Field<ListGraphType<TaskType>>("allTasks")
               .Argument<IntGraphType>("storageTypeId")
               .ResolveAsync(async context =>
               {
                   var storageTypeId = context.GetArgument<int>("storageTypeId");

                   return await taskService.GetAllTasksAsync(storageTypeId);
               });

            Field<TaskType>("task")
                .Description("Отримати завдання за ID")
                .Argument<NonNullGraphType<IntGraphType>>("id")
               .Argument<IntGraphType>("storageTypeId")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var storageTypeId = context.GetArgument<int>("storageTypeId");

                    return await taskService.GetTaskByIdAsync(id, storageTypeId);
                });
        }
    }
}
