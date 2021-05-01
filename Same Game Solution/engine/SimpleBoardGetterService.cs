namespace Same_Game_Solution.engine
{
    public class SimpleBoardGetterService : IBoardGetterService
    {
        private int[][] board;
        public SimpleBoardGetterService(int[][] board)
        {
            this.board = board;
        }

        public int[][] getBoard()
        {
            return board;
        }
    }
}