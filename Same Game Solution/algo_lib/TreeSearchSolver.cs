using System.Collections.Generic;
using System.Linq;
using Same_Game_Solution.engine;
using Same_Game_Solution.engine.visualizers;

namespace Same_Game_Solution.algo_lib
{
    public class TreeSearchSolver : ISolver<GameState, SameGameSolution>
    {
        private readonly int _beamWidth;
        private readonly int _depth;
        private IEstimator _currentEstimator;
        private readonly IEnumerable<IEstimator> _estimators;

        public TreeSearchSolver(int beamWidth, int depth, IEstimator currentEstimator)
        {
            _beamWidth = beamWidth;
            _depth = depth;
            _currentEstimator = currentEstimator;
        }

        public TreeSearchSolver(int beamWidth, int depth, IEnumerable<IEstimator> estimators)
        {
            _beamWidth = beamWidth;
            _depth = depth;
            _estimators = estimators;
            _currentEstimator = estimators.First();
        }

        public Tree<Block> SearchTree { get; set; }


        public IEnumerable<SameGameSolution> GetSolutions(GameState problem, Countdown countdown = null)
        {
            var currentProblem = problem.copy();
            var root = new TreeNode<Block>(
                0, null, null, null, currentProblem);
            var potentialResults = new List<TreeNode<Block>>();
            SearchTree = new Tree<Block>(root) {LocalRoot = root};
            while (!currentProblem.Terminal)
            {
                // StaticTreeVisualizer.render(SearchTree);
                ApplyRecursion(SearchTree.LocalRoot, 0);
                potentialResults.AddRange(SearchTree.Shrink());
                SearchTree.LocalRoot = SearchTree.GetNextRoot(SearchTree.BestLeaf, SearchTree.LocalRoot);
                currentProblem = SearchTree.LocalRoot.GameState;
                
            }

            var bestPath = SearchTree.GetBestPath();
            yield return new SameGameSolution(bestPath.ToArray(), SearchTree.BestLeaf.GameState.Score);
        }

        private void ApplyRecursion(TreeNode<Block> node, int recDepth)
        {
            if (recDepth == _depth - 1)
            {
                _currentEstimator = _estimators.Last();
            }
            else
            {
                _currentEstimator = _estimators.First();
            }

            var nextTurns = getNextTurns(node.GameState);

            if (nextTurns.Count == 0)
            {
                node.Score = _currentEstimator.Estimate(node.GameState);
                return;
            }

            if (recDepth == _depth) return;

            if (node.Children == null)
            {
                node.Children = new List<TreeNode<Block>>();
                foreach (var (block, score) in nextTurns)
                {
                    var currentGameState = node.GameState.copy();
                    currentGameState.deleteBlock(block);
                    node.Children.Add(new TreeNode<Block>(score, null, node, block,
                        currentGameState));
                }
            }

            foreach (var child in node.Children) ApplyRecursion(child, recDepth + 1);
        }

        private List<(Block, double)> getNextTurns(GameState curGameState)
        {
            var initialLegals = curGameState.legals();
            var bestTurns = new List<(Block, double)>();

            foreach (var block in initialLegals)
            {
                var mutableGameState = curGameState.copy();
                mutableGameState.deleteBlock(block);
                var curScore = _currentEstimator.Estimate(mutableGameState);
                bestTurns.Add((block, curScore));
            }

            bestTurns.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            return bestTurns.Take(_beamWidth).ToList();
        }
    }
}