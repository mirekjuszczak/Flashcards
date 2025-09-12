using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlashCards.Models;
using FlashCards.Models.Dto;
using FlashCards.Services.DatabaseService;
using FlashCards.Showroom.Firebase.Storage;

namespace FlashCards.Services.DatabaseServiceMock;

public class DatabaseServiceMock
{
    public List<SingleCardDto> CardsCollection { get; }
    public List<Category> CategoriesCollection { get; }

    public DatabaseServiceMock()
    {
        CardsCollection = CreateMockedCardsList();
        CategoriesCollection = CreateMockedCategoriesList();
    }

    public Task<List<Category>> GetCategoriesCollection()
    {
        var listCategories = CreateMockedCategoriesList();

        return Task.FromResult(listCategories);
    }

    private List<Category> CreateMockedCategoriesList()
    {
        var categories = new List<Category>();
        
        categories.Add(new Category
        {
            Id = 1.ToString(),  //Firebase automationally generates string IDs
            Name = "Noun",
            CountCards = 10,
            LastModified = DateTime.Now
        });
        
        categories.Add(new Category
        {
            Id = 2.ToString(),  //Firebase automationally generates string IDs
            Name = "Verb",
            CountCards = 3,
            LastModified = DateTime.Now
        });
        
        categories.Add(new Category
        {
            Id = 3.ToString(),  //Firebase automationally generates string IDs
            Name = "Airport",
            CountCards = 5,
            LastModified = DateTime.Now
        });

        categories.Add(new Category
        {
            Id = 4.ToString(),  //Firebase automationally generates string IDs
            Name = "Adjective",
            CountCards = 13,
            LastModified = DateTime.Now
        });
        
        categories.Add(new Category
        {
            Id = 5.ToString(),  //Firebase automationally generates string IDs
            Name = "Restaurant",
            CountCards = 15,
            LastModified = DateTime.Now
        });
        
        categories.Add(new Category
        {
            Id = 6.ToString(),  //Firebase automationally generates string IDs
            Name = "Hotel",
            CountCards = 23,
            LastModified = DateTime.Now
        });
        
        categories.Add(new Category
        {
            Id = 7.ToString(),  //Firebase automationally generates string IDs
            Name = "Prepositions",
            CountCards = 2,
            LastModified = DateTime.Now
        });
        
        categories.Add(new Category
        {
            Id = 8.ToString(),  //Firebase automationally generates string IDs
            Name = "Tenses",
            CountCards = 0,
            LastModified = DateTime.Now
        });
        
        categories.Add(new Category
        {
            Id = 9.ToString(),  //Firebase automationally generates string IDs
            Name = "Phrasal Verbs",
            CountCards = 0,
            LastModified = DateTime.Now
        });
        
        categories.Add(new Category
        {
            Id = 10.ToString(),  //Firebase automationally generates string IDs
            Name = "Idioms",
            CountCards = 9,
            LastModified = DateTime.Now
        });
        
        return categories;
    }

    private List<SingleCardDto> CreateMockedCardsList()
    {
        var categories = CreateMockedCategoriesList();
        
        var cards = new List<SingleCardDto>();

        cards.Add(new SingleCardDto
        {
            Id = "C1",
            Phrase = "Cat",
            Translation = "Kot",
            Example = "This is my cat",
            CategoryName = RandomTestsValuesMock.GetRandomCategoryName(),
            CategoryId = RandomTestsValuesMock.GetRandomIdCategory(),
            LearningProgress = LearningProgress.InProgress,
            Favourite = true
        });
        
        cards.Add(new SingleCardDto
        {
            Id = "C2",
            Phrase = "Dog",
            Translation = "Pies",
            Example = "This is my dog",
            CategoryName = RandomTestsValuesMock.GetRandomCategoryName(),
            CategoryId = RandomTestsValuesMock.GetRandomIdCategory(),
            LearningProgress = LearningProgress.Learned,
            Favourite = true
        });
        
        cards.Add(new SingleCardDto
        {
            Id = "C3",
            Phrase = "run",
            Translation = "biec/biegać",
            Example = "I was running yesterday",
            CategoryName = RandomTestsValuesMock.GetRandomCategoryName(),
            CategoryId = RandomTestsValuesMock.GetRandomIdCategory(),
            LearningProgress = LearningProgress.InProgress,
            Favourite = false
        });
        
        cards.Add(new SingleCardDto
        {
            Id = "C4",
            Phrase = "buy",
            Translation = "kupować",
            Example = "This is my cat",
            CategoryName = RandomTestsValuesMock.GetRandomCategoryName(),
            CategoryId = RandomTestsValuesMock.GetRandomIdCategory(),
            LearningProgress = LearningProgress.InProgress,
            Favourite = true
        });
        
        cards.Add(new SingleCardDto
        {
            Id = "C5",
            Phrase = "move",
            Translation = "przemiaszczać się/przesuwać",
            Example = "We moved our flat last year",
            CategoryName = RandomTestsValuesMock.GetRandomCategoryName(),
            CategoryId = RandomTestsValuesMock.GetRandomIdCategory(),
            LearningProgress = LearningProgress.Learned,
            Favourite = true
        });
        
        cards.Add(new SingleCardDto
        {
            Id = "C61",
            Phrase = "Prepare",
            Translation = "Przygotowywać",
            Example = "I try to prepare this application",
            CategoryName = RandomTestsValuesMock.GetRandomCategoryName(),
            CategoryId = RandomTestsValuesMock.GetRandomIdCategory(),
            LearningProgress = LearningProgress.NotStarted,
            Favourite = true
        });
        
        return cards;
    }
}