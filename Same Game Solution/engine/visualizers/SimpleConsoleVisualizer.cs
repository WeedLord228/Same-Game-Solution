using System;

namespace Same_Game_Solution.engine
{
    class SimpleConsoleVisualizer : IVisualizer
    {
        public SimpleConsoleVisualizer()
        {
        }
        
        public void render(GameState gameState)
        {
            Console.WriteLine(gameState.ToString());    
        }
    }
}