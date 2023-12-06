// See https://aka.ms/new-console-template for more information

using Day5;
using Helpers;

const string filename = "input.txt";
var input = FileHelpers.GetFileContent(filename).ToList();
var part1 = new Part1(input);

var part1Result = part1.GetResult();
Console.WriteLine($"Results of Day5, Part1 : {part1Result}");

// var part2 = new Part2(input);
// var part2Result = part2.GetResult();
// Console.WriteLine($"Results of Day5, Part2 : {part2Result}");