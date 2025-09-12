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
    /// Updates CountCards for a specific category based on actual card count
    /// </summary>
    public void UpdateCategoryCardCount(string categoryId)
    {
        var category = GetCategory(categoryId);
        if (category != null)
        {
            category.CountCards = GetCardsByCategory(categoryId).Count();
            category.LastModified = DateTime.Now;
        }
    }

    /// <summary>
    /// Updates CountCards for all categories
    /// </summary>
    public void UpdateAllCategoryCardCounts()
    {
        foreach (var category in Categories)
        {
            if (!string.IsNullOrEmpty(category.Id))
            {
                UpdateCategoryCardCount(category.Id);
            }
        }
    }

    /// <summary>
    /// Adds a card and updates category metadata
    /// </summary>
    public void AddCard(SingleCard card)
    {
        Cards.Add(card);
        UpdateCategoryCardCount(card.CategoryId);
    }

    /// <summary>
    /// Removes a card and updates category metadata
    /// </summary>
    public bool RemoveCard(string cardId)
    {
        var card = Cards.FirstOrDefault(c => c.Id == cardId);
        if (card != null)
        {
            Cards.Remove(card);
            UpdateCategoryCardCount(card.CategoryId);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Updates card category and adjusts category counts
    /// </summary>
    public bool UpdateCardCategory(string cardId, string newCategoryId)
    {
        var card = Cards.FirstOrDefault(c => c.Id == cardId);
        if (card != null)
        {
            var oldCategoryId = card.CategoryId;
            card.CategoryId = newCategoryId;
            
            // Update both old and new category counts
            UpdateCategoryCardCount(oldCategoryId);
            UpdateCategoryCardCount(newCategoryId);
            return true;
        }
        return false;
    }
}
