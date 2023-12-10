namespace Day8;

public class Node
{
    public Node(string name, string leftValue, string rightValue)
    {
        Name = name;
        LeftValue = leftValue;
        RightValue = rightValue;
    }

    public string Name { get; set; }
    public string LeftValue { get; set; }
    public string RightValue { get; set; }
}