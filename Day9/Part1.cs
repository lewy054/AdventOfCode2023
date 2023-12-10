namespace Day9;

public class Part1
{
    public Part1(List<string> input)
    {
        GetInput = input;
    }

    protected virtual List<string> GetInput { get; }

    public int GetResult()
    {
        var extrapolatedHistoriesSum = 0;
        foreach (var history in GetInput)
        {
            var histories = new List<List<int>>();
            var values = history.Split(" ").Select(int.Parse).ToList();
            histories.Add(values);
            PredictNextValue(ref histories);
            var extrapolatedHistories = ExtrapolateHistory(histories);
            extrapolatedHistoriesSum += extrapolatedHistories.First().Last();
        }


        return extrapolatedHistoriesSum;
    }

    private static List<List<int>> PredictNextValue(ref List<List<int>> histories)
    {
        var lineToExtrapolate = histories.Last();
        if (lineToExtrapolate.All(e => e == 0))
        {
            return histories;
        }

        var nextLine = new List<int>();
        for (var i = 1; i < lineToExtrapolate.Count; i++)
        {
            var value = lineToExtrapolate[i] - lineToExtrapolate[i - 1];
            nextLine.Add(value);
        }

        histories.Add(nextLine);
        return PredictNextValue(ref histories);
    }

    private static List<List<int>> ExtrapolateHistory(List<List<int>> history)
    {
        history.Last().Add(0);
        for (var i = history.Count - 2; i >= 0; i--)
        {
            var sameRowLast = history[i].Last();
            var previousRowLast = history[i + 1].Last();
            history[i].Add(sameRowLast + previousRowLast);
        }

        return history;
    }
}