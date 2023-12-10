using Day8;
using Helpers;

var inputFileName = "input.txt";
var input = FileHelpers.GetFileContent(inputFileName).ToList();
var instructions = input.Skip(2).ToList();
var nodes = Day8.Helpers.MapNodes(instructions);
var map = new Map(input.ElementAt(0), nodes);
var part1Result = map.GetResult();
Console.Write($"Day8, Part1 result: {part1Result}");



