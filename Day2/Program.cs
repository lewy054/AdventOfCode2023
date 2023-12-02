using Day2;

var bag = new Record()
{
    RedQuantity = 12,
    GreenQuantity = 13,
    BlueQuantity = 14,
};
var part1 = new Part1("input.txt", bag);
var result = part1.GetResult();
Console.WriteLine($"Results of Day2, Part1 : {result}");