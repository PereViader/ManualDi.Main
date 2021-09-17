namespace ManualDi.Main
{
    public interface ITypeMetadata
    {
        bool Has(object key);
        T Get<T>(object key);
        bool TryGet<T>(object key, out T value);
        T GetOrDefault<T>(object key, T defaultValue);
        T GetOrDefault<T>(object key);
        void Set<T>(object key, T value);
    }
}
