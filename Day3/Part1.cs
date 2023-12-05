using System.Text;
using Helpers;

namespace Day3;

public class Part1 : IResolver
{
    public Part1(IList<string> input)
    {
        Input = input;
    }


    public IList<string> Input { get; init; }

    public int GetResult()
    {
        var symbolPositions = GetSymbolsPosition().ToList();
        var numbers = new AdjacentNumberList();
        foreach (var symbolPosition in symbolPositions)
        {
            //top
            var topLeftNumber = GetNumberFromSide(symbolPosition.Y - 1, symbolPosition.X, SideToSearch.Left);
            var topRightNumber = GetNumberFromSide(symbolPosition.Y - 1, symbolPosition.X, SideToSearch.Right);

            var topLine = Input.ElementAt(symbolPosition.Y - 1);

            var topMiddleChar = topLine.ElementAt(symbolPosition.X);

            if (char.IsDigit(topMiddleChar))
            {
                var topNumber = "";
                if (topLeftNumber != -1)
                {
                    topNumber += topLeftNumber;
                }

                topNumber += topMiddleChar;
                RemoveDigitFromInput(symbolPosition.X, symbolPosition.Y - 1);
                if (topRightNumber != -1)
                {
                    topNumber += topRightNumber;
                }

                numbers.Add(int.Parse(topNumber));
            }
            else
            {
                numbers.Add(topLeftNumber);
                numbers.Add(topRightNumber);
            }

            //middle
            var leftNumber = GetNumberFromSide(symbolPosition.Y, symbolPosition.X, SideToSearch.Left);
            numbers.Add(leftNumber);
            var rightNumber = GetNumberFromSide(symbolPosition.Y, symbolPosition.X, SideToSearch.Right);
            numbers.Add(rightNumber);


            //bottom
            var bottomLeftNumber = GetNumberFromSide(symbolPosition.Y + 1, symbolPosition.X, SideToSearch.Left);
            var bottomRightNumber = GetNumberFromSide(symbolPosition.Y + 1, symbolPosition.X, SideToSearch.Right);

            var bottomLine = Input.ElementAt(symbolPosition.Y + 1);
            var bottomMiddleChar = bottomLine.ElementAt(symbolPosition.X);

            if (char.IsDigit(bottomMiddleChar))
            {
                var bottomNumber = "";
                if (bottomLeftNumber != -1)
                {
                    bottomNumber += bottomLeftNumber;
                }

                bottomNumber += bottomMiddleChar;
                RemoveDigitFromInput(symbolPosition.X, symbolPosition.Y + 1);
                if (bottomRightNumber != -1)
                {
                    bottomNumber += bottomRightNumber;
                }

                numbers.Add(int.Parse(bottomNumber));
            }
            else
            {
                numbers.Add(bottomLeftNumber);
                numbers.Add(bottomRightNumber);
            }
        }

        foreach (var num in numbers.Numbers)
        {
            if (num <= 0)
            {
                Console.Write("przegr");
            }
        }

        return numbers.Numbers.Sum();
    }

    private int GetNumberFromSide(int y, int x, SideToSearch sideToSearch)
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

        var isNumber = int.TryParse(number, out var num);
        return isNumber ? num : -1;
    }

    private void RemoveDigitFromInput(int x, int y)
    {
        var aStringBuilder = new StringBuilder(Input.ElementAt(y));
        aStringBuilder.Remove(x, 1);
        aStringBuilder.Insert(x, ".");
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

    private enum SideToSearch
    {
        Left,
        Right
    }
}