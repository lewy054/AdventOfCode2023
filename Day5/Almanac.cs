using System.Text.RegularExpressions;

namespace Day5;

public class Almanac
{
    public Almanac(List<string> input)
    {
        Input = input;
        SetSeeds();
        SetCategories();
    }

    private List<string> Input { get; set; }

    public List<long> Seeds { get; private set; } = new();
    public List<Map> SeedToSoil { get; private set; } = new();
    public List<Map> SoilToFertilizer { get; private set; } = new();
    public List<Map> FertilizerToWater { get; private set; } = new();
    public List<Map> WaterToLight { get; private set; } = new();
    public List<Map> LightToTemperature { get; private set; } = new();
    public List<Map> TemperatureToHumidity { get; private set; } = new();
    public List<Map> HumidityToLocation { get; private set; } = new();


    private void SetSeeds()
    {
        Seeds = Regex.Matches(Input.First(), @"\d+").Select(e => long.Parse(e.Value)).ToList();
    }

    private void SetCategories()
    {
        var categories = GetCategories().ToList();
        SeedToSoil = categories[0];
        SoilToFertilizer = categories[1];
        FertilizerToWater = categories[2];
        WaterToLight = categories[3];
        LightToTemperature = categories[4];
        TemperatureToHumidity = categories[5];
        HumidityToLocation = categories[6];

    }

    private IEnumerable<List<Map>> GetCategories()
    {
        const int categoriesQuantity = 7;
        var lines = Input.Skip(2).ToList();
        var index = 0;
        for (var i = 0; i < categoriesQuantity; i++)
        {
            var maps = new List<Map>();
            var category = lines.Skip(index).TakeWhile(e => !string.IsNullOrEmpty(e)).ToList();
            index += category.Count + 1;

            for (var j = 1; j < category.Count; j++)
            {
                var values = category[j].Split(" ").Select(long.Parse).ToList();
                maps.Add(new Map(values.ElementAt(0),
                    values.ElementAt(1), values.ElementAt(2)));
            }

            yield return maps;
        }
    }
}