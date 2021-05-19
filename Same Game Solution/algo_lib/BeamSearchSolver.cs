using System.Collections.Generic;
using System.Linq;
using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class BeamSearchSolver : ISolver<GameState, SameGameSolution>
    {
        private IEstimator _estimator;
        private int _beamWidth;
        public BeamSearchTree<Block> SearchTree { get; set; }

        public BeamSearchSolver(IEstimator estimator, int beamWidth)
        {
            _estimator = estimator;
            _beamWidth = beamWidth;
        }

        public IEnumerable<SameGameSolution> GetSolutions(GameState problem, Countdown countdown)
        {
            var beams = new List<TreeNode<Block>>();
            SearchTree = new BeamSearchTree<Block>(new TreeNode<Block>(0, null, null, null, problem.copy()));
            beams.Add(SearchTree.Root);

            while (!countdown.IsFinished() && beams.Any(x => x.GameState.legals().Count > 0))
            {
                var bestTurns = GetBestTurns(beams.Select(beam => beam.GameState).ToList());
                var tempBeams = new List<TreeNode<Block>>();

                foreach (var (block, score, gameState, prevGameState) in bestTurns)
                {
                    var copyOfProblem = gameState.copy();
                    var batya = SearchTree.GetNodeByGameState(prevGameState);
                    var node = new TreeNode<Block>(score, null, batya, block, copyOfProblem);
                    if (batya.Children != null)
                    {
                        batya.Children.Add(node);
                    }
                    else
                    {
                        batya.Children = new List<TreeNode<Block>> {node};
                    }

                    tempBeams.Add(node);
                }

                foreach (var beam in beams)
                {
                    if (tempBeams.Any(x => x.Score <= beam.Score && beam.GameState.Terminal))
                    {
                        tempBeams.Add(beam);
                    }
                }

                beams = tempBeams;
            }

            var bestBeam = beams.Aggregate((x, y) => x.GameState.Score > y.GameState.Score ? x : y);
            return new[] {new SameGameSolution(SearchTree.GetBlockPath(bestBeam).ToArray(), bestBeam.GameState.Score)};
        }


        public IEnumerable<(Block, double, GameState, GameState)> GetBestTurns(IEnumerable<GameState> gameStates)
        {
            var result = new List<(Block, double, GameState, GameState)>();

            foreach (var gameState in gameStates)
            {
                foreach (var legal in gameState.legals())
                {
                    var gameStateCopy = gameState.copy();
                    gameStateCopy.deleteBlock(legal);
                    result.Add((legal, _estimator.Estimate(gameStateCopy), gameStateCopy, gameState));
                }
            }

            result = result.GroupBy(x => x.Item3).Select(y => y.First()).ToList();
            result.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            result = result.Take(_beamWidth).ToList();
            return result;
        }
    }
}