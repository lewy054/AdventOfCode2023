namespace Day3;

public class AdjacentNumberList
{
    public List<int> Numbers { get; private set; } = new();
    
    public void Add(int number)
    {
        if (number != -1)
        {
            Numbers.Add(number);
        }
    }
}