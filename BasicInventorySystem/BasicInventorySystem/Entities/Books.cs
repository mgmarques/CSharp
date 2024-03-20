using BasicInventorySystem.Entities.Enums;
using System.Globalization;

namespace BasicInventorySystem.Entities
{
	public class Books : Item
    {
        public Genres Genre { get; set; }
        public string ?Author { get; set; }
        public string ?Publisher { get; set; }
        public DateTime PublicationDate { get; set; }

        public Books()
        {
        }

        public Books(string name, string description, float weight,
            double price, Genres genre, string author, string publisher,
            DateTime publicationDate) : base(name, description, weight, price)
        {
            Genre = genre;
            Author = author;
            Publisher = publisher;
            PublicationDate = publicationDate;
        }

        public override string PriceTag()
        {
            return Name
                + "\nAutor: " + Author + " Genre: " + Genre.ToString()
                + " \nPublisher: " + Publisher
                + "\n$ " + Price.ToString("F2", CultureInfo.InvariantCulture)
                + "\n (Publication date: "
                + PublicationDate.ToString("MM/dd/yyyy")
                + ")";
        }
    }
}