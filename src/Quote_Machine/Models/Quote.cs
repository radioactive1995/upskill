using System.ComponentModel.DataAnnotations;

namespace Quote_Machine.Models;

public class QuoteDto
{
    [Required]
    public string Content { get; set; } = string.Empty;
    [Required]
    public string Author { get; set; } = string.Empty;
}

public class Quote
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public required string Author { get; set; }
}
