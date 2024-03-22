namespace dotnet_demo.Services
{
    public class CacheService<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _cachedData = new();

        public TValue Get(TKey key, Func<TKey, TValue> getDataFirst)
        {
            if (!_cachedData.ContainsKey(key))
            {
                _cachedData[key] = getDataFirst(key);
            }

            return _cachedData[key];
        }

    }

    public interface IDataGetter
    {
        string DataSleeper(string resource);
    }

    public class DataGetter : IDataGetter
    {
        private readonly CacheService<string, string> _cache = new();

        public string DataFaster(string resource)
        {
            return _cache.Get(resource, DataSleeper);
        }

        public string DataSleeper(string resource)
        {
            Thread.Sleep(100000);
            return resource;
        }
    }
}
