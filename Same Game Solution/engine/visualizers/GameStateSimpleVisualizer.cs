using System;

namespace Same_Game_Solution.engine.visualizers
{
    internal class GameStateSimpleVisualizer : IVisualizer<GameState>
    {
        public void render(GameState gameState)
        {
            Console.WriteLine(gameState.ToString());
        }
    }
}