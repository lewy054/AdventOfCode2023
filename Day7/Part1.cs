namespace Day7;

public class Part1
{
    public Part1(List<(string hand, int bid)> input)
    {
        Input = input;
    }

    private List<(string hand, int bid)> Input { get; set; }

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
            .OrderBy(e => (e.hand, e.strong), new Extensions.CustomSorter()).ToList();
        var result = sorted.Select(e => new Tuple<string, int>(e.hand, e.bid)).ToList();
        return result;
    }

    private static int GetHandStrength(string hand)
    {
        var nonDistinctItems =
            from list in hand.ToArray()
            group list by list
            into grouped
            where grouped.Count() > 1
            select string.Concat(grouped).ToList();
        var nonDistinctItemsList = nonDistinctItems.ToList();
        var typeStrong = nonDistinctItemsList.Count switch
        {
            1 when nonDistinctItemsList.ElementAt(0).Count == 5 => (int)TypeStrong.FiveOfKind,
            1 when nonDistinctItemsList.ElementAt(0).Count == 4 => (int)TypeStrong.FourOfKind,
            2 when nonDistinctItemsList.ElementAt(0).Count + nonDistinctItemsList.ElementAt(1).Count == 5 =>
                (int)TypeStrong.FullHouse,
            1 when nonDistinctItemsList.ElementAt(0).Count == 3 => (int)TypeStrong.ThreeOfKind,
            2 => (int)TypeStrong.TwoPair,
            1 => (int)TypeStrong.OnePair,
            _ => (int)TypeStrong.HighCard
        };

        return typeStrong;
    }

    enum TypeStrong
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