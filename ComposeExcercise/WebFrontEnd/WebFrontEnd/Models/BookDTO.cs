using System.ComponentModel.DataAnnotations;
using WebFrontEnd.Models.Enums;

namespace WebFrontEnd.Models;

public class BookDTO
{
    [Key]
    public long Id { get; set; }
    public Categories Category { get; set; }
    [DataType(DataType.Text)]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "The book title cannot exceed 200 characters and must be at least 3 characters long.")]
    [Required(ErrorMessage = "This field {0} is mandatory")]
    public string? Title { get; set; }
    [DataType(DataType.Text)]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "The book publisher name cannot exceed 20 characters and must be at least 3 characters long.")]
    [Required(ErrorMessage = "This field {0} is mandatory")]
    public string? Publisher  { get; set; } 
    public int Edition { get; set; }
    [Display(Name = "Edition Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "This field {0} is mandatory")]
    public DateTime EditionDate { get; set; }
    public Languages Language { get; set; }
    public int PaperbackPages { get; set; }
    [DataType(DataType.Text)]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "The book ISBN_10 must be 10 characters long.")]
    public string? ISBN_10  { get; set; }
    [DataType(DataType.Text)]
    [StringLength(13, MinimumLength = 13, ErrorMessage = "The book ISBN_13 must be 13 characters long.")]
    public string? ISBN_13 { get; set; }
    public double Weight { get; set; }
    public string? Dimensions { get; set; } 
}