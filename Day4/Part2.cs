namespace Day4;

public class Part2 : Part1
{
    public Part2(IList<string> input) : base(input)
    {
        Input = input;
    }
    
    public override int GetResult()
    {
        var cards = GetCards().ToList();
        var resultScratchCards = new List<Card>();

        var winningDictionary = cards.ToDictionary(card => card.Id, _ => 1);

        for (var i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < winningDictionary[i + 1]; j++)
            {
                var howManyWinsInCurrentCard = GetHowManyWiningNumbers(cards[i]);
                resultScratchCards.Add(cards[i]);
                for (var x = 1; x <= howManyWinsInCurrentCard; x++)
                {
                    winningDictionary[x + i + 1] += 1;
                }
            }
        }

        return resultScratchCards.Count;
    }
}