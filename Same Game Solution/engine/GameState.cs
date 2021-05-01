using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace Same_Game_Solution.engine
{
    public class GameState
    {
        private int[][] board;
        private int score;
        private bool terminal;

        public GameState(int[][] board, int score)
        {
            this.board = board;
            this.score = score;
        }
        

        public GameState deleteBlock(Block turn)
        {
            foreach (var point in turn.points)
            {
                board[point.Item1][point.Item2] = -1;
            }
            
            normalizeHorizontaly();
            normalizeVertically();
            
            GameState gameState = new GameState(board, score + (turn.size - 2) * (turn.size - 2));

            if (gameState.legals().Count != 0)
            {
                return gameState;
            }

            this.terminal = true;
            if (board[0][0] == -1)
            {
                score += 1000;
            }
            return this;
        }

        public GameState copy()
        {
            return new GameState(board, score);
        }

        public HashSet<Block> legals()
        {
            return null;
        }
        
        private void normalizeVertically()
        {
            for (int x=0; x < board.Length; x++)
            {
                for (int y=0; y < board[0].Length; y++)
                {
                    if (board[x][y] != -1)
                        continue;

                    int gapEnd = y+1;
                    while (gapEnd < board[0].Length && board[x][gapEnd]==-1)
                        gapEnd++;

                    if (gapEnd==board[0].Length)
                        break; // column checked
                    board[x][y] = board[x][gapEnd];
                    board[x][gapEnd] = -1;
                }
            }
        }
        
        private void normalizeHorizontaly()
        {
            for (int x=0; x < board.Length; x++)
            {
                if (board[x][0] != -1)
                    continue;

                int gapEnd = x+1;

                while (gapEnd < board.Length && board[gapEnd][0]==-1)
                    gapEnd++;

                if (gapEnd==board.Length)
                    return; // all columns checked

                for (int y=0; y < board[0].Length; y++)
                {
                    board[x][y] = board[gapEnd][y];
                    board[gapEnd][y] = -1;
                }
            }
        }
        
        private HashSet<Tuple<int, int>> computeBlock(int x, int y)
        {
            int color = board[x][y];
            HashSet<Tuple<int, int>> region = new HashSet<Tuple<int, int>>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>(); 
            Queue<Tuple<int, int>> open = new Queue<Tuple<int, int>>();

            open.Enqueue(new Tuple<int, int>(x,y));
            visited.Add(new Tuple<int, int>(x,y));

            while (open.Count != 0)
            {
                Tuple<int, int> xxyy = open.Dequeue();
                if (region.Contains(xxyy))
                    continue;

                region.Add(xxyy);

                Tuple<int, int> left = new Tuple<int, int>(xxyy.Item1 -1, xxyy.Item2);
                if (xxyy.Item1 >0 && board[left.Item1][left.Item2]==color && !visited.Contains(left))
                    open.Enqueue(left);
                visited.Add(left);

                Tuple<int, int> right = new Tuple<int, int>(xxyy.Item1+1,xxyy.Item2) ;
                if (xxyy.Item1 <board.Length-1 && board[right.Item1][right.Item2]==color && !visited.Contains(right))
                    open.Enqueue(right);
                visited.Add(right);

                Tuple<int, int> down = new Tuple<int,int>(xxyy.Item1, xxyy.Item2-1);
                if (xxyy.Item2 >0 && board[down.Item1][down.Item2]==color && !visited.Contains(down))
                    open.Enqueue(down);
                visited.Add(down);

                Tuple<int, int> up = new Tuple<int,int>(xxyy.Item1, xxyy.Item2+1);
                if (xxyy.Item2 <board[0].Length-1 && board[up.Item1][up.Item2]==color && !visited.Contains(up))
                    open.Enqueue(up);
                visited.Add(up);
            }

            return region;
        }
    }

    public class Block
    {
        public readonly Collection<Tuple<int, int>> points;
        public readonly int color;
        public readonly int size;
        
        public Block(Collection<Tuple<int, int>> points, int color, int size)
        {
            this.points = points;
            this.color = color;
            this.size = size;
        }
    }
}