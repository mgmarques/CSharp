using System;
using BasicInventorySystem.Entities;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using BasicInventorySystem.Entities.Enums;

namespace BasicInventorySystem.Entities
{
	public class Furnitures : Item
    {
        public Furniture Kind { get; set; }
        public HomeLocals HomeLocal { get; set; }
        public DateTime ManufactureDate { get; set; }

        public Furnitures()
		{
		}

        public Furnitures(string name, string description, float weight,
            double price, Furniture kind, HomeLocals homeLocal,
            DateTime manufactureDate) : base(name, description, weight, price)
        {
            Kind = kind;
            HomeLocal = homeLocal;
            ManufactureDate = manufactureDate;
        }


        public override string PriceTag()
        {
            return Name
            + "\nKind: " + Kind + " Local: " + HomeLocal
            + "\n$ " + Price.ToString("F2", CultureInfo.InvariantCulture)
            + "\n (Manufacture date: "
            + ManufactureDate.ToString("MM/dd/yyyy")
            + ")";
        }
    }
}
