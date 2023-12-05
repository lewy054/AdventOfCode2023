using Day4;
using Helpers;

const string fileName = "input.txt";
var input = FileHelpers.GetFileContent(fileName).ToList();

var part1 = new Part1(input);
var part1Result = part1.GetResult();
Console.WriteLine($"Results of Day4, Part1 : {part1Result}");

var part2 = new Part2(input);
var part2Result = part2.GetResult();
Console.WriteLine($"Results of Day4, Part2 : {part2Result}");