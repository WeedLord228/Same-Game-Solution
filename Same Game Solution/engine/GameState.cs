using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Same_Game_Solution.engine
{
    public class GameState
    {
        private readonly int[][] _board;

        public GameState(int[][] board, int score, bool terminal = false)
        {
            _board = board;
            Score = score;
            Terminal = terminal;
        }

        public int Score { get; private set; }

        public bool Terminal { get; private set; }

        public void deleteBlock(Block turn)
        {
            foreach (var (y, x) in turn.Points) _board[y][x] = -1;

            normalizeVertically();
            normalizeHorizontally();

            Score += (turn.Size - 2) * (turn.Size - 2);

            if (legals().Count != 0) return;

            Terminal = true;
            if (_board[0][0] == -1) Score += _board.Length * _board[0].Length;
        }

        public GameState copy()
        {
            var newBoard = new int[_board.Length][];

            for (var y = 0; y < _board.Length; y++)
            {
                newBoard[y] = new int[_board[0].Length];
                for (var x = 0; x < _board[0].Length; x++) newBoard[y][x] = _board[y][x];
            }

            return new GameState(newBoard, Score, Terminal);
        }

        public HashSet<Block> legals()
        {
            var blocks = new HashSet<Block>();
            var visited = new HashSet<Tuple<int, int>>();

            for (var x = 0; x < _board.Length; x++)
            for (var y = 0; y < _board[0].Length; y++)
            {
                if (_board[x][y] == -1 || visited.Contains(new Tuple<int, int>(x, y)))
                    continue;
                var block = computeBlock(x, y);
                if (block.Size < 2)
                    continue;
                blocks.Add(block);

                foreach (var point in block.Points) visited.Add(point);
            }

            return blocks;
        }

        private void normalizeHorizontally()
        {
            for (var x = 0; x < _board[0].Length; x++)
            {
                if (_board[0][x] != -1) continue;

                var gapEnd = x + 1;

                while (gapEnd < _board[0].Length && _board[0][gapEnd] == -1) gapEnd++;

                if (gapEnd == _board[0].Length) return;

                for (var y = 0; y < _board.Length; y++)
                {
                    _board[y][x] = _board[y][gapEnd];
                    _board[y][gapEnd] = -1;
                }
            }
        }

        private void normalizeVertically()
        {
            for (var x = 0; x < _board[0].Length; x++)
            for (var y = 0; y < _board.Length; y++)
            {
                if (_board[y][x] != -1) continue;

                var gapEnd = y + 1;

                while (gapEnd < _board.Length && _board[gapEnd][x] == -1) gapEnd++;

                if (gapEnd == _board.Length) break;

                _board[y][x] = _board[gapEnd][x];
                _board[gapEnd][x] = -1;
            }
        }

        private Block computeBlock(int x, int y)
        {
            var color = _board[x][y];
            var region = new HashSet<Tuple<int, int>>();
            var visited = new HashSet<Tuple<int, int>>();
            var open = new Queue<Tuple<int, int>>();

            open.Enqueue(new Tuple<int, int>(x, y));
            visited.Add(new Tuple<int, int>(x, y));

            while (open.Count != 0)
            {
                var xxyy = open.Dequeue();
                if (region.Contains(xxyy))
                    continue;

                region.Add(xxyy);

                var up = new Tuple<int, int>(xxyy.Item1 - 1, xxyy.Item2);
                if (xxyy.Item1 > 0 && _board[up.Item1][up.Item2] == color && !visited.Contains(up))
                    open.Enqueue(up);
                visited.Add(up);

                var down = new Tuple<int, int>(xxyy.Item1 + 1, xxyy.Item2);
                if (xxyy.Item1 < _board.Length - 1 && _board[down.Item1][down.Item2] == color &&
                    !visited.Contains(down))
                    open.Enqueue(down);
                visited.Add(down);

                var left = new Tuple<int, int>(xxyy.Item1, xxyy.Item2 - 1);
                if (xxyy.Item2 > 0 && _board[left.Item1][left.Item2] == color && !visited.Contains(left))
                    open.Enqueue(left);
                visited.Add(left);

                var right = new Tuple<int, int>(xxyy.Item1, xxyy.Item2 + 1);
                if (xxyy.Item2 < _board[0].Length - 1 && _board[right.Item1][right.Item2] == color &&
                    !visited.Contains(right))
                    open.Enqueue(right);
                visited.Add(right);
            }

            return new Block(region, color);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var row in _board.Reverse()) sb.AppendLine(string.Join(" ", row));

            return sb.ToString();
        }
    }

    public class Block
    {
        private readonly int _color;
        public readonly ICollection<Tuple<int, int>> Points;

        public Block(ICollection<Tuple<int, int>> points, int color)
        {
            Points = points;
            _color = color;
        }

        public int Size => Points.Count;

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var point in Points) sb.Append(point + "\t");

            sb.Append(_color);

            return sb.ToString();
        }
    }
}