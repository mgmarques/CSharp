using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "This field {0} is mandatory")]
    [StringLength(60, MinimumLength = 3)]
    public string? Title { get; set; }
    [Required(ErrorMessage = "This field {0} is mandatory")]
    [DataType(DataType.Date)]
    [Display(Name = "Release Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime ReleaseDate { get; set; }
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required(ErrorMessage = "This field {0} is mandatory")]
    [StringLength(30)]
    public string? Genre { get; set; }
    [Required(ErrorMessage = "This field {0} is mandatory")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    [Required(ErrorMessage = "This field {0} is mandatory")]
    public string? Rating { get; set; }
}
