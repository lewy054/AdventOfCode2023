using Helpers;

namespace Day1;

public class Part1
{
    public Part1(string fileName)
    {
        FileName = fileName;
    }

    private string FileName { get; }

    public int GetResult()
    {
        var input = FileHelpers.GetFileContent(FileName);
        var results = input.Select(GetCombinedFirstAndLastDigit).ToList();
        return results.Sum();
    }

    private static int GetCombinedFirstAndLastDigit(string input)
    {
        var firstValue = input.First(char.IsNumber);
        var lastValue = input.Last(char.IsNumber);
        var result = string.Concat(firstValue, lastValue);
        return int.Parse(result);
    }
}