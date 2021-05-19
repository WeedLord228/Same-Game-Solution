using System;

namespace Same_Game_Solution.engine
{
    public class RandomBoardGetterService : IBoardGetterService
    {
        private readonly int _height;
        private readonly int _width;
        private readonly int _maxType;
        private readonly int _seed;
        
        public RandomBoardGetterService(int height, int width, int maxType, int seed)
        {
            _height = height;
            _width = width;
            _maxType = maxType;
            _seed = seed;
        }

        private int[][] GenerateBoard()
        {
            var random = new Random(_seed);

            var result = new int[_height][]; 
            for (int i = 0; i < _height; i++)
            {
                result[i] = new int[_width];
                for (int j = 0; j < _width; j++)
                {
                    result[i][j] = random.Next(1,_maxType); 
                }
            }
            
            return result;
        }
        
        public int[][] getBoard()
        {
            return GenerateBoard();
        }
    }
}