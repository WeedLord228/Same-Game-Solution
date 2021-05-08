using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class BeamSearchBoardwiseEstimator : IEstimator
    {
        public double Estimate(GameState gameState)
        {
            double result = 0;
            foreach (var block in gameState.legals())
            {
                result += (block.size - 2) * (block.size - 2) * 100000;
            }

            result += gameState.Score;
            return result;
        }
    }
}