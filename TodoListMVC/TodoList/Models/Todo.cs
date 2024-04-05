using System.ComponentModel.DataAnnotations;

namespace TodoList.Models;

public class Todo
{
    [Key]
    public int Id { get; set; }
    [DataType(DataType.Text)]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "The task name cannot exceed 50 characters and must be at least 3 characters long.")]
    [Required(ErrorMessage = "This field {0} is mandatory")]
    public string? Task { get; set; }
    [Display(Name = "Planned Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "This field {0} is mandatory")]
    public DateTime PlannedDate { get; set; }
    [Display(Name = "Due Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DateGreaterThan("PlannedDate")]
    public DateTime? DueDate { get; set; }
    [RegularExpression(@"^[A-Z]\d", ErrorMessage = "The priority must start with a letter in upper case followed by a number.")]
    [StringLength(2, ErrorMessage = "The priority should be within 2 characters")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "This field {0} is mandatory")]
    public string? Priority { get; set; }
    public bool Done { get; set; }
}