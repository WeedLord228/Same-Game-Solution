namespace Same_Game_Solution.engine
{
    public interface IVisualizer<T>
    {
        public void render(T toRender);
    }
}