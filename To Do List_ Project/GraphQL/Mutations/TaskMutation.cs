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
            .Argument<DateTimeGraphType>("dueDate", nullable: true)
            .Argument<IntGraphType>("categoryId", nullable: true)
            .Argument<StringGraphType>("source", nullable: true)
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

                var task = new TaskModel
                {
                    Text = text,
                    Due_Date = dueDate,
                    Category_Id = categoryId ?? 0,
                    Is_Completed = false,
                    Created_At = DateTime.Now
                };

                taskService.AddTask(task);

                var createdTask = taskService.GetTaskById(task.Id);
                return createdTask;
            });
    }
}