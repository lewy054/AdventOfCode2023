using System.Text.RegularExpressions;
using Day6;
using Helpers;

const string inputFileName = "input.txt";
var input = FileHelpers.GetFileContent(inputFileName).ToList();

var times = Regex.Matches(input.ElementAt(0), @"\d+").Select(e => int.Parse(e.Value)).ToList();
var distances = Regex.Matches(input.ElementAt(1), @"\d+").Select(e => int.Parse(e.Value)).ToList();

var result = 1;
for (var i = 0; i < times.Count; i++)
{
    var race = new Race(times[i], distances[i]);
    result *= race.GetNumberOfPossibleWaysToWin();
}

Console.WriteLine($"Result of Day6, Part1: {result}");

var part2Time = long.Parse(string.Concat(times));
var part2Distance = long.Parse(string.Concat(distances));

var race2 = new Race(part2Time, part2Distance);
var part2Result = race2.GetNumberOfPossibleWaysToWin();


Console.WriteLine($"Result of Day6, Part1: {part2Result}");