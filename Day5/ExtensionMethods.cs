namespace Day5;

public static class ExtensionMethods
{
    public static long GetDestinationValue(this List<Map> destination, long seed)
    {
        var map = destination.FirstOrDefault(e =>
            e.SourceRangeStart <= seed && e.SourceRangeStart + e.RangeLength >= seed);
        var number = map != null ? seed +  map.DestinationRangeStart - map.SourceRangeStart : seed;
        return number;
    }
    
    public static long GetSourceValue(this List<Map> destination, long seed)
    {
        var map = destination.FirstOrDefault(e =>
            e.DestinationRangeStart <= seed && e.DestinationRangeStart + e.RangeLength >= seed);
        var number = map != null ? seed  -  map.DestinationRangeStart + map.SourceRangeStart: seed;
        return number;
    }
}