using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DALECommerceApp.Models.Enums;

namespace DALECommerceApp.Models;

[Table("Orders")]
public class Order
{
    [Key]
    public long OrderId { get; set; }

    public long CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }

    public OrderStatus Status { get; set; }
    public SaleStatus SaleStatus { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime Created { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime DateToDelivery { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
    public DateTime? DeliveryDate { get; set; }

    public virtual ICollection<OrderItem>? OrderItems { get; set; }
}
