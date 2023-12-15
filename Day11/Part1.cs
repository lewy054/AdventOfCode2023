namespace Day11;

public class Part1
{
    public Part1(char[][] input)
    {
        Input = input;
    }

    private char[][] Input { get; }


    public int GetResult()
    {
        var space = Input.ToArray();
        var verticallyExpandedSpace = ExpandSpaceVertically(space);
        var spaceExpanded = ExpandSpaceHorizontally(verticallyExpandedSpace, space);
        var galaxiesPositions = GetGalaxiesPosition(spaceExpanded);
        var stepsBetweenPairs = GetStepsCountBetweenAllGalaxyPairs(spaceExpanded, galaxiesPositions);
        return stepsBetweenPairs.Sum();
    }

    private IEnumerable<int> GetStepsCountBetweenAllGalaxyPairs(char[][] spaceExpanded,
        List<Position> galaxiesPositions)
    {
        var galaxyIndex = 1;
        var steps = new List<int>();
        foreach (var galaxy in galaxiesPositions)
        {
            for (int i = galaxyIndex; i < galaxiesPositions.Count; i++)
            {
                var galaxyToGo = galaxiesPositions[i];
                var x = galaxyToGo.X - galaxy.X;
                var y = galaxyToGo.Y - galaxy.Y;
                var howManySteps = (y <= 0 ? y : y * -1) + (x <= 0 ? x : x * -1);
                if (howManySteps < 0)
                {
                    howManySteps *= -1;
                }

                steps.Add(howManySteps);
            }

            galaxyIndex++;
        }

        return steps;
    }

    private char[][] ExpandSpaceVertically(char[][] space)
    {
        var spaceAfterExpand = space.ToList();
        var columnIndex = 0;
        for (var i = 0; i < space.Length; i++)
        {
            var spaceRow = space[i];
            if (spaceRow.Any(e => e == '#'))
            {
                continue;
            }

            var rowToAdd = new char[space.Length];
            Array.Fill(rowToAdd, '.');
            spaceAfterExpand.Insert(columnIndex + i, rowToAdd);
            columnIndex++;
        }

        return spaceAfterExpand.ToArray();
    }

    private char[][] ExpandSpaceHorizontally(char[][] verticallyExpandedSpace, char[][] originalSpace)
    {
        var rowIndex = 0;
        var expandedSpace = verticallyExpandedSpace.Select(e => e.ToList()).ToList();
        for (var y = 0; y < originalSpace.Length; y++)
        {
            var column = Enumerable.Range(0, originalSpace.GetLength(0))
                .Select(e => originalSpace[e][y])
                .ToArray();
            if (column.Any(e => e == '#'))
            {
                continue;
            }

            foreach (var row in expandedSpace)
            {
                row.Insert(y + rowIndex, '.');
            }

            rowIndex++;
        }

        return expandedSpace.Select(e => e.ToArray()).ToArray();
    }

    private List<Position> GetGalaxiesPosition(char[][] spaceExpanded)
    {
        var galaxies = new List<Position>();
        for (var y = 0; y < spaceExpanded.Length; y++)
        {
            for (var x = 0; x < spaceExpanded[y].Length; x++)
            {
                if (spaceExpanded[y][x].Equals('#'))
                {
                    galaxies.Add(new Position(x, y));
                }
            }
        }

        return galaxies;
    }
}