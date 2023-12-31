﻿using Day2;

var elfBag = new Record()
{
    RedQuantity = 12,
    GreenQuantity = 13,
    BlueQuantity = 14,
};
var part1 = new Part1("input.txt", elfBag);
var part1Result = part1.GetResult();
Console.WriteLine($"Results of Day2, Part1 : {part1Result}");

var part2 = new Part2("input.txt", elfBag);
var part2Result = part2.GetResult();
Console.WriteLine($"Results of Day2, Part2 : {part2Result}");