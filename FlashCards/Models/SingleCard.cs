namespace FlashCards.Models;

public class SingleCard
{
    public string Phrase { get; set; }
    public string Translation { get; set; }
    public string Example { get; set; }
    public Categories Category { get; set; }
    public LearningProgress LearningProgress { get; set; }
    public bool Favourite { get; set; }
}