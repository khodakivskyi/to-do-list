using GraphQL;
using GraphQL.Types;
using todo.Repositories.SQLRepositories;
using todo.Repositories.XMLRepositories;
using todo.Repositories.Interfaces;
using todo.Models;

public class TaskMutation : ObjectGraphType
{
    public TaskMutation()
    {
        Field<TaskType>("addTask")
       .Argument<NonNullGraphType<StringGraphType>>("text")
       .Argument<DateTimeGraphType>("dueDate")
       .Argument<IntGraphType>("categoryId")
       .Argument<StringGraphType>("source")
       .Resolve(context =>
       {
           var text = context.GetArgument<string>("text");
           var dueDate = context.GetArgument<DateTime?>("dueDate");
           var categoryId = context.GetArgument<int?>("categoryId");
           var source = context.GetArgument<string>("source")?.ToLower();

           var services = context.RequestServices;

           ITaskRepository taskService = source switch
           {
               "xml" => services!.GetRequiredService<todo.Repositories.XMLRepositories.XmlTaskRepository>(),
               _ => services!.GetRequiredService<todo.Repositories.SQLRepositories.SqlTaskRepository>()
           };

           var taskToAdd = new TaskModel
           {
               Text = text,
               Due_Date = dueDate,
               Category_Id = categoryId,
               Is_Completed = false,
               Created_At = DateTime.Now
           };

           var createdTask = taskService.AddTaskAsync(taskToAdd);

           return createdTask;
       });

        Field<BooleanGraphType>("clearTasks")
            .Argument<StringGraphType>("source")
            .Resolve(context =>
            {
                var source = context.GetArgument<string>("source")?.ToLower();

                var services = context.RequestServices;

                ITaskRepository taskService = source switch
                {
                    "xml" => services!.GetRequiredService<todo.Repositories.XMLRepositories.XmlTaskRepository>(),
                    _ => services!.GetRequiredService<todo.Repositories.SQLRepositories.SqlTaskRepository>()
                };

                bool res = taskService.ClearTasksAsync();

                return res;
            });

        Field<BooleanGraphType>("updateTask")
            .Argument<NonNullGraphType<IntGraphType>>("id")
            .Argument<StringGraphType>("source")
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("id");
                var source = context.GetArgument<string>("source")?.ToLower();

                var services = context.RequestServices;

                ITaskRepository taskService = source switch
                {
                    "xml" => services!.GetRequiredService<todo.Repositories.XMLRepositories.XmlTaskRepository>(),
                    _ => services!.GetRequiredService<todo.Repositories.SQLRepositories.SqlTaskRepository>()
                };

                var taskToUpdate = taskService.GetTaskByIdAsync(id);
                if (taskToUpdate == null)
                {
                    return false;
                }

                taskToUpdate.Is_Completed = true;
                taskToUpdate.Completed_At = DateTime.Now;

                bool res = taskService.UpdateTask(taskToUpdate);

                return res;
            });
    }
}