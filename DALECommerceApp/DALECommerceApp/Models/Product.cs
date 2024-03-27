using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DALECommerceApp.Models.Enums;

namespace DALECommerceApp.Models;


[Table("Products")]
public class Product
	{
    [Key]
    public long ProductId { get; set; }

    [Column("ProductName", TypeName = "ntext")]
    [Required(ErrorMessage = "This field {0} is mandatory")]
    [MaxLength(50, ErrorMessage = "Product Name must be 3 characters or up to 50"), MinLength(3)]
    public string Name { get; set; }

    public Categories Category { get; set; }

    [Required(ErrorMessage = "This field {0} is mandatory")]
    [Range(0.01, int.MaxValue, ErrorMessage = "Invalid Value")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double UnitPrice { get; set; }

    public virtual ICollection<OrderItem>? OrderItems { get; set; }

}

