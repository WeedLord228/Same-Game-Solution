using System;

namespace Same_Game_Solution.engine
{
    class GameStateSimpleVisualizer : IVisualizer<GameState>
    {
        public GameStateSimpleVisualizer()
        {
        }
        
        public void render(GameState gameState)
        {
            Console.WriteLine(gameState.ToString());    
        }
    }
}