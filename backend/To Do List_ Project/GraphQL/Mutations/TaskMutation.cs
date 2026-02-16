using GraphQL;
using GraphQL.Types;
using todo.Models;
using todo.Services.Interfaces;

public class TaskMutation : ObjectGraphType
{
    public TaskMutation(ITaskService taskService)
    {
        Field<TaskType>("addTask")
       .Argument<NonNullGraphType<StringGraphType>>("text")
       .Argument<DateTimeGraphType>("dueDate")
       .Argument<IntGraphType>("categoryId")
       .Argument<IntGraphType>("storageTypeId")
       .ResolveAsync(async context =>
       {
           var text = context.GetArgument<string>("text");
           var dueDate = context.GetArgument<DateTime?>("dueDate");
           var categoryId = context.GetArgument<int?>("categoryId");
           var storageTypeId = context.GetArgument<int>("storageTypeId");

           var taskToAdd = new TaskModel
           {
               Text = text,
               Due_Date = dueDate,
               Category_Id = categoryId,
               Is_Completed = false,
               Created_At = DateTime.Now
           };

           return await taskService.AddTaskAsync(taskToAdd, storageTypeId);
       });

        Field<BooleanGraphType>("clearTasks")
            .Argument<IntGraphType>("storageTypeId")
            .ResolveAsync(async context =>
            {
                var storageTypeId = context.GetArgument<int>("storageTypeId");

                await taskService.ClearTasksAsync(storageTypeId);

                return true;
            });

        Field<BooleanGraphType>("updateTask")
            .Argument<NonNullGraphType<IntGraphType>>("id")
            .Argument<IntGraphType>("storageTypeId")
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");
                var storageTypeId = context.GetArgument<int>("storageTypeId");

                return await taskService.MarkTaskAsCompleteAsync(id, storageTypeId);
            });
    }
}