using System.Collections.Generic;
using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class GreedySolver : ISolver<GameState, SameGameSolution>
    {
        private IEstimator _estimator;

        public GreedySolver(IEstimator estimator)
        {
            _estimator = estimator;
        }

        // public IEnumerable<SameGameSolution> GetSolutions(GameState problem, Countdown countdown)
        public IEnumerable<SameGameSolution> GetSolutions(GameState problem)
        {
            var coolerProblem = problem.Copy();
            var legals = coolerProblem.Legals();
            var result = new List<Block>();
            while (legals.Count != 0)
            {
                var curBlock = GetNextTurn(coolerProblem);
                result.Add(curBlock);
                coolerProblem.DeleteBlock(curBlock);
                legals = coolerProblem.Legals();
            }

            yield return new SameGameSolution(result.ToArray(), coolerProblem.Score);
        }

        private Block GetNextTurn(GameState curGameState)
        {
            var initialLegals = curGameState.Legals();
            Block bestTurn = null;
            var bestTurnScore = double.MinValue;


            foreach (var block in initialLegals)
            {
                var mutableGameState = curGameState.Copy();
                mutableGameState.DeleteBlock(block);
                var curScore = _estimator.Estimate(mutableGameState);
                if (!(curScore > bestTurnScore)) continue;
                bestTurn = block;
                bestTurnScore = curScore;
            }

            return bestTurn;
        }
    }
}