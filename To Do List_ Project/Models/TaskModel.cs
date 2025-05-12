namespace To_Do_List__Project.Models
{
    public class TaskModel
    {
        public int Id { set; get; }
        public required string Text { set; get; }
        public DateTime Due_Date { get; set; }
        public int? Category_Id { set; get; }
        public bool? Is_Complited { get; set; } = false;
        public DateTime Created_At {  get; set; }= DateTime.Now;
        public DateTime? Complited_At { get; set; }
    }
}
