using System;
using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class BeamSearchBoardwiseEstimator : IEstimator
    {
        private int coef;
        public BeamSearchBoardwiseEstimator(int coef = 1000)
        {
            this.coef = coef;
            
        }
        public double Estimate(GameState gameState)
        {
            double result = 0;
            foreach (var block in gameState.legals()) result += (block.Size - 2) * (block.Size - 2) * coef;

            result += gameState.Score;
            return result;
        }
    }
}