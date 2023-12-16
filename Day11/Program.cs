using Day11;
using Helpers;

const string fileName = "input.txt";
var input = FileHelpers.GetFileContent(fileName).Select(e=> e.ToArray()).ToArray();
var part1 = new CosmicExpansion(input, 1);
var part1Result = part1.GetResult();
Console.WriteLine($"Day 11, part1 : {part1Result}");

var part2 = new CosmicExpansion(input, 1_000_000 - 1);
var part2Result = part2.GetResult();
Console.WriteLine($"Day 11, part2 : {part2Result}");