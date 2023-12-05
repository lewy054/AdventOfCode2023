namespace Day3;

public class ValidatedList
{
    public List<int> Numbers { get; private set; } = new();

    public void Add(string number)
    {
        if (number != "")
        {
            Numbers.Add(int.Parse(number));
        }
    }

    public void AddRange(List<int> numbers)
    {
        foreach (var number in numbers.Where(number => number != -1))
        {
            Numbers.Add(number);
        }
    }
}