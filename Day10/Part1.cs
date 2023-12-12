namespace Day10;

public class Part1
{
    public Part1(char[][] input)
    {
        Input = input;
    }

    private char[][] Input { get; set; }


    public int GetResult()
    {
        var startPoint = Pipes.GetStartPoint(Input);
        var furthestPosition = new List<int>();
        var currentPositions = new List<(Position cameFrom, Position position, char PipeType, int steps)>();
        var leftPosition = new Position(startPoint.X - 1, startPoint.Y);
        var rightPosition = new Position(startPoint.X + 1, startPoint.Y);
        var upperPosition = new Position(startPoint.X, startPoint.Y - 1);
        var bottomPosition = new Position(startPoint.X, startPoint.Y + 1);
        var leftChar = GetPositionChar(leftPosition);
        var rightChar = GetPositionChar(rightPosition);
        var upperChar = GetPositionChar(upperPosition);
        var bottomChar = GetPositionChar(bottomPosition);
        if (leftChar.HasValue && Pipes.CanGoToLeft(leftChar.Value))
        {
            currentPositions.Add((startPoint, leftPosition, leftChar.Value, 1));
        }

        if (rightChar.HasValue && Pipes.CanGoToRight(rightChar.Value))
        {
            currentPositions.Add((startPoint, rightPosition, rightChar.Value, 1));
        }

        if (upperChar.HasValue && Pipes.CanGoUp(upperChar.Value))
        {
            currentPositions.Add((startPoint, upperPosition, upperChar.Value, 1));
        }

        if (bottomChar.HasValue && Pipes.CanGoBottom(bottomChar.Value))
        {
            currentPositions.Add((startPoint, bottomPosition, bottomChar.Value, 1));
        }

        while (true)
        {
            var nextPositions = new List<(Position cameFrom, Position position, char PipeType, int steps)>();
            foreach (var currentPosition in currentPositions)
            {
                //check where am i
                var positionToGo = GetPositionToGo(currentPosition);
                if (positionToGo == null)
                {
                    furthestPosition.Add(currentPosition.steps);
                    continue;
                }

                var nextPositionChar = GetPositionChar(positionToGo);
                if (nextPositionChar is '.' or 'S' or null)
                {
                    continue;
                }
                var existing = nextPositions
                    .Where(e => e.position.X == positionToGo.X && e.position.Y == positionToGo.Y).ToList();
                if (existing.Any())
                {
                    foreach (var valueTuple in existing)
                    {
                        furthestPosition.Add(valueTuple.steps);
                        nextPositions.Remove(valueTuple);
                    }
                }
                else
                {
                    nextPositions.Add((currentPosition.position, positionToGo, nextPositionChar.Value,
                        currentPosition.steps + 1));
                }
            }


            if (!nextPositions.Any())
            {
                break;
            }

            currentPositions = nextPositions;
        }

        return furthestPosition.Max();
    }


    private Position? GetPositionToGo((Position cameFrom, Position position, char PipeType, int steps) position)
    {
        var leftPosition = new Position(position.position.X - 1, position.position.Y);
        var rightPosition = new Position(position.position.X + 1, position.position.Y);
        var upperPosition = new Position(position.position.X, position.position.Y - 1);
        var bottomPosition = new Position(position.position.X, position.position.Y + 1);
        var leftChar = GetPositionChar(leftPosition);
        var rightChar = GetPositionChar(rightPosition);
        var upperChar = GetPositionChar(upperPosition);
        var bottomChar = GetPositionChar(bottomPosition);
        return position.PipeType switch
        {
            Pipes.VerticalPipe when position.position.Y - 1 == position.cameFrom.Y && bottomChar.HasValue &&
                                    Pipes.CanGoBottom(bottomChar.Value) => bottomPosition,
            Pipes.VerticalPipe when position.position.Y + 1 == position.cameFrom.Y && upperChar.HasValue &&
                                    Pipes.CanGoUp(upperChar.Value) => upperPosition,
            Pipes.HorizontalPipe when position.position.X - 1 == position.cameFrom.X && rightChar.HasValue &&
                                      Pipes.CanGoToRight(rightChar.Value) => rightPosition,
            Pipes.HorizontalPipe when position.position.X + 1 == position.cameFrom.X && leftChar.HasValue &&
                                      Pipes.CanGoToLeft(leftChar.Value) => leftPosition,
            Pipes.NorthEastBend when position.position.Y - 1 == position.cameFrom.Y && rightChar.HasValue &&
                                     Pipes.CanGoToRight(rightChar.Value) => rightPosition,
            Pipes.NorthEastBend when position.position.X + 1 == position.cameFrom.X && upperChar.HasValue &&
                                     Pipes.CanGoUp(upperChar.Value) => upperPosition,
            Pipes.NorthWestBend when position.position.Y - 1 == position.cameFrom.Y && leftChar.HasValue &&
                                     Pipes.CanGoToLeft(leftChar.Value) => leftPosition,
            Pipes.NorthWestBend when position.position.X - 1 == position.cameFrom.X && upperChar.HasValue &&
                                     Pipes.CanGoUp(upperChar.Value) => upperPosition,
            Pipes.SouthWestBend when position.position.Y + 1 == position.cameFrom.Y && leftChar.HasValue &&
                                     Pipes.CanGoToLeft(leftChar.Value) => leftPosition,
            Pipes.SouthWestBend when position.position.X - 1 == position.cameFrom.X && bottomChar.HasValue &&
                                     Pipes.CanGoBottom(bottomChar.Value) => bottomPosition,
            Pipes.SouthEastBend when position.position.Y + 1 == position.cameFrom.Y && rightChar.HasValue &&
                                     Pipes.CanGoToRight(rightChar.Value) => rightPosition,
            Pipes.SouthEastBend when position.position.X + 1 == position.cameFrom.X &&
                                     bottomChar.HasValue && Pipes.CanGoBottom(bottomChar.Value) => bottomPosition,
            _ => null
        };
    }

    private char? GetPositionChar(Position position)
    {
        if (Input.Length <= position.Y || position.Y < 0 || Input[position.Y].Length <= position.X ||  position.X < 0)
        {
            return null;
        }

        return Input[position.Y][position.X];
    }
}