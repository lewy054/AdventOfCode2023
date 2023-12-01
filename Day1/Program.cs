// See https://aka.ms/new-console-template for more information

using Day1;

const string fileName = "input.txt";

var part1 = new Part1(fileName);
var part1Result = part1.GetResult();
Console.WriteLine($"Results of Day1, Part1 : {part1Result}");

var part2 = new Part2(fileName);
var part2Result = part2.GetResult();
Console.WriteLine($"Results of Day1, Part2 : {part2Result}");