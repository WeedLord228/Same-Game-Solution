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
        public IEnumerable<SameGameSolution> GetSolutions(GameState problem, Countdown countdown = null)
        {
            var coolerProblem = problem.copy();
            var legals = coolerProblem.legals();
            var result = new List<Block>();
            while (legals.Count != 0)
            {
                var curBlock = getNextTurn(coolerProblem);
                result.Add(curBlock);
                coolerProblem.deleteBlock(curBlock);
                legals = coolerProblem.legals();
            }

            yield return new SameGameSolution(result.ToArray(), coolerProblem.Score);
        }

        private Block getNextTurn(GameState curGameState)
        {
            var initialLegals = curGameState.legals();
            Block bestTurn = null;
            var bestTurnScore = double.MinValue;


            foreach (var block in initialLegals)
            {
                var mutableGameState = curGameState.copy();
                mutableGameState.deleteBlock(block);
                var curScore = _estimator.Estimate(mutableGameState);
                if (!(curScore > bestTurnScore)) continue;
                bestTurn = block;
                bestTurnScore = curScore;
            }

            return bestTurn;
        }
    }
}