using System.Text.Json.Serialization;
using BooksMvcClient.Models.Enums;

namespace BooksMvcClient.Models;


public record class Book(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("category")] Categories Category,
    [property: JsonPropertyName("publisher")] string Publisher,
    [property: JsonPropertyName("edition")] int Edition,
    [property: JsonPropertyName("language")] Languages Language,
    [property: JsonPropertyName("paperbackPages")] int PaperbackPages,
    [property: JsonPropertyName("isbN_10")] string ISBN_10,
    [property: JsonPropertyName("isbN_13")] string ISBN_13,
    [property: JsonPropertyName("weight")] double Weight,
    [property: JsonPropertyName("dimensions")] string Dimensions,
    [property: JsonPropertyName("editionDate")] DateTime EditionDateUtc)
{
    public DateTime EditionDate => EditionDateUtc.ToLocalTime();
}
