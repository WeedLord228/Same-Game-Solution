using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;
using Same_Game_Solution.algo_lib;
using Same_Game_Solution.engine;
using Node = Microsoft.Msagl.Drawing.Node;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Same_Game_Solution
{
    class Program
    {
        private static GameStateSimpleVisualizer _gameStateSimpleVisualizer = new GameStateSimpleVisualizer();
        private static TreeVisualizer _treeVisualizer = new TreeVisualizer();
        private static IBoardGetterService _sbgt;
        
        static void Main(string[] args)
        {
            int[] colors = {1, 2, 3};

            int[][] board = SameGameRepo.real15x15board;

                _sbgt = new SimpleBoardGetterService(board);
            var initialGameState = new GameState(_sbgt.getBoard(), 0);
            _gameStateSimpleVisualizer.render(initialGameState);
            Console.WriteLine("LEGALS:");
            var legals = initialGameState.legals();
            foreach (var block in legals)
            {
                Console.WriteLine(block.ToString());
            }

            var simpleScoreEstimator = new SimpleScoreEstimator();
            var boardwiseScoreEstimator = new BoardwiseScoreEstimator();
            var beamSearchScroeEstimator = new BeamSearchBoardwiseEstimator();

            var beamSearchSolver = new BeamSearchSolver(3, 4, beamSearchScroeEstimator);
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