using System.Text.RegularExpressions;
using Helpers;

namespace Day1;

public class Part2
{
    public Part2(string fileName)
    {
        FileName = fileName;
    }

    private string FileName { get; }

    public int GetResult()
    {
        var input = FileHelpers.GetFileContent(FileName);
        var results = input.Select(GetCombinedFirstAndLastDigitText).ToList();
        return results.Sum();
    }

    private static int GetCombinedFirstAndLastDigitText(string input)
    {
        var dic = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
        };
        var regexString = $"(?=({string.Join("|", dic.Keys)}|\\d))";
        var matches = Regex.Matches(input, regexString);
        var firstValue = matches.First().Groups.Values.ToList().Last().Value;
        var secondValue = matches.Last().Groups.Values.ToList().Last().Value;
        var tryParseFirst = int.TryParse(firstValue, out var firstInt);
        var tryParseSecond = int.TryParse(secondValue, out var secondInt);
        var firstNumber = tryParseFirst ? firstInt : dic[firstValue];
        var secondNumber = tryParseSecond ? secondInt : dic[secondValue];
        return int.Parse(firstNumber.ToString() + secondNumber.ToString());
    }
}