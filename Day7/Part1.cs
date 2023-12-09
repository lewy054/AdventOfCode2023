namespace Day7;

public class Part1
{
    public Part1(List<(string hand, int bid)> input)
    {
        Input = input;
    }

    private List<(string hand, int bid)> Input { get; set; }
    protected virtual string GetCardsRanking => "AKQJT98765432";

    public int GetTotalWinnings()
    {
        var sortedHand = GetSortedHand();
        return sortedHand.Select((t, i) => t.Item2 * (i + 1)).Sum();
    }


    private IEnumerable<Tuple<string, int>> GetSortedHand()
    {
        var hands = new List<(string hand, int bid, int strong)>();
        foreach (var (hand, bid) in Input)
        {
            var typeStrong = GetHandStrength(hand);
            hands.Add((hand, bid, typeStrong));
        }

        var sorted = hands
            .OrderBy(e => (e.hand, e.strong, GetCardsRanking), new Extensions.CustomSorter()).ToList();
        var result = sorted.Select(e => new Tuple<string, int>(e.hand, e.bid)).ToList();
        return result;
    }

    private int GetHandStrength(string hand)
    {
        var handSymbols = GetHandSymbols(hand);
        int typeStrong;
        if (handSymbols.Any(e => e.Length == 5))
        {
            typeStrong = (int)TypeStrong.FiveOfKind;
        }
        else if (handSymbols.Any(e => e.Length == 4))
        {
            typeStrong = (int)TypeStrong.FourOfKind;
        }
        else if (handSymbols.Count == 2 && handSymbols.ElementAt(0).Length + handSymbols.ElementAt(1).Length == 5)
        {
            typeStrong = (int)TypeStrong.FullHouse;
        }
        else if (handSymbols.Any(e => e.Length == 3))
        {
            typeStrong = (int)TypeStrong.ThreeOfKind;
        }
        else if (handSymbols.Count == 2)
        {
            typeStrong = (int)TypeStrong.TwoPair;
        }
        else if (handSymbols.Count == 1)
        {
            typeStrong = (int)TypeStrong.OnePair;
        }
        else
        {
            typeStrong = (int)TypeStrong.HighCard;
        }

        return typeStrong;
    }

    protected virtual List<string> GetHandSymbols(string hand)
    {
        var nonDistinctItems =
            from list in hand.ToArray()
            group list by list
            into grouped
            where grouped.Count() > 1
            select string.Concat(grouped).ToList();
        var nonDistinctItemsList = nonDistinctItems.Select(e => string.Concat(e)).ToList();
        return nonDistinctItemsList;
    }
    

    private enum TypeStrong
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfKind,
        FullHouse,
        FourOfKind,
        FiveOfKind,
    }
}