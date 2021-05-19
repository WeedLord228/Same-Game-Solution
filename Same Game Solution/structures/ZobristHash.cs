using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Same_Game_Solution.structures
{
    public class ZobristHash
    {
        private readonly int _height;
        private readonly int _width;
        private readonly int[][] _table;
        private int hash;
        public ZobristHash(int height, int width, IEnumerable<int> types)
        {
            var rand = new Random(42);
            _height = height;
            _width = width;
            _table = new int[_height * _width][];
            for (int i = 0; i < _height * _width; i++)
            {
                _table[i] = new int[types.Count()];
                for (int j = 0; j < _table[i].Length; j++)
                {
                    _table[i][j] = rand.Next();
                }
            }
        }

        public int Update(int x, int y, int type)
        {
            return 0;
        }

        public int GetHashCode(int[][] board)
        {
            if (hash == 0)
            {
                var h = 0;
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board[i].Length; j++)
                    {
                        if (board[i][j] == -1) continue;

                        var val = board[i][j];
                        h ^= _table[i * j][val - 1];
                    }
                }

                hash = h;
                return h;
            }
            else{
                return hash;
            }
        }
    }
}