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

        private static TreeVisualizer _treeVisualizer = new TreeVisualizer();
        private static IBoardGetterService _simpleBoardGetterService;

        private static void Main(string[] args)
        {
            var board = SameGameRepo.real15x15board;

            _simpleBoardGetterService = new SimpleBoardGetterService(board);
            var initialGameState = new GameState(_simpleBoardGetterService.getBoard(), 0);
            GameStateSimpleVisualizer.render(initialGameState);
            Console.WriteLine("LEGALS:");
            var legals = initialGameState.Legals();
            foreach (var block in legals) Console.WriteLine(block.ToString());

            var beamSearchSolver = new BeamSearchSolver(3, 3, new IEstimator[] {new BoardwiseScoreEstimator(), new SimpleScoreEstimator()});
            // var beamSearchSolver = new BeamSearchSolver(2, 3, new IEstimator[] {new BeamSearchBoardwiseEstimator()});
            // var beamSearchSolver = new GreedySolver(boardwiseScoreEstimator);
            var sameGameBeamSearchSolution = beamSearchSolver.GetSolutions(initialGameState.Copy()).First();

            Console.WriteLine();
            Console.WriteLine("SIMPLE SCORE GAME ! ! ! ! ! ");
            foreach (var block in sameGameBeamSearchSolution.Turns)
            {
                Console.WriteLine(block);
                Console.WriteLine();
                initialGameState.DeleteBlock(block);
                Console.WriteLine(initialGameState.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("FINAL SCORE:" + initialGameState.Score);

            _treeVisualizer.render(beamSearchSolver.SearchTree, true);
        }
    }
}