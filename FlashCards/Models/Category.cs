using Plugin.Firebase.Firestore;

namespace FlashCards.Models;

public class Category
{
    [FirestoreDocumentId]
    public string? Id { get; set; }
    
    [FirestoreProperty("name")]
    public string Name { get; set; }
}