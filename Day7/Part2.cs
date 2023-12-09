namespace Day7;

public class Part2 : Part1
{
    public Part2(List<(string hand, int bid)> input) : base(input)
    {
    }

    private const char JokerSymbol = 'J';
    protected override string GetCardsRanking => "AKQT98765432J";

    protected override List<string> GetHandSymbols(string hand)
    {
        var jokersCount = hand.Count(e => e == JokerSymbol);

        var nonDistinctItems = hand.ToArray()
            .Where(list => list != 'J')
            .GroupBy(list => list)
            .Where(grouped => grouped.Count() > 1)
            .Select(grouped => grouped.ToList())
            .OrderByDescending(e=> e.Count)
            .ToList();
        if (jokersCount <= 0)
        {
            return nonDistinctItems.Select(e => string.Concat(e)).ToList();
        }

        var handWithoutJokers = hand.Replace("J", string.Empty);
        var elementToPopulate = GetElementToPopulate(handWithoutJokers);
        if (!elementToPopulate.HasValue)
        {
            return new List<string>{hand};
        }

        for (var i = 0; i < jokersCount; i++)
        {
            if (!nonDistinctItems.Any())
            {
                nonDistinctItems = new List<List<char>> { new() };
                nonDistinctItems.First().Add(elementToPopulate.Value);
            }
            nonDistinctItems.First().Add(elementToPopulate.Value);
        }

        return nonDistinctItems.Select(e => string.Concat(e)).ToList();
    }

    private static char? GetElementToPopulate(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return null;
        }

        var charMap = input.Distinct().ToDictionary(c => c, c => input.Count(s => s == c));
        return charMap.MaxBy(kvp => kvp.Value).Key;
    }
}