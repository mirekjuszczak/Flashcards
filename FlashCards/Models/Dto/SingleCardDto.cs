namespace FlashCards.Models.Dto;

/// <summary>
/// Data Transfer Object for SingleCard with enriched category information
/// Used to avoid multiple database calls when displaying cards with category names
/// </summary>
public class SingleCardDto
{
    public string? Id { get; set; }
    public string Phrase { get; set; } = string.Empty;
    public string Translation { get; set; } = string.Empty;
    public string? Example { get; set; }
    public string CategoryId { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public LearningProgress LearningProgress { get; set; } = LearningProgress.NotStarted;
    public bool Favourite { get; set; }
}