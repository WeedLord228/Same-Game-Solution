using System;
using System.Linq;
using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class BoardwiseScoreEstimator : IEstimator
    {
        public double Estimate(GameState gameState)
        {
            double result = 0;
            foreach (Block block in gameState.legals())
            {
                result += (block.size - 2) * (block.size - 2);
            }
            
            return result;
        }
    }
}