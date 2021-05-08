using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public interface IEstimator
    {
        public double Estimate(GameState gameState);
    }
}