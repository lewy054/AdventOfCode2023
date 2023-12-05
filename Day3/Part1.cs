using System.Globalization;
using System.Text;
using Helpers;

namespace Day3;

public partial class Part1 : IResolver
{
    public Part1(IList<string> input)
    {
        Input = input;
    }

    public IList<string> Input { get; init; }

    public int GetResult()
    {
        var symbolPositions = GetSymbolsPosition().ToList();
        var numbers = new ValidatedList();
        foreach (var symbolPosition in symbolPositions)
        {
            var topNumbers = GetNumbersFromTopAndBottom(symbolPosition.Y, symbolPosition.X, SideToSearch.Up);
            numbers.AddRange(topNumbers.Numbers);

            var leftNumber = GetNumberFromSide(symbolPosition.Y, symbolPosition.X, SideToSearch.Left);
            numbers.Add(leftNumber);
            var rightNumber = GetNumberFromSide(symbolPosition.Y, symbolPosition.X, SideToSearch.Right);
            numbers.Add(rightNumber);

            var bottomNumbers = GetNumbersFromTopAndBottom(symbolPosition.Y, symbolPosition.X, SideToSearch.Bottom);
            numbers.AddRange(bottomNumbers.Numbers);
        }

        return numbers.Numbers.Sum();
    }

    private string GetNumberFromSide(int y, int x, SideToSearch sideToSearch)
    {
        var number = "";
        var nextX = x;
        while (true)
        {
            nextX = sideToSearch == SideToSearch.Left ? nextX - 1 : nextX + 1;
            if (nextX == -1 || nextX == Input.ElementAt(y).Length || !char.IsDigit(Input.ElementAt(y).ElementAt(nextX)))
            {
                break;
            }

            var symbol = Input.ElementAt(y).ElementAt(nextX);
            number = symbol + number;
            RemoveDigitFromInput(nextX, y);
        }

        if (sideToSearch == SideToSearch.Right)
        {
            number = string.Concat(number.Reverse());
        }

        return number;
    }

    private ValidatedList GetNumbersFromTopAndBottom(int y, int x, SideToSearch sideToSearch)
    {
        var nextY = y;
        switch (sideToSearch)
        {
            case SideToSearch.Bottom:
                nextY += 1;
                break;
            case SideToSearch.Up:
                nextY -= 1;
                break;
            case SideToSearch.Left:
            case SideToSearch.Right:
            default:
                throw new InvalidDataException();
        }

        var leftNumber = GetNumberFromSide(nextY, x, SideToSearch.Left);
        var rightNumber = GetNumberFromSide(nextY, x, SideToSearch.Right);
        var middleChar = Input.ElementAt(nextY).ElementAt(x);

        var foundNumbers = new ValidatedList();
        if (!char.IsDigit(middleChar))
        {
            foundNumbers.Add(leftNumber);
            foundNumbers.Add(rightNumber);
            return foundNumbers;
        }

        RemoveDigitFromInput(x, nextY);
        var number = leftNumber + middleChar + rightNumber;
        foundNumbers.Add(number);
        return foundNumbers;
    }

    private void RemoveDigitFromInput(int x, int y)
    {
        var aStringBuilder = new StringBuilder(Input.ElementAt(y));
        aStringBuilder.Remove(x, 1).Insert(x, ".");
        Input[y] = aStringBuilder.ToString();
    }

    private IEnumerable<Position> GetSymbolsPosition()
    {
        var result = new List<Position>();
        for (var y = 0; y < Input.Count; y++)
        {
            for (var x = 0; x < Input.ElementAt(y).Length; x++)
            {
                var symbol = Input.ElementAt(y)[x];
                if (symbol != '.' && !char.IsDigit(symbol))
                {
                    result.Add(new Position(x, y));
                }
            }
        }

        return result;
    }
}