using GraphQL.Types;
using todo.Models;

public class TaskType : ObjectGraphType<TaskModel>
{
    public TaskType()
    {
        Name = "Task";

        Field(x => x.Id);

        Field(x => x.Text);

        Field<DateTimeGraphType>("dueDate").Resolve(ctx => ctx.Source.Due_Date.HasValue);

        Field<IntGraphType>("categoryId").Resolve(ctx => ctx.Source.Category_Id.HasValue);

        Field<BooleanGraphType>("isCompleted").Resolve(ctx => ctx.Source.Is_Completed);

        Field<DateTimeGraphType>("createdAt").Resolve(ctx => ctx.Source.Created_At);

        Field<DateTimeGraphType>("completedAt").Resolve(ctx => ctx.Source.Completed_At.HasValue);
    }
}
