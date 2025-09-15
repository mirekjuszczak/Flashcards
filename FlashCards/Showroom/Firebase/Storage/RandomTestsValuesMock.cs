namespace FlashCards.Showroom.Firebase.Storage;

public static class RandomTestsValuesMock
{
    private static readonly Random _random = new();

    public static int GetRandomNumber(int min, int max)
    {
        return _random.Next(min, max + 1);
    }
    
    public static string GetRandomCategoryName()
    {
        var number = GetRandomNumber(1, 4);

        return number switch
        {
            1 => "beginner",
            2 => "pre-intermediate",
            3 => "intermediate",
            4 => "advanced"
        };
    }
    
    public static string GetRandomIdCategory()
    {
        var letterRandom = GetRandomNumber(1, 3);
        var letter = letterRandom switch
        {
            1 => "A",
            2 => "B",
            3 => "C"
        };
        
        var numberRandom = GetRandomNumber(1, 2);
        
        return letter+numberRandom;
    }
}