namespace Heroes.Fun.AuraColorizer;

public static class Utility
{
    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (T item in enumeration)
        {
            action(item);
        }
    }

    public static TSource[] GetEnumValues<TSource>()
    {
        return (TSource[])Enum.GetValues(typeof(TSource));
    }
}