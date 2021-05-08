using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class BoardwiseScoreEstimator : IEstimator
    {
        public double Estimate(GameState gameState)
        {
            double result = 0;
            foreach (var block in gameState.legals()) result += (block.Size - 2) * (block.Size - 2);

            return result;
        }
    }
}