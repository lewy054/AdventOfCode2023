namespace Day10;

public static class Pipes
{
    public const char StartPipe= 'S';
    public const char VerticalPipe = '|';
    public const char HorizontalPipe = '-';
    public const char NorthEastBend = 'L';
    public const char NorthWestBend = 'J';
    public const char SouthWestBend = '7';
    public const char SouthEastBend= 'F';

    public static Position GetStartPoint(IReadOnlyList<char[]> input)
    {
        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == StartPipe)
                {
                    return new Position(x, y);
                }
            }
        }

        throw new InvalidDataException();
    }

    public static bool CanGoToRight(char pipe)
    {
        return pipe is HorizontalPipe or NorthWestBend or SouthWestBend;
    }
    
    public static bool CanGoToLeft(char pipe)
    {
        return pipe is HorizontalPipe or NorthEastBend or SouthEastBend;
    }
    
    public static bool CanGoUp(char pipe)
    {
        return pipe is VerticalPipe or SouthWestBend or SouthEastBend;
    }
    
    public static bool CanGoBottom(char pipe)
    {
        return pipe is VerticalPipe or NorthEastBend or NorthWestBend;
    }
}