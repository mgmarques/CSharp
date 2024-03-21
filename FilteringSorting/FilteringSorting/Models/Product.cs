using System.ComponentModel.DataAnnotations;
using FilteringSorting.Models.Enums;

namespace FilteringSorting.Models
{
	public class Product
	{
        public long Id { get; set; }
        public string Name { get; set; }
        public Categories Category { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Price { get; set; }

        public Product(long id, string name, Categories category, double price)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
        }
    }
}

