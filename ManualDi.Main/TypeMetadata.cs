using System.Collections.Generic;

namespace ManualDi.Main
{
    public class TypeMetadata : ITypeMetadata
    {
        public Dictionary<object, object> keyValuePairs = new Dictionary<object, object>();

        public T Get<T>(object key)
        {
            return (T)keyValuePairs[key];
        }

        public T GetOrDefault<T>(object key, T defaultValue)
        {
            if (!TryGet<T>(key, out var value))
            {
                return defaultValue;
            }

            return value;
        }

        public T GetOrDefault<T>(object key)
        {
            return GetOrDefault<T>(key, default);
        }

        public bool Has(object key)
        {
            return keyValuePairs.ContainsKey(key);
        }

        public void Set<T>(object key, T value)
        {
            keyValuePairs[key] = value;
        }

        public bool TryGet<T>(object key, out T value)
        {
            var contains = keyValuePairs.TryGetValue(key, out var objValue);
            value = (T)objValue;
            return contains;
        }
    }
}
