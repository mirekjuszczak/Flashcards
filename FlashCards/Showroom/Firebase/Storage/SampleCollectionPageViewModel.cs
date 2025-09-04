using FlashCards.ViewModels;
using FlashCards.Models;
using System.Collections.ObjectModel;
using Plugin.Firebase.Firestore;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FlashCards.Showroom.Firebase.Storage;

public partial class SampleCollectionPageViewModel : BaseViewModel
{
    private readonly IFirebaseFirestore _firebaseFirestore;
    
    [ObservableProperty] private ObservableCollection<SingleCard> _cards = new();
    
    [ObservableProperty] private bool _isLoading;
    
    [ObservableProperty] private string _infoText = "Click and get cards from Firestore";
    
    [ObservableProperty] private string _errorMessage = string.Empty;

    public SampleCollectionPageViewModel(IFirebaseFirestore firebaseFirestore)
    {
        _firebaseFirestore = firebaseFirestore;
        Title = "Cards collection of Firebase";

        LoadCardsCommand = new Command(async () => await LoadCardsAsync());
        DeleteCardsCollectionCommand = new Command(async () => await DeleteCardsCollectionAsync());
        AddSampleCardCommand = new Command(async () => await AddSampleCardAsync());
    }

    public Command LoadCardsCommand { get; }
    public Command DeleteCardsCollectionCommand { get; }
    public Command AddSampleCardCommand { get; }
    
    private async Task LoadCardsAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            InfoText = "Getting cards from Firestore...";
            Cards.Clear();

            // TEST
            // var mockcollection = new DatabaseServiceMock();
            // foreach (var card in mockcollection.CardsCollection)
            // {
            //     Cards.Add(card);
            // }
            // InfoText = $"{Cards.Count} cards from mocked database";
            // TEST

            // Get "cards" collection from Firestore
            var collectionReference = _firebaseFirestore.GetCollection("cards");
            var querySnapshot = await collectionReference.GetDocumentsAsync<SingleCard>();
            
            if (querySnapshot.Documents?.Any() == true)
            {
                foreach (var document in querySnapshot.Documents)
                {
                    // Convert a document form Firebase to SingleCard
                    var card = document.Data;
                    if (card != null)
                    {
                        Cards.Add(card);
                    }
                }
            
                InfoText = $"{Cards.Count} cards got from Firestore";
            }
            else
            {
                InfoText = "No card in collection. Add a few cards to the collection at the beginning.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Getting cards failed: {ex.Message}";
            InfoText = "Error during getting cards from Firestore";
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    private async Task DeleteCardsCollectionAsync()
    {
        try
        {
            IsLoading = true;
            
            // Delete "cards" collection from Firestore
            var collectionReference = _firebaseFirestore.GetCollection("cards");
            var querySnapshot = await collectionReference.GetDocumentsAsync<SingleCard>();
            
            var deletingCounter = 0;
            var collectionCount = querySnapshot.Count;
            
            if (querySnapshot.Documents?.Any() == true)
            {
                foreach (var document in querySnapshot.Documents)
                {
                    await document.Reference.DeleteDocumentAsync();
                    deletingCounter++;
                }
            
                InfoText = $"{deletingCounter} of {collectionCount} cards deleted from Firestore";
            }
            else
            {
                InfoText = "No card in collection. Add a few cards to the collection at the beginning.";
            }
            
            InfoText = "All cards deleted from Collection";
            
            await LoadCardsAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task AddSampleCardAsync()
    {
        try
        {
            IsLoading = true;
            InfoText = "Adding a card as an example...";
            
            var ticksString = DateTime.Now.Ticks.ToString();
            var phraseSufix = ticksString.Substring(ticksString.Length - 3);

            var sampleCard = new SingleCard
            {
                Phrase = $"Hello {phraseSufix}",
                Translation = $"Czesc {phraseSufix}",
                Example = "Hello, how are you?",
                CategoryName = "Basic",
                CategoryId = "basic",
                Favourite = false,
                LearningProgress = LearningProgress.NotStarted
            };
            
            var collectionReference = _firebaseFirestore.GetCollection("cards");
            await collectionReference.AddDocumentAsync(sampleCard);

            InfoText = "Example card has been added";
            
            await LoadCardsAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error while adding a card: {ex.Message}";
            InfoText = "Adding card failed...";
        }
        finally
        {
            IsLoading = false;
        }
    }
}