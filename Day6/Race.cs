namespace Day6;

public class Race
{
    public Race(long duration, long record)
    {
        Duration = duration;
        Record = record;
    }

    private long Record { get; set; }
    private long Duration { get; set; }


    public int GetNumberOfPossibleWaysToWin()
    {
        var waysToWin = 0;
        for (var j = 1; j < Duration; j++) // time to hold button
        {
            var timeToTravel = Duration - j;
            var score = timeToTravel * j;
            if (score > Record)
            {
                waysToWin += 1;
            }
        }

        return waysToWin;
    }
}