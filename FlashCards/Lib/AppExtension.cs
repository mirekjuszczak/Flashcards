namespace FlashCards.Lib;

public static class AppExtension
{
    public static TOut GetResource<TOut>(this Application? application, string key)
    {
        object? resource = null;

        application?.Resources.TryGetValue(key, out resource);

        if (resource is not TOut outValue)
        {
            throw new KeyNotFoundException($"Resource key not found: {key} or is not of type {typeof(TOut).Name}");
        }

        return outValue;
    }
}