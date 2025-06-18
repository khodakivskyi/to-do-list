using GraphQL;
using GraphQL.Types;
using To_Do_List__Project.Database.SQLRepositories;
using To_Do_List__Project.Database.XMLRepositories;
using To_Do_List__Project.DatabaseServices.Interfaces;
using To_Do_List__Project.Models;

public class TaskMutation : ObjectGraphType
{
    public TaskMutation()
    {
        Field<TaskType>("createTask")
            .Argument<NonNullGraphType<StringGraphType>>("text")
            .Argument<DateTime?>("dueDate", nullable: true)
            .Argument<int?>("categoryId", nullable: true)
            .Argument<string>("source")
            .Resolve(context =>
            {
                var text = context.GetArgument<string>("text");
                var dueDate = context.GetArgument<DateTime?>("dueDate");
                var categoryId = context.GetArgument<int?>("categoryId");
                var source = context.GetArgument<string>("source")?.ToLower();

                var services = context.RequestServices;

                ITaskService taskService = source switch
                {
                    "xml" => services!.GetRequiredService<XMLTaskRepository>(),
                    _ => services!.GetRequiredService<SQLTaskRepository>()
                };

                var taskToAdd = new TaskModel
                {
                    Text = text,
                    Due_Date = dueDate,
                    Category_Id = categoryId ?? 0,
                    Is_Completed = false,
                    Created_At = DateTime.Now
                };

                taskService.AddTask(taskToAdd);

                var createdTask = taskService.GetTaskById(taskToAdd.Id);
                return createdTask;
            });

        Field<BooleanGraphType>("clearTasks")
            .Argument<string>("source")
            .Resolve(context =>
            {
                var source = context.GetArgument<string>("source")?.ToLower();

                var services = context.RequestServices;

                ITaskService taskService = source switch
                {
                    "xml" => services!.GetRequiredService<XMLTaskRepository>(),
                    _ => services!.GetRequiredService<SQLTaskRepository>()
                };

                taskService.ClearTasks();

                return true;
            });

        Field<BooleanGraphType>("updateTask")
            .Argument<NonNullGraphType<IntGraphType>>("id")
            .Argument<string>("source")
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

                var taskToUpdate = taskService.GetTaskById(id);
                if (taskToUpdate == null)
                {
                    return false;
                }

                taskService.UpdateTask(taskToUpdate);

                return true;
            });
    }
}