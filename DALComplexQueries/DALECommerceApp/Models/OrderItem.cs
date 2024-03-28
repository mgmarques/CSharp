using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DALECommerceApp.Models;

[Table("OrderItems")]
public class OrderItem
{
    [Key]
    [Column(Order = 1)]
    public long OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order Order { get; set; }

    [Key]
    [Column(Order = 2)]
    public long ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    [Required(ErrorMessage = "This field {0} is mandatory")]
    [Range(1, 1000, ErrorMessage = "Invalid Value")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "This field {0} is mandatory")]
    [Range(0.01, int.MaxValue, ErrorMessage = "Invalid Value")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Price { get; set; }

    [NotMapped]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double InvoiceItemAmount
    {
        get
        {
            return Quantity * Price;
        }
    }
}
