namespace Day3;

public class Part2 : Part1
{
    public Part2(IList<string> input) : base(input)
    {
    }

    private static char GearSymbol => '*';

    public override int GetResult()
    {
        var symbolPositions = GetSpecificSymbolPosition().ToList();
        var sum = 0;
        foreach (var symbolPosition in symbolPositions)
        {
            var numbers = new ValidatedList();
            var topNumbers = GetNumbersFromTopAndBottom(symbolPosition.Y, symbolPosition.X, SideToSearch.Up);
            numbers.AddRange(topNumbers.Numbers);

            var leftNumber = GetNumberFromSide(symbolPosition.Y, symbolPosition.X, SideToSearch.Left);
            numbers.Add(leftNumber);
            var rightNumber = GetNumberFromSide(symbolPosition.Y, symbolPosition.X, SideToSearch.Right);
            numbers.Add(rightNumber);

            var bottomNumbers = GetNumbersFromTopAndBottom(symbolPosition.Y, symbolPosition.X, SideToSearch.Bottom);
            numbers.AddRange(bottomNumbers.Numbers);

            if (numbers.Numbers.Count == 2)
            {
                sum += numbers.Numbers.Aggregate((a, x) => a * x);
            }
        }

        return sum;
    }

    private IEnumerable<Position> GetSpecificSymbolPosition()
    {
        var result = new List<Position>();
        for (var y = 0; y < Input.Count; y++)
        {
            for (var x = 0; x < Input.ElementAt(y).Length; x++)
            {
                var symbol = Input.ElementAt(y)[x];
                if (symbol == GearSymbol)
                {
                    result.Add(new Position(x, y));
                }
            }
        }

        return result;
    }
}