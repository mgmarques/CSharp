using BasicInventorySystem.Entities;
using BasicInventorySystem.Entities.Enums;
using System.Globalization;

namespace BasicInventorySystem.Unit.Tests;

public class BasicInventorySystemUnitTest
{ 
    public class itemTest
    {

        [Theory]
        [InlineData("test", "tests book of the books", 0.9, 12.99,
            Genres.Mystery, "Missinyou", "Alone in the dark", "10/20/1971")]
        public void BooksTest(string name, string description, float weight,
            double price, Genres genre, string author, string publisher,
            string date)
        {
            DateTime publicationDate = DateTime.Parse(date);
            Books book = new Books(name, description, weight, price, genre,
                author, publisher, publicationDate);
          
            string expected = name
                + "\nAutor: " + author + " Genre: Mystery"
                + " \nPublisher: " + publisher
                + "\n$ " + price.ToString("F2", CultureInfo.InvariantCulture)
                + "\n (Publication date: "
                + publicationDate.ToString("MM/dd/yyyy")
                + ")";
            Assert.Equal(expected, book.PriceTag());
        }

        [Theory]
        [InlineData("X-Box", "Super Game", 0.75, 359.99, Electronic.Console,
            "X-BOX S", "01/01/2023")]
        public void ElectronicsTest(string name, string description, float weight,
            double price, Electronic kind, string model, string date)
        {
            DateTime manufactureDate = DateTime.Parse(date);
            Electronics electronic = new Electronics(name, description, weight,
                price, kind, model, manufactureDate);

            string expected = name
                + $"\n (Console) - {model}"
                + $"\n$ {price.ToString("F2", CultureInfo.InvariantCulture)}"
                + $"\n (Manufacture date: "
                + $"{manufactureDate.ToString("MM/dd/yyyy")})";
            Assert.Equal(expected, electronic.PriceTag());
        }

        [Theory]
        [InlineData("Super Queen", "Bed Queen soze extra confort nasa", 10.0,
            1199.99, Furniture.Bed, HomeLocals.Bedroom, "01/10/2024")]
        public void FurnituresTest(string name, string description, float weight,
            double price, Furniture kind, HomeLocals homeLocal, string date)
        {
            DateTime manufactureDate = DateTime.Parse(date);
            Furnitures furniture = new Furnitures(name, description, weight,
                            price, kind, homeLocal, manufactureDate);

            string expected = name
                + "\nKind: Bed Local: Bedroom"
                + $"\n$ {price.ToString("F2", CultureInfo.InvariantCulture)}"
                + $"\n (Manufacture date: " 
                + $"{ manufactureDate.ToString("MM/dd/yyyy")})";
            Assert.Equal(expected, furniture.PriceTag());
        }
    }
}