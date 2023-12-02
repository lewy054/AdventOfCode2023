using Helpers;

namespace Day2;

public class Part2 : Part1
{
    private string Filename { get; }
    public Part2(string filename, Record bagLoad) : base(filename, bagLoad)
    {
        Filename = filename;
    }

    public override int GetResult()
    {
        var input = FileHelpers.GetFileContent(Filename);
        var records = GetRecords(input);
        var result = 0;
        foreach (var record in records)
        {
            var gameMinimalHand = GetMinimalHandForOneGame(record.Value);
            result += gameMinimalHand.BlueQuantity * gameMinimalHand.RedQuantity * gameMinimalHand.GreenQuantity;
        }
        return result;
    }

    private static Record GetMinimalHandForOneGame(List<Record> game)
    {
        var result = new Record();
        foreach (var record in game)
        {
            if (record.BlueQuantity > result.BlueQuantity)
            {
                result.BlueQuantity = record.BlueQuantity;
            }
            if (record.RedQuantity > result.RedQuantity)
            {
                result.RedQuantity = record.RedQuantity;
            }
            if (record.GreenQuantity > result.GreenQuantity)
            {
                result.GreenQuantity = record.GreenQuantity;
            }
        }
        return result;
    }
}