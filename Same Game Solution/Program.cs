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
            testLogic(args);
        }

        private static void mainLogic(string[] args)
        {
            var board = SameGameRepo.fiveOnFourboard;

            _simpleBoardGetterService = new SimpleBoardGetterService(board);
            var initialGameState = new GameState(_simpleBoardGetterService.getBoard(), 0);
            GameStateSimpleVisualizer.render(initialGameState);
            Console.WriteLine("LEGALS:");
            var legals = initialGameState.legals();
            foreach (var block in legals) Console.WriteLine(block.ToString());

            var beamSearchSolver = new TreeSearchSolver(1, 1, new IEstimator[] {new BeamSearchBoardwiseEstimator()});
            // var beamSearchSolver = new GreedySolver(boardwiseScoreEstimator);
            var sameGameBeamSearchSolution = beamSearchSolver.GetSolutions(initialGameState.copy()).First();

            Console.WriteLine();
            Console.WriteLine("SIMPLE SCORE GAME ! ! ! ! ! ");
            foreach (var block in sameGameBeamSearchSolution.Turns)
            {
                // _treeVisualizer.render(beamSearchSolver.SearchTree, false);
                Console.WriteLine(block);
                Console.WriteLine();
                initialGameState.deleteBlock(block);
                Console.WriteLine(initialGameState.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("FINAL SCORE:" + initialGameState.Score);

            _treeVisualizer.render(beamSearchSolver.SearchTree, false);
        }

        private static void testLogic(string[] args)
        {
            var board = SameGameRepo.real15x15board;
            
            _simpleBoardGetterService = new SimpleBoardGetterService(board);

            var beamSearchSolver = new BeamSearchSolver(new BeamSearchBoardwiseEstimator(), 3);
            var greedySolver = new GreedySolver(new BeamSearchBoardwiseEstimator());
            
            var initialGameState = new GameState(_simpleBoardGetterService.getBoard(), 0);
            var turns = beamSearchSolver.GetBestTurns(new[] {initialGameState});
            var beamSearchSolutin = beamSearchSolver.GetSolutions(initialGameState, new Countdown(20000));
            // var greedySolution = greedySolver.GetSolutions(initialGameState);
            _treeVisualizer.render(beamSearchSolver.SearchTree, true);
            // Console.WriteLine(greedySolution.First().Score);
        }
    }
}