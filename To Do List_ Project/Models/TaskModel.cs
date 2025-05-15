namespace To_Do_List__Project.Models
{
    public class TaskModel
    {
        public int Id { set; get; }
        public required string Text { set; get; }
        public DateTime? Due_Date { get; set; }
        public int? Category_Id { set; get; }
        public bool? Is_Completed { get; set; } = false;
        public DateTime Created_At {  get; set; }= DateTime.Now;
        public DateTime? Completed_At { get; set; }
    }
}
