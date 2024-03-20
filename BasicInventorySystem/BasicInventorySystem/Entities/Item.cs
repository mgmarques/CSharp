using System.Globalization;


namespace BasicInventorySystem.Entities
{
    public class Item
    {
        public string Name { get; init; } = "No name";
        public string Description { get; init; } = "No description";
        public float Weight { get; init; }
        public double Price { get; init; }

        public Item()
        {
        }

        public Item(string name, string description, float weight, double price)
        {
            Name = name;
            Description = description;
            Price = price;
            Weight = weight;

        }

        public virtual string PriceTag()
        {
            return Name
            + " $ "
                + Price.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}