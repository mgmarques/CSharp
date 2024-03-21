using System.Globalization;
using BasicInventorySystem.Entities.Enums;

namespace BasicInventorySystem.Entities
{
    public class Electronics : Item
	{
        public Electronic Kind { get; set; }
        public string ?Model { get; set; }
        public DateTime ManufactureDate { get; set; }

        public Electronics()
        {
        }

        public Electronics(string name, string description, float weight,
            double price, Electronic kind, string model,
            DateTime manufactureDate) : base(name, description, weight, price)
        {
            Kind = kind;
            Model = model;
            ManufactureDate = manufactureDate;
        }

        public override string PriceTag()
        {
            return Name
                + "\n (" + Kind + ") - " + Model
                + "\n$ " + Price.ToString("F2", CultureInfo.InvariantCulture)
                + "\n (Manufacture date: "
                + ManufactureDate.ToString("MM/dd/yyyy")
                + ")";
        }
    }
}
