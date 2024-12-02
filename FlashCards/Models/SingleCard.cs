namespace FlashCards.Models;

public class SingleCard
{
    public string Id { get; set; }
    public string Phrase { get; set; }
    public string Translation { get; set; }
    public string Example { get; set; }
    //TODO - removing CategoryName will be better - next step
    public string CategoryName { get; set; }
    public string CategoryId { get; set; }
    public LearningProgress LearningProgress { get; set; }
    public bool Favourite { get; set; }
}