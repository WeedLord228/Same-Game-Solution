using System.Collections.Generic;
using System.Linq;

namespace Same_Game_Solution.engine.visualizers
{
    public static class NumbersToGraphicConverter
    {
        private readonly static Dictionary<string, string> mapping = new()
            {{"-1", Emoji.Clubs}, {"1", Emoji.Spades}, {"2", Emoji.Diamonds}, {"3", Emoji.Hearts}};

        public static string Convert(string source)
        {
            return mapping.Aggregate(source, (current, value) =>
                current.Replace(value.Key, value.Value));
        }
    }
}