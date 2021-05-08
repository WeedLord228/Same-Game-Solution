using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Same_Game_Solution.engine
{
    public class GameState
    {
        private int[][] board;
        private int score;

        public int Score => score;

        private bool terminal;

        public bool Terminal => terminal;

        public GameState(int[][] board, int score, bool terminal = false)
        {
            this.board = board;
            this.score = score;
            this.terminal = terminal;
        }

        public void deleteBlock(Block turn)
        {
            foreach (var point in turn.points)
            {
                board[point.Item1][point.Item2] = -1;
            }

            normalizeVertically();
            normalizeHorizontally();

            score = score + (turn.size - 2) * (turn.size - 2);

            if (legals().Count != 0)
            {
                return;
            }

            terminal = true;
            if (board[0][0] == -1)
            {
                score += 1000;
            }
        }
        
        public GameState copy()
        {
            int[][] newBoard = new int[board.Length][];
            
            for (int y = 0; y < board.Length; y++)
            {
                newBoard[y] = new int[board[0].Length];
                for (int x = 0; x < board[0].Length; x++)
                {
                    newBoard[y][x] = board[y][x];
                }        
            }
            
            return new GameState(newBoard, score, terminal);
        }

        public HashSet<Block> legals()
        {
            HashSet<Block> blocks = new HashSet<Block>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();

            for (int x = 0; x < board.Length; x++)
            {
                for (int y = 0; y < board[0].Length; y++)
                {
                    if (board[x][y] == -1 || visited.Contains(new Tuple<int, int>(x, y)))
                        continue;
                    Block block = computeBlock(x, y);
                    if (block.size < 2)
                        continue;
                    blocks.Add(block);

                    foreach (var point in block.points)
                    {
                        visited.Add(point);
                    }
                }
            }

            return blocks;
        }

        private void normalizeHorizontally()
        {
            for (int x = 0; x < board[0].Length; x++)
            {
                if (board[0][x] != -1)
                {
                    continue;
                }

                int gapEnd = x + 1;

                while (gapEnd < board[0].Length && board[0][gapEnd] == -1)
                {
                    gapEnd++;
                }

                if (gapEnd == board[0].Length)
                {
                    return;
                }

                for (int y = 0; y < board.Length; y++)
                {
                    board[y][x] = board[y][gapEnd];
                    board[y][gapEnd] = -1;
                }
            }
        }
        
        private void normalizeVertically()
        {
            for (int x = 0; x < board[0].Length; x++)
            {
                for (int y = 0; y < board.Length; y++)
                {
                    if (board[y][x] != -1)
                    {
                        continue;
                    }

                    var gapEnd = y + 1;
                    
                    while (gapEnd < board.Length && board[gapEnd][x] == -1)
                    {
                        gapEnd++;
                    }

                    if (gapEnd == board.Length)
                    {
                        break;
                    }
                    
                    board[y][x] = board[gapEnd][x];
                    board[gapEnd][x] = -1;
                }
            }
        }

        private Block computeBlock(int x, int y)
        {
            int color = board[x][y];
            HashSet<Tuple<int, int>> region = new HashSet<Tuple<int, int>>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();
            Queue<Tuple<int, int>> open = new Queue<Tuple<int, int>>();

            open.Enqueue(new Tuple<int, int>(x, y));
            visited.Add(new Tuple<int, int>(x, y));

            while (open.Count != 0)
            {
                Tuple<int, int> xxyy = open.Dequeue();
                if (region.Contains(xxyy))
                    continue;

                region.Add(xxyy);

                Tuple<int, int> up = new Tuple<int, int>(xxyy.Item1 - 1, xxyy.Item2);
                if (xxyy.Item1 > 0 && board[up.Item1][up.Item2] == color && !visited.Contains(up))
                    open.Enqueue(up);
                visited.Add(up);

                Tuple<int, int> down = new Tuple<int, int>(xxyy.Item1 + 1, xxyy.Item2);
                if (xxyy.Item1 < board.Length - 1 && board[down.Item1][down.Item2] == color && !visited.Contains(down))
                    open.Enqueue(down);
                visited.Add(down);

                Tuple<int, int> left = new Tuple<int, int>(xxyy.Item1, xxyy.Item2 - 1);
                if (xxyy.Item2 > 0 && board[left.Item1][left.Item2] == color && !visited.Contains(left))
                    open.Enqueue(left);
                visited.Add(left);

                Tuple<int, int> right = new Tuple<int, int>(xxyy.Item1, xxyy.Item2 + 1);
                if (xxyy.Item2 < board[0].Length - 1 && board[right.Item1][right.Item2] == color &&
                    !visited.Contains(right))
                    open.Enqueue(right);
                visited.Add(right);
            }

            return new Block(region, color);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var row in board.Reverse())
            {
                sb.AppendLine(String.Join(" ", row));
            }

            return sb.ToString();
        }
    }

    public class Block
    {
        public readonly ICollection<Tuple<int, int>> points;
        public readonly int color;
        public int size => points.Count;

        public Block(ICollection<Tuple<int, int>> points, int color)
        {
            this.points = points;
            this.color = color;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Tuple<int, int> point in points)
            {
                sb.Append(point.ToString() + "\t");
            }

            sb.Append(color);

            return sb.ToString();
        }
    }
}