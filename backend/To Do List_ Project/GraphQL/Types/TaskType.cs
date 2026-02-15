using GraphQL.Types;
using todo.Models;

public class TaskType : ObjectGraphType<TaskModel>
{
    public TaskType()
    {
        Name = "Task";

        Field(x => x.Id).Description("Унікальний ідентифікатор завдання");
        Field(x => x.Text).Description("Текст завдання");

        Field<DateTimeGraphType>("dueDate")
            .Description("Кінцева дата")
        .Resolve(ctx => ctx.Source.Due_Date.HasValue ? ctx.Source.Due_Date.Value : null);

        Field<IntGraphType>("categoryId")
            .Description("ID категорії")
        .Resolve(ctx => ctx.Source.Category_Id.HasValue ? ctx.Source.Category_Id.Value : null);

        Field<BooleanGraphType>("isCompleted")
            .Description("Чи виконане завдання")
            .Resolve(ctx => ctx.Source.Is_Completed ?? false);

        Field<DateTimeGraphType>("createdAt")
            .Description("Дата створення")
            .Resolve(ctx => ctx.Source.Created_At);

        Field<DateTimeGraphType>("completedAt")
            .Description("Дата завершення")
        .Resolve(ctx => ctx.Source.Completed_At.HasValue ? ctx.Source.Completed_At.Value : null);
    }
}
