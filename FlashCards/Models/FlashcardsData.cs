using System.Collections.ObjectModel;
using FlashCards.Models.Dto;

namespace FlashCards.Models;

/// <summary>
/// Container class holding both Cards and Categories collections
/// Provides efficient in-memory operations and easy data relationships
/// </summary>
public class FlashcardsData
{
    public ObservableCollection<SingleCard> Cards { get; set; } = new();
    public ObservableCollection<Category> Categories { get; set; } = new();

    /// <summary>
    /// Gets all cards belonging to a specific category
    /// </summary>
    public IEnumerable<SingleCard> GetCardsByCategory(string categoryId)
        => Cards.Where(card => card.CategoryId == categoryId);

    /// <summary>
    /// Gets category by ID
    /// </summary>
    public Category? GetCategory(string categoryId)
        => Categories.FirstOrDefault(c => c.Id == categoryId);

    /// <summary>
    /// Gets cards with category information as DTOs
    /// </summary>
    public IEnumerable<SingleCardDto> GetCardsWithCategoryInfo()
        => Cards.Select(card => 
        {
            var category = GetCategory(card.CategoryId);
            return SingleCardDto.FromCardAndCategory(card, category);
        });

    /// <summary>
    /// Gets cards from specific category with category information as DTOs
    /// </summary>
    public IEnumerable<SingleCardDto> GetCardsWithCategoryInfo(string categoryId)
        => GetCardsByCategory(categoryId).Select(card =>
        {
            var category = GetCategory(categoryId);
            return SingleCardDto.FromCardAndCategory(card, category);
        });

    /// <summary>
    /// Gets favorite cards
    /// </summary>
    public IEnumerable<SingleCard> GetFavoriteCards()
        => Cards.Where(card => card.Favourite);

    /// <summary>
    /// Gets cards by learning progress
    /// </summary>
    public IEnumerable<SingleCard> GetCardsByLearningProgress(LearningProgress progress)
        => Cards.Where(card => card.LearningProgress == progress);

    /// <summary>
    /// Adds a card to the collection
    /// </summary>
    public void AddCard(SingleCard card)
    {
        Cards.Add(card);
    }

    /// <summary>
    /// Removes a card from the collection
    /// </summary>
    public bool RemoveCard(string cardId)
    {
        var card = Cards.FirstOrDefault(c => c.Id == cardId);
        if (card != null)
        {
            Cards.Remove(card);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Updates card category
    /// </summary>
    public bool UpdateCardCategory(string cardId, string newCategoryId)
    {
        var card = Cards.FirstOrDefault(c => c.Id == cardId);
        if (card != null)
        {
            card.CategoryId = newCategoryId;
            return true;
        }
        return false;
    }
}
