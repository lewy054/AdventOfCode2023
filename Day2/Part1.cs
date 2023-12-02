using System.Text.RegularExpressions;
using Helpers;

namespace Day2;

public class Part1
{
    public Part1(string filename, Record bagLoad)
    {
        Filename = filename;
        BagLoad = bagLoad;
    }

    private string Filename { get; }
    private Record BagLoad { get; }
    private static int IdIndex => 5;
    private static char GameStartChar => ':';
    private const string GamesSeparator = "; ";

    public virtual int GetResult()
    {
        var input = FileHelpers.GetFileContent(Filename);
        var records = GetRecords(input);
        var possibleGames = DetermineWhichRecordsArePossible(records);
        var result = possibleGames.Sum(e => e.Key);
        return result;
    }

    protected static Dictionary<int, List<Record>> GetRecords(IEnumerable<string> input)
    {
        var records = new Dictionary<int, List<Record>>();
        foreach (var record in input)
        {
            var idString = string.Concat(record.Skip(IdIndex - 1).TakeWhile(e => e != GameStartChar));
            var id = int.Parse(idString ?? throw new InvalidOperationException(nameof(idString)));
            var gamesInOneRecord = record.Split(GamesSeparator);
            var recordResult = GetColorsOccurenceInOneRecord(gamesInOneRecord);
            records.Add(id, recordResult);
        }

        return records;
    }

    private static List<Record> GetColorsOccurenceInOneRecord(IEnumerable<string> games)
    {
        return games.Select(game => new Record()
        {
            BlueQuantity = GetOneColorQuantityInGame(game, "blue"),
            RedQuantity = GetOneColorQuantityInGame(game, "red"),
            GreenQuantity = GetOneColorQuantityInGame(game, "green"),
        }).ToList();
    }

    private static int GetOneColorQuantityInGame(string game, string color)
    {
        var result = 0;
        var pattern = $@"(\d+)[^\d]+{color}";
        var values = Regex.Matches(game, pattern);
        foreach (Match value in values)
        {
            for (var i = 1; i < value.Groups.Count; i++)
            {
                result += int.Parse(value.Groups[i].ToString());
            }
        }

        return result;
    }

    private Dictionary<int, List<Record>> DetermineWhichRecordsArePossible(Dictionary<int, List<Record>> allRecords)
    {
        var result = new Dictionary<int, List<Record>>();
        foreach (var record in allRecords)
        {
            var possibleRecords = record.Value.Where(recordValue =>
                    recordValue.BlueQuantity <= BagLoad.BlueQuantity &&
                    recordValue.GreenQuantity <= BagLoad.GreenQuantity &&
                    recordValue.RedQuantity <= BagLoad.RedQuantity)
                .ToList();
            if (possibleRecords.Count == record.Value.Count)
            {
                result.Add(record.Key, record.Value);
            }
        }

        return result;
    }
}