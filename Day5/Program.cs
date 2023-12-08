// See https://aka.ms/new-console-template for more information

using Day5;
using Helpers;

const string filename = "input.txt";
var input = FileHelpers.GetFileContent(filename).ToList();
var almanac = new Almanac(input);

var location = almanac.Seeds.Select(almanac.FindLocationForSeed).Min();
Console.WriteLine($"Results of Day5, Part1 : {location}");

var maxRange = almanac.GetSeedMaxRange();
for (var minLocation = 0; minLocation < maxRange; minLocation++)
{
    var seed = almanac.FindSeedForLocation(minLocation);
    if (almanac.SeedExists(seed))
    {
        Console.WriteLine($"Results of Day5, Part2 : {minLocation}");
        break;
    }
}
