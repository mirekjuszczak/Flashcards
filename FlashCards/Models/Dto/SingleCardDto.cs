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

    /// <summary>
    /// Creates DTO from SingleCard and Category
    /// </summary>
    public static SingleCardDto FromCardAndCategory(SingleCard card, Category? category)
        => new SingleCardDto
        {
            Id = card.Id,
            Phrase = card.Phrase,
            Translation = card.Translation,
            Example = card.Example,
            CategoryId = card.CategoryId,
            CategoryName = category?.Name ?? "Unknown",
            LearningProgress = card.LearningProgress,
            Favourite = card.Favourite
        };


    /// <summary>
    /// Converts DTO back to SingleCard model
    /// </summary>
    public SingleCard ToSingleCard()
        => new SingleCard
        {
            Id = Id,
            Phrase = Phrase,
            Translation = Translation,
            Example = Example,
            CategoryId = CategoryId,
            LearningProgress = LearningProgress,
            Favourite = Favourite
        };
}