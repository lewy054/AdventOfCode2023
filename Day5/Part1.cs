using System.Text.RegularExpressions;
using Helpers;

namespace Day5;

public class Part1
{
    public Part1(IList<string> input)
    {
        Input = input;
    }

    public IList<string> Input { get; init; }

    public long GetResult()
    {
        var almanac = new Almanac(Input.ToList());
        var locations = new List<long>();
        foreach (var seed in almanac.Seeds)
        {
            var soil = almanac.SeedToSoil.GetDestinationValue(seed);
            var fertilizer = almanac.SoilToFertilizer.GetDestinationValue(soil);
            var water = almanac.FertilizerToWater.GetDestinationValue(fertilizer);
            var light = almanac.WaterToLight.GetDestinationValue(water);
            var temperature = almanac.LightToTemperature.GetDestinationValue(light);
            var humidity = almanac.TemperatureToHumidity.GetDestinationValue(temperature);
            var location = almanac.HumidityToLocation.GetDestinationValue(humidity);
            locations.Add(location);
        }

        var minLocation = locations.Min();
        return minLocation;
    }
}