using System.ComponentModel.DataAnnotations;

namespace todo.Models
{
    public class TaskModel
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "Назва завдання обов'язкова")]
        [StringLength(100, ErrorMessage = "Назва не може перевищувати 100 символів")]
        public required string Text { set; get; }
        public DateTime? Due_Date { get; set; }
        public int? Category_Id { set; get; }
        public bool Is_Completed { get; set; } = false;
        public DateTime Created_At {  get; set; }= DateTime.Now;
        public DateTime? Completed_At { get; set; }
    }
}
