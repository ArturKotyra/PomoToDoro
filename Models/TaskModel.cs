using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PomoToDoro.Models;

public class TaskModel
{
    [Key]
    public int TaskId { get; set; }
    [DisplayName("Tytuł zadania:")]
    public String Name { get; set; }
    public bool IsDone { get; set; }
}