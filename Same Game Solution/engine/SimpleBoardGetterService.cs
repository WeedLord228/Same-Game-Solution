namespace Same_Game_Solution.engine
{
    public class SimpleBoardGetterService : IBoardGetterService
    {
        private readonly int[][] _board;

        public SimpleBoardGetterService(int[][] board)
        {
            _board = board;
        }

        public int[][] getBoard()
        {
            return _board;
        }
    }
}