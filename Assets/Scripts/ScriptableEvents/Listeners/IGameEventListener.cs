namespace DapperDino.GGJ2020.ScriptableEvents.Listeners
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}
