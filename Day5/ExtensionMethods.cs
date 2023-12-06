namespace Day5;

public static class ExtensionMethods
{
    public static long GetDestinationValue(this List<Map> source, long seed)
    {
        var map = source.FirstOrDefault(e =>
            e.SourceRangeStart <= seed && e.SourceRangeStart + e.RangeLength >= seed);
        var number = map != null ? seed +  map.DestinationRangeStart - map.SourceRangeStart : seed;
        return number;
    }
}