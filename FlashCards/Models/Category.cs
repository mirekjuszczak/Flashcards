using Plugin.Firebase.Firestore;

namespace FlashCards.Models;

public class Category
{
    [FirestoreDocumentId]
    public string? Id { get; set; }
    
    [FirestoreProperty("name")]
    public string Name { get; set; }
    
    [FirestoreProperty("countcards")]
    public int CountCards { get; set; }
    
    [FirestoreProperty("lastmodified")]
    public DateOnly LastModified { get; set; }
}