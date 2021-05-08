namespace Same_Game_Solution.engine.visualizers
{
    public interface IVisualizer<T>
    {
        public void render(T toRender);
    }
}