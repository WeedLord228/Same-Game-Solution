using System;
using System.Collections.Generic;
using System.Linq;
using Same_Game_Solution.algo_lib;
using Same_Game_Solution.engine;

namespace Same_Game_Solution
{
    class Program
    {
        private static IVisualizer _visualizer = new SimpleConsoleVisualizer();
        private static IBoardGetterService _sbgt;

        static void Main(string[] args)
        {
            int[] colors = {1, 2, 3};
            // int[][] board = {
            //     new int[] {1, 1, 1, 1},
            //     new int[] {2, 2, 2, 2},
            //     new int[] {4, 4, 4, 4},
            //     new int[] {3, 3, 3, 3}
            // };
            // int[][] board = {
            //     new int[] {4, 3, 2, 1},
            //     new int[] {4, 3, 2, 1},
            //     new int[] {4, 3, 2, 1},
            //     new int[] {4, 3, 2, 1}
            // };
            int[][] board =
            {
                new int[] {4, 4, 1, 9},
                new int[] {4, 3, 1, 9},
                new int[] {1, 1, 1, 9},
                new int[] {1, 1, 1, 9},
                new int[] {7, 7, 1, 9}
            };

            _sbgt = new SimpleBoardGetterService(board);
            var initialGameState = new GameState(_sbgt.getBoard(), 0);
            _visualizer.render(initialGameState);
            Console.WriteLine("LEGALS:");
            var legals = initialGameState.legals();
            foreach (var block in legals)
            {
                Console.WriteLine(block.ToString());
            }

            var simpleScoreEstimator = new SimpleScoreEstimator();
            var boardwiseScoreEstimator = new BoardwiseScoreEstimator();

            #region Simple Score

            var greedySolver = new GreedySolver(simpleScoreEstimator);
            var sameGameSimpleSolution = greedySolver.GetSolutions(initialGameState.copy()).First();

            Console.WriteLine();
            Console.WriteLine("SIMPLE SCORE GAME ! ! ! ! ! ");
            foreach (var block in sameGameSimpleSolution.Turns)
            {
                Console.WriteLine(block);
                Console.WriteLine();
                initialGameState.deleteBlock(block);
                Console.WriteLine(initialGameState.ToString());
            }
            
            Console.WriteLine();
            Console.WriteLine("FINAL SCORE:" + sameGameSimpleSolution.Score);
            #endregion
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