namespace Day8;

public static class Helpers
{
    public static List<Node> MapNodes(List<string> instructions)
    {
        var nodes1 = new List<Node>();
        foreach (var instruction in instructions)
        {
            var nameWithRestInfo = instruction.Split(" = ");
            var leftAndRightValue = nameWithRestInfo[1].Split(", ");
            var name = nameWithRestInfo[0];
            var leftValue = leftAndRightValue[0].Replace("(", string.Empty);
            var rightValue = leftAndRightValue[1].Replace(")", string.Empty);
            nodes1.Add(new Node(name, leftValue, rightValue));
        }

        return nodes1;
    }
}