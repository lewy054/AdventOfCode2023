using System.Text.RegularExpressions;
using Helpers;

namespace Day4;

public class Part1
{
    public Part1(string fileName)
    {
        FileName = fileName;
    }

    public string FileName { get; set; }
    private IEnumerable<string> Input { get; set; }

    public int GetResult()
    {
        Input = FileHelpers.GetFileContent(FileName).ToList();
        var cards = GetCards();
        var points = 0;
        foreach (var card in cards)
        {
            var allNumbersWithoutDuplicates = card.Numbers.Concat(card.WinningNumbers).Distinct().ToList();
            var totalCount = card.Numbers.Count + card.WinningNumbers.Count;
            var howManyWins = totalCount - allNumbersWithoutDuplicates.Count;
            if (howManyWins == 0)
            {
                continue;
            }

            var pow = Convert.ToInt32(Math.Pow(2, howManyWins - 1));

            points += howManyWins == 1 ? 1 : pow;
        }

        return points;
    }

    private IEnumerable<Card> GetCards()
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
}