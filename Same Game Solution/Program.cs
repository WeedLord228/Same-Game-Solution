using System;
using System.Collections.Generic;
using System.Linq;
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
            int[][] board = {
                new int[] {1, 1, 1, 1},
                new int[] {2, 2, 2, 2},
                new int[] {4, 4, 4, 4},
                new int[] {3, 3, 3, 3}
            };
            // // int[][] board = {
            // //     new int[] {4, 3, 2, 1},
            // //     new int[] {4, 3, 2, 1},
            // //     new int[] {4, 3, 2, 1},
            // //     new int[] {4, 3, 2, 1}
            // // };
            // int[][] board =
            // {
            //     new int[] {4, 4, 5, 5},
            //     new int[] {4, 3, 3, 3},
            //     new int[] {1, 1, 1, 1},
            //     new int[] {2, 2, 2, 2}
            // };

            _sbgt = new SimpleBoardGetterService(board);
            GameState initialGameState = new GameState(_sbgt.getBoard(), 0);
            _visualizer.render(initialGameState);
            Console.WriteLine("LEGALS:");
            HashSet<Block> legals = initialGameState.legals();
            foreach (Block block in legals)
            {
                Console.WriteLine(block.ToString());
            }

            Block turn1 = legals.First(x => x.color == 2);
            GameState newGameState = initialGameState.deleteBlock(turn1);

            Console.WriteLine("TURN 1: " + turn1.ToString());
            _visualizer.render(newGameState);
            Console.WriteLine("LEGALS 1:");
            HashSet<Block> legals1 = newGameState.legals();
            foreach (Block block in legals1)
            {
                Console.WriteLine(block.ToString());
            }
        }
    }
}