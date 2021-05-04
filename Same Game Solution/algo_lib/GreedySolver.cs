using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class GreedySolver : ISolver<GameState, SameGameSolution >
    {
        // public IEnumerable<SameGameSolution> GetSolutions(GameState problem, Countdown countdown)
        public IEnumerable<SameGameSolution> GetSolutions(GameState problem)
        {
            GameState coolerProblem = problem.copy();
            HashSet<Block> legals = coolerProblem.legals();
            List<Block> result = new List<Block>();
            while (legals.Count != 0)
            {
                IEnumerable<Block> maxBlocks = legals.Where(x => x.size == legals.Max(y => y.size));
                Block curBlock = maxBlocks.First();
                result.Add(curBlock);
                coolerProblem = coolerProblem.deleteBlock(curBlock);
                legals = coolerProblem.legals();
            }

            yield return new SameGameSolution(result.ToArray(), coolerProblem.Score);
        }
    }
}