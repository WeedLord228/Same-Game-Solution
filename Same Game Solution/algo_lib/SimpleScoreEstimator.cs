using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class SimpleScoreEstimator : IEstimator
    {
        public double Estimate(GameState gameState)
        {
            return gameState.Score;
        }
    }
}