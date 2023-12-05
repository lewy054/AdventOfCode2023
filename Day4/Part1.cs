using System.Text.RegularExpressions;
using Helpers;

namespace Day4;

public class Part1 : IResolver
{
    public Part1(IEnumerable<string> input)
    {
        Input = input;
    }

    private IEnumerable<string> Input { get; set; }

    public virtual int GetResult()
    {
        var cards = GetCards();
        var points = 0;
        foreach (var card in cards)
        {
            var howManyWins = GetHowManyWiningNumbers(card);

            if (howManyWins == 0)
            {
                continue;
            }

            var pow = Convert.ToInt32(Math.Pow(2, howManyWins - 1));

            points += howManyWins == 1 ? 1 : pow;
        }

        return points;
    }

    protected IEnumerable<Card> GetCards()
    {
        var cards = new List<Card>();

        foreach (var line in Input)
        {
            var lineWithSameDelimiters = line.Replace(":", " | ");
            var lineSplit = lineWithSameDelimiters.Split(" | ");
            var cardId = int.Parse(Regex.Match(lineSplit[0], @"\d+").Value);
            var winningNumbers = Regex.Matches(lineSplit[1], @"\d+").Select(e => int.Parse(e.Value)).ToList();
            var numbers = Regex.Matches(lineSplit[2], @"\d+").Select(e => int.Parse(e.Value)).ToList();
            cards.Add(new Card(cardId, winningNumbers, numbers));
        }

        return cards;
    }

    protected static int GetHowManyWiningNumbers(Card card)
    {
        var allNumbersWithoutDuplicates = card.Numbers.Concat(card.WinningNumbers).Distinct().ToList();
        var totalCount = card.Numbers.Count + card.WinningNumbers.Count;
        var howManyWins = totalCount - allNumbersWithoutDuplicates.Count;
        return howManyWins;
    }
}