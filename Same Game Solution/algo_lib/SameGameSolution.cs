using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class SameGameSolution : ISolution
    {
        public readonly Block[] Turns;

        public SameGameSolution(Block[] turns, double score)
        {
            Turns = turns;
            Score = score;
        }

        public double Score { get; set; }
    }
}