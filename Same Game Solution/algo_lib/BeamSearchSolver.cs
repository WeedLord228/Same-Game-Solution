using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class BeamSearchSolver : ISolver<GameState, SameGameSolution>
    {
        private int beamWidth;
        private int depth;
        private IEstimator _estimator;

        public BeamSearchSolver(int beamWidth, int depth, IEstimator _estimator)
        {
            this.beamWidth = beamWidth;
            this.depth = depth;
            this._estimator = _estimator;
        }


        public IEnumerable<SameGameSolution> GetSolutions(GameState problem)
        {
            var currentProblem = problem.copy();
            var legals = currentProblem.legals();
            var result = new List<Block>();
            var root = new TreeNode<Block>(
                0, new List<TreeNode<Block>>(), null, null, currentProblem);
            var searchTree = new Tree<Block>(root);
            while (!currentProblem.Terminal)
            {
                ApplyRecurssion(root, 0);
                root = searchTree.BestLeaf;
                currentProblem = root.GameState;
            }

            var bestPath = searchTree.GetBestPath();
            yield return new SameGameSolution(bestPath.ToArray(), searchTree.BestLeaf.GameState.Score);
        }

        private void ApplyRecurssion(TreeNode<Block> node, int depth)
        {
            if (depth == this.depth)
            {
                return;
            }

            if (node.Childs == null)
            {
                foreach (var turn in getNextTurns(node.GameState))
                {
                    var currentGameState = node.GameState.copy();
                    currentGameState.deleteBlock(turn.Item1);
                    node.Childs.Add(new TreeNode<Block>(turn.Item2, null, node, turn.Item1,
                        currentGameState));
                }
            }

            foreach (var child in node.Childs)
            {
                ApplyRecurssion(child, depth + 1);
            }
        }

        private List<(Block, double)> getNextTurns(GameState curGameState)
        {
            var initialLegals = curGameState.legals();
            var bestTurns = new List<(Block, double)>();

            foreach (var block in initialLegals)
            {
                var mutableGameState = curGameState.copy();
                mutableGameState.deleteBlock(block);
                var curScore = _estimator.Estimate(mutableGameState);
                bestTurns.Add((block, curScore));
            }

            bestTurns.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            return bestTurns.Take(beamWidth).ToList();
        }
    }
}