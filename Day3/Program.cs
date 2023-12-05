using Day3;
using Helpers;

var fileName = "input.txt";
var input = FileHelpers.GetFileContent(fileName).ToList();

var part1 = new Part1(input);
var part1Result = part1.GetResult();
Console.WriteLine($"Results of Day3, Part1 : {part1Result}");

// var part2 = new Part2("input.txt", elfBag);
// var part2Result = part2.GetResult();
// Console.WriteLine($"Results of Day2, Part2 : {part2Result}");