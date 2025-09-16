namespace FlashCards.Models.Dto;

public static class SingleCardAndDtoExtensions
{
    /// <summary>
    /// Creates DTO from SingleCard and Category
    /// </summary>
    public static SingleCardDto FromCardAndCategory(this SingleCard card, Category? category)
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
    public static SingleCard ToSingleCard(this SingleCardDto cardDto)
        => new SingleCard
        {
            Id = cardDto.Id,
            Phrase = cardDto.Phrase,
            Translation = cardDto.Translation,
            Example = cardDto.Example,
            CategoryId = cardDto.CategoryId,
            LearningProgress = cardDto.LearningProgress,
            Favourite = cardDto.Favourite
        };
}