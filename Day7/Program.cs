using Day7;
using Helpers;

const string inputFileName = "input.txt";
var input = FileHelpers.GetFileContent(inputFileName).ToList();
List<(string hand, int bid)> pairs = input
    .Select(x =>
    {
        var s = x.Split(' ');
        return (s[0], int.Parse(s[1]));
    })
    .ToList();

var part1 = new Part1(pairs);
var part1Result = part1.GetTotalWinnings();

Console.WriteLine($"Result of Day7, Part1: {part1Result}");

