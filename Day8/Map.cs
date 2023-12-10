namespace Day8;

public class Map
{
    public Map(string instructions, List<Node> nodes)
    {
        Instructions = instructions;
        Nodes = nodes;
    }

    private string Instructions { get; }
    private List<Node> Nodes { get; }
    private static string StartPositionName => "AAA";
    private static string EndPositionName => "ZZZ";


    public int GetResult()
    {
        var position = Nodes.FirstOrDefault(e=> e.Name == StartPositionName);
        var stepsCount = 0;
        var escaped = false;
        while (!escaped)
        {
            foreach (var instruction in Instructions)
            {
                position = instruction switch
                {
                    'L' => Nodes.FirstOrDefault(e => e.Name == position?.LeftValue),
                    'R' => Nodes.FirstOrDefault(e => e.Name == position?.RightValue),
                    _ => throw new Exception()
                };
                stepsCount++;
                if (position?.Name == EndPositionName)
                {
                    escaped = true;
                }
            }
        }
        return stepsCount;
    }
}