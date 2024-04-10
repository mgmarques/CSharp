using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;
using WebApi.Models.Enums;
using System;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApi.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<BookContext>>()))
        {
            // Look for any book.
            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }
            context.Books.AddRange(
                new Book
                {
                    Id = 1,
                    Category = Categories.ActionAdventure,
                    Title = "How to Create Action Fantasy and Adventure Comics",
                    Publisher = "North Ligth Books",
                    Edition = 1,
                    EditionDate = DateTime.Parse("1996-04-02"),
                    Language = Languages.English,
                    PaperbackPages = 143,
                    ISBN_10 = "0891346616",
                    ISBN_13 = "9780891346616",
                    Weight = 0.7,
                    Dimensions = "25 x 15 x 1.25"
                },
                new Book
                {
                    Id = 2,
                    Category = Categories.ActionAdventure,
                    Title = "How to Create Action Fantasy and Adventure Comics",
                    Publisher = "Atheuneau",
                    Edition = 2,
                    EditionDate = DateTime.Parse("1988-11-12"),
                    Language = Languages.Portuguese,
                    PaperbackPages = 363,
                    ISBN_10 = "0000000000",
                    ISBN_13 = "0000000000000",
                    Weight = 0.87,
                    Dimensions = "20 x 12 x 2.45"
                },
                new Book
                {
                    Id = 3,
                    Category = Categories.IT,
                    Title = "Apps and Services with .NET 8: Build practical projects with Blazor, .NET MAUI, gRPC, GraphQL, and other enterprise technologies",
                    Publisher = "Packt Publishing",
                    Edition = 2,
                    EditionDate = DateTime.Parse("2023-12-12"),
                    Language = Languages.English,
                    PaperbackPages = 798,
                    ISBN_10 = "183763713X",
                    ISBN_13 = "978-183763713X",
                    Weight = 3.01,
                    Dimensions = "9.25 x 7.52 x 1.59"
                },
                new Book
                {
                    Id = 4,
                    Category = Categories.IT,
                    Title = "Blazor 8.0: Moderne Webanwendungen und hybride Cross-Platform-Apps mit .NET 8.0, C# 12.0 und Visual Studio 2022 (.NET 8.0-Fachbuchreihe von Dr. Holger Schwichtenberg 3)",
                    Publisher = "IT Visions",
                    Edition = 8,
                    EditionDate = DateTime.Parse("2023-07-23"),
                    Language = Languages.German,
                    PaperbackPages = 1647,
                    ISBN_10 = "183763713X",
                    ISBN_13 = "ASN-B0CLKZJKTX",
                    Weight = 5.75,
                    Dimensions = "9.25 x 7.52 x 3.09"
                },
                new Book
                {
                    Id = 5,
                    Category = Categories.Science,
                    Title = "The Hierarchy of Energy in Architecture: Emergy Analysis",
                    Publisher = "Routledge",
                    Edition = 1,
                    EditionDate = DateTime.Parse("2015-06-12"),
                    Language = Languages.English,
                    PaperbackPages = 167,
                    ISBN_10 = "1138803537",
                    ISBN_13 = "ASN-1138803537",
                    Weight = 1.75,
                    Dimensions = "8.00 x 6.00 x 1.10"
                }

            ) ;
            context.SaveChanges();
        }
    }
}