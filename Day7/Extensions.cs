using System.Collections;

namespace Day7;

public static class Extensions
{
    public class CustomSorter : IComparer<(string, int, string)>
    {
        public int Compare((string, int, string) hand1, (string, int, string) hand2)
        {
            if (hand1.Item2 > hand2.Item2)
            {
                return 1;
            }

            if (hand2.Item2 > hand1.Item2)
            {
                return -1;
            }

            var chars = hand1.Item3;
            for (var i = 0; i < hand1.Item1.Length; i++)
            {
                if (chars.IndexOf(hand1.Item1[i], StringComparison.Ordinal) <
                    chars.IndexOf(hand2.Item1[i], StringComparison.Ordinal))
                    return 1;
                if (chars.IndexOf(hand1.Item1[i], StringComparison.Ordinal) >
                    chars.IndexOf(hand2.Item1[i], StringComparison.Ordinal))
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}