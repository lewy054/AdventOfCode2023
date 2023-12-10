using Day9;
using Helpers;

const string fileName = "input.txt";
var input = FileHelpers.GetFileContent(fileName).ToList();
var part1 = new Part1(input);
var part1Result = part1.GetResult();

Console.WriteLine($"Day 9, Part1 result: {part1Result}");
