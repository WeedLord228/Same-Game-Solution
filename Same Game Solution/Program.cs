using System;
using System.Linq;
using Same_Game_Solution.algo_lib;
using Same_Game_Solution.engine;
using Same_Game_Solution.engine.visualizers;

namespace Same_Game_Solution
{
    internal class Program
    {
        private static readonly GameStateSimpleVisualizer GameStateSimpleVisualizer = new();

        // private static TreeVisualizer _treeVisualizer = new TreeVisualizer();
        private static IBoardGetterService _simpleBoardGetterService;

        private static void Main(string[] args)
        {
            var board = SameGameRepo.real15x15board;

            _simpleBoardGetterService = new SimpleBoardGetterService(board);
            var initialGameState = new GameState(_simpleBoardGetterService.getBoard(), 0);
            GameStateSimpleVisualizer.render(initialGameState);
            Console.WriteLine("LEGALS:");
            var legals = initialGameState.legals();
            foreach (var block in legals) Console.WriteLine(block.ToString());

            var simpleScoreEstimator = new SimpleScoreEstimator();
            var boardwiseScoreEstimator = new BoardwiseScoreEstimator();
            var beamSearchScoreEstimator = new BeamSearchBoardwiseEstimator();

            var beamSearchSolver = new BeamSearchSolver(5, 3, new SimpleBeamSearchScoreEstimator());
            // var beamSearchSolver = new GreedySolver(boardwiseScoreEstimator);
            var greedySolver = new GreedySolver(boardwiseScoreEstimator);
            // var sameGameSimpleSolution = greedySolver.GetSolutions(initialGameState.copy()).First();
            var sameGameBeamSearchSolution = beamSearchSolver.GetSolutions(initialGameState.copy()).First();

            Console.WriteLine();
            Console.WriteLine("SIMPLE SCORE GAME ! ! ! ! ! ");
            foreach (var block in sameGameBeamSearchSolution.Turns)
            {
                Console.WriteLine(block);
                Console.WriteLine();
                initialGameState.deleteBlock(block);
                Console.WriteLine(initialGameState.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("FINAL SCORE:" + initialGameState.Score);

            // _treeVisualizer.render(beamSearchSolver.SearchTree);


//------------------------------------------------------------------------------------------------------------------------

            //
            // #region Boardwise Score
            //
            // BoardwiseScoreEstimator boardwiseScoreEstimator = new BoardwiseScoreEstimator();
            // greedySolver.changeEstimator(boardwiseScoreEstimator);
            // SameGameSolution sameGameBoardwiseSolution = greedySolver.GetSolutions(initialGameState).First();
            //
            // Console.WriteLine();
            // Console.WriteLine("BOARDWISE SCORE GAME ! ! ! ! ! ");
            // foreach (Block block in sameGameSimpleSolution.Turns)
            // {
            //     Console.WriteLine(block);
            //     Console.WriteLine();
            //     initialGameState.deleteBlock(block);
            //     Console.WriteLine(initialGameState.ToString());
            // }
            //
            // Console.WriteLine();
            // Console.WriteLine("FINAL SCORE:" + sameGameSimpleSolution.Score);
            //
            // #endregion

            // Block turn1 = legals.First(x => x.color == 1);
            // GameState newGameState = initialGameState.deleteBlock(turn1);
            // // Console.WriteLine(newGameState.terminal);
            // Console.WriteLine("TURN 1: " + turn1.ToString());
            // _visualizer.render(newGameState);
            // Console.WriteLine("LEGALS 1:");
            // HashSet<Block> legals1 = newGameState.legals();
            // foreach (Block block in legals1)
            // {
            //     Console.WriteLine(block.ToString());
            // }
        }
    }
}