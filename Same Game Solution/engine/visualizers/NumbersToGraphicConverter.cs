using System.Collections.Generic;
using System.Linq;

namespace Same_Game_Solution.engine.visualizers
{
    public static class NumbersToGraphicConverter
    {
        private readonly static Dictionary<string, string> mapping = new()
            {{"-1", "0"}, {"1", "1"}, {"2", "2"}, {"3", "3"}};

        public static string Convert(string source)
        {
            return mapping.Aggregate(source, (current, value) =>
                current.Replace(value.Key, value.Value));
        }
    }
}