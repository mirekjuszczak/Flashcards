using Plugin.Firebase.Firestore;

namespace FlashCards.Models;

public class SingleCard
{
    [FirestoreDocumentId]
    public string? Id { get; set; }
    
    [FirestoreProperty("phrase")]
    public string Phrase { get; set; } = string.Empty;
    
    [FirestoreProperty("translation")]
    public string Translation { get; set; } = string.Empty;
    
    [FirestoreProperty("example")]
    public string? Example { get; set; }
    
    [FirestoreProperty("categoryName")]
    public string CategoryName { get; set; } = string.Empty;
    
    [FirestoreProperty("categoryId")]
    public string CategoryId { get; set; } = string.Empty;
    
    [FirestoreProperty("learningProgress")]
    public LearningProgress LearningProgress { get; set; } = LearningProgress.NotStarted;
    
    [FirestoreProperty("favourite")]
    public bool Favourite { get; set; }
}