using BookApi.Models.Enums;

namespace BookApi.Models;

public class Book
{
    public long Id { get; set; }
    public Categories Category { get; set; }
    public string? Title { get; set; }
    public string? Publisher  { get; set; } 
    public int Edition { get; set; }
    public DateTime EditionDate { get; set; }
    public Languages Language { get; set; }
    public int PaperbackPages { get; set; }
    public string? ISBN_10  { get; set; }
    public string? ISBN_13 { get; set; }
    public double Weight { get; set; }
    public string? Dimensions { get; set; } 
}