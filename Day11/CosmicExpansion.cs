namespace Day11;

public class CosmicExpansion
{
    public CosmicExpansion(char[][] input, int expansion)
    {
        Input = input;
        Expansion = expansion;
    }

    private char[][] Input { get; }
    private int Expansion { get; }

    public long GetResult()
    {
        var space = Input.ToArray();
        var expandedYIds = ExpandSpaceVertically(space);
        var expandedXIds = ExpandSpaceHorizontally(space);
        var galaxiesPositions = GetGalaxiesPosition(space);
        var stepsBetweenPairs = GetStepsCountBetweenAllGalaxyPairs(galaxiesPositions, expandedYIds, expandedXIds);
        var result = stepsBetweenPairs.Aggregate((a, c) => a + c);
        return result;
    }

    private IEnumerable<long> GetStepsCountBetweenAllGalaxyPairs(List<Position> galaxiesPositions,
        IEnumerable<int> expandedYIds, IEnumerable<int> expandedXIds)
    {
        var galaxyIndex = 1;
        var steps = new List<long>();
        foreach (var galaxy in galaxiesPositions)
        {
            for (int i = galaxyIndex; i < galaxiesPositions.Count; i++)
            {
                var galaxyToGo = galaxiesPositions[i];
                var lowerX = galaxy.X >= galaxyToGo.X ? galaxyToGo.X : galaxy.X;
                var higherX = galaxy.X < galaxyToGo.X ? galaxyToGo.X : galaxy.X;

                var lowerY = galaxy.Y >= galaxyToGo.Y ? galaxyToGo.Y : galaxy.Y;
                var higherY = galaxy.Y < galaxyToGo.Y ? galaxyToGo.Y : galaxy.Y;
                var xRange = Enumerable.Range(lowerX, higherX - lowerX).ToList();
                var yRange = Enumerable.Range(lowerY, higherY - lowerY).ToList();

                var expandedRowsPassed = xRange.Count(e => expandedXIds.Any(y => y == e));
                var expandedColumnsPassed = yRange.Count(e => expandedYIds.Any(y => y == e));

                var x = galaxyToGo.X - galaxy.X;
                var y = galaxyToGo.Y - galaxy.Y;
                x = x <= 0 ? x : x * -1;
                y = y <= 0 ? y : y * -1;

                var howManySteps = y + x;
                if (howManySteps < 0)
                {
                    howManySteps *= -1;
                }

                howManySteps += expandedRowsPassed * Expansion;
                howManySteps += expandedColumnsPassed * Expansion;

                steps.Add(howManySteps);
            }

            galaxyIndex++;
        }

        return steps;
    }

    private static IEnumerable<int> ExpandSpaceVertically(IReadOnlyList<char[]> space)
    {
        List<int> expandedYIds = new();
        for (var i = 0; i < space.Count; i++)
        {
            var spaceRow = space[i];
            if (spaceRow.Any(e => e == '#'))
            {
                continue;
            }

            expandedYIds.Add(i);
        }

        return expandedYIds;
    }

    private static IEnumerable<int> ExpandSpaceHorizontally(char[][] originalSpace)
    {
        var expandedXIds = new List<int>();
        for (var y = 0; y < originalSpace.Length; y++)
        {
            var column = Enumerable.Range(0, originalSpace.GetLength(0))
                .Select(e => originalSpace[e][y])
                .ToArray();
            if (column.Any(e => e == '#'))
            {
                continue;
            }

            expandedXIds.Add(y);
        }

        return expandedXIds;
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