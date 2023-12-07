// See https://aka.ms/new-console-template for more information

using Day5;
using Helpers;

const string filename = "input.txt";
var input = FileHelpers.GetFileContent(filename).ToList();
var almanac = new Almanac(input);

var locations = almanac.GetLocations();
var minLocationPart1 = locations.Min();
Console.WriteLine($"Results of Day5, Part1 : {minLocationPart1}");

var seeds = almanac.Seeds;
var minLocation = long.MaxValue;
Parallel.ForEach(SteppedIterator(0, almanac.Seeds.Count, 2), (i) =>
{
    var maxRange = almanac.Seeds[i] + almanac.Seeds[i + 1] - 1;
    Console.WriteLine($"{i}: Range start: {almanac.Seeds[i]}, Range end: {maxRange}");
    for (var seed = almanac.Seeds[i]; seed <= maxRange; seed++)
    {
        var location = almanac.GetSeedLocation(seed);
        if (location < minLocation)
        {
            minLocation = location;
            Console.WriteLine($"Found new min: {minLocation}");
        }
    }

    Console.WriteLine($"end: {i}");
});


static IEnumerable<int> SteppedIterator(int startIndex, int endIndex, int stepSize)
{
    for (int i = startIndex; i < endIndex; i = i + stepSize)
    {
        yield return i;
    }
}


Console.WriteLine($"Results of Day5, Part2 : {minLocation}");