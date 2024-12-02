namespace FlashCards.Models;

public class Category
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public static class CategoriesExtention
{
    public static string? GetCategoryName(this List<Category> categories, string id)
    {
        var categoryName = categories.FirstOrDefault(x => x.Id == id)?.Name;
        
        return categoryName;
    }
}