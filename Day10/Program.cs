using Day10;
using Helpers;

const string fileName = "input.txt";
var input = FileHelpers.GetFileContent(fileName).Select(e => e.ToArray()).ToArray();
var part1 = new Part1(input);
var part1Result = part1.GetResult();

Console.WriteLine($"Day 10, Part1 result: {part1Result}");
