using System.Text.RegularExpressions;
using Helpers;

namespace Day5;

public class Part1 : IResolver
{
    public Part1(IList<string> input)
    {
        Input = input;
    }

    public IList<string> Input { get; init; }

    public int GetResult()
    {
        var almanac = new Almanac(Input.ToList());
        foreach (var seed in almanac.Seeds)
        {
            // var soil = 
        }
        return 0;
    }
    
    // private 
    
}