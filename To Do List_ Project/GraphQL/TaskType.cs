using GraphQL.Types;
using To_Do_List__Project.Models;

public class TaskType : ObjectGraphType<TaskModel>
{
    public TaskType()
    {
        Name = "Task";

        Field(x => x.Id).Description("Унікальний ідентифікатор завдання");
        Field(x => x.Text).Description("Текст завдання");

        Field<DateTimeGraphType>("dueDate")
            .Description("Кінцева дата")
            .Resolve(ctx => ctx.Source.Due_Date);

        Field<IntGraphType>("categoryId")
            .Description("ID категорії")
            .Resolve(ctx => ctx.Source.Category_Id);

        Field<BooleanGraphType>("isCompleted")
            .Description("Чи виконане завдання")
            .Resolve(ctx => ctx.Source.Is_Completed ?? false);

        Field<DateTimeGraphType>("createdAt")
            .Description("Дата створення")
            .Resolve(ctx => ctx.Source.Created_At);

        Field<DateTimeGraphType>("completedAt")
            .Description("Дата завершення")
            .Resolve(ctx => ctx.Source.Completed_At);
    }
}
