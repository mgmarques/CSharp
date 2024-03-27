using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DALECommerceApp.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DALECommerceApp.Models;

[Table("Customers")]
public class Customer
{
    [Key]
    public long CustomerId { get; set; }

    [Required(ErrorMessage = "This field {0} is mandatory")]
    [MaxLength(50, ErrorMessage = "Client Name must be 3 characters or up to 50"), MinLength(3)]
    public string ClientName { get; set; }

    [Required(ErrorMessage = "This field {0} is mandatory")]
    public CustomerKinds CustomerKind { get; set; }

    [Required(ErrorMessage = "This field {0} is mandatory")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "This field {0} is mandatory")]
    public string Password { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime Created { get; set; }

    public virtual ICollection<Order>? Orders { get; set; }

}

