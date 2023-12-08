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
    public List<(long start, long end)> SeedsRange { get; private set; } = new();
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
        for (var i = 0; i < Seeds.Count; i += 2)
        {
            SeedsRange.Add((Seeds[i], Seeds[i] + Seeds[i + 1]));
        }
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
    
    public bool SeedExists(long seed)
    {
        return SeedsRange.Any(e => e.start <= seed && e.end >= seed);
    }
    
    public long FindLocationForSeed(long seed)
    {
        var soil = SeedToSoil.GetDestinationValue(seed);
        var fertilizer = SoilToFertilizer.GetDestinationValue(soil);
        var water = FertilizerToWater.GetDestinationValue(fertilizer);
        var light = WaterToLight.GetDestinationValue(water);
        var temperature = LightToTemperature.GetDestinationValue(light);
        var humidity = TemperatureToHumidity.GetDestinationValue(temperature);
        var location = HumidityToLocation.GetDestinationValue(humidity);
        return location;
    }

    public long FindSeedForLocation(long location)
    {
        var humidity = HumidityToLocation.GetSourceValue(location);
        var temperature = TemperatureToHumidity.GetSourceValue(humidity);
        var light = LightToTemperature.GetSourceValue(temperature);
        var water = WaterToLight.GetSourceValue(light);
        var fertilizer = FertilizerToWater.GetSourceValue(water);
        var soil = SoilToFertilizer.GetSourceValue(fertilizer);
        var seed = SeedToSoil.GetSourceValue(soil);
        return seed;
    }

    public long GetSeedMaxRange()
    {
        long maxRange = 0;
        for (var i = 0; i < Seeds.Count; i+=2)
        {
            var setRange = Seeds[i] + Seeds[i + 1];
            if (setRange > maxRange)
            {
                maxRange = setRange;
            }
        }

        return maxRange;
    }
}