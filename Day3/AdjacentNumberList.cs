namespace Day3;

public class AdjacentNumberList
{
    public List<int> Numbers { get; private set; } = new();
    public void Add(string number)
    {
        if (number != "")
        {
            Console.WriteLine($"Adding:{number}");
            Numbers.Add(int.Parse(number));
        }
    }
}