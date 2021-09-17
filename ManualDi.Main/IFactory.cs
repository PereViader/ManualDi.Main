namespace ManualDi.Main
{
    public interface IFactory<T>
    {
        T Create();
    }
}
