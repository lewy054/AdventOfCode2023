namespace Day9;

public class Part2 : Part1
{
    private readonly List<string> _input;

    public Part2(List<string> input) : base(input)
    {
        _input = input;
    }

    protected override List<string> GetInput => ReverseInput();

    private List<string> ReverseInput()
    {
        return _input.Select(elem => string.Join(" ", elem.Split(" ").Reverse())).ToList();
    }
}