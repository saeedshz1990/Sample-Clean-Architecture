using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.MemoryCash
{
    public interface IFlushableMemoryCache
    {
        void Set<T>(string cacheId, object key, T value);
        bool TryGetValue<T>(object key, out T value);
        void Remove(string cacheId, object key);
        void Flush(string cacheId);
        void Reset();
    }


    public class FlushableMemoryCache : IFlushableMemoryCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDictionary<string, HashSet<object>> _keyDictionary;
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private TimeSpan typeExpiration;

        public FlushableMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _keyDictionary = new Dictionary<string, HashSet<object>>();
        }


        public void Set<T>(string cacheId, object key, T value)
        {
            typeExpiration = TimeSpan.Parse(value.ToString());
            var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal).SetAbsoluteExpiration(typeExpiration);
            options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
            _memoryCache.Set(cacheId, key, options);

            if (_keyDictionary.ContainsKey(cacheId))
            {
                if (!_keyDictionary[cacheId].Contains(key))
                {
                    _keyDictionary[cacheId].Add(key);
                }
            }
            else
            {
                _keyDictionary.Add(cacheId, new HashSet<object>(new[] { key }));
            }
        }

        public bool TryGetValue<T>(object key, out T value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public void Remove(string cacheId, object key)
        {
            _memoryCache.Remove(key);

            if (_keyDictionary.ContainsKey(cacheId) && _keyDictionary[cacheId].Contains(key))
            {
                _keyDictionary[cacheId].Remove(key);
            }
        }

        public void Flush(string cacheId)
        {
            foreach (var key in _keyDictionary[cacheId])
            {
                _memoryCache.Remove(key);
            }

            _keyDictionary[cacheId] = new HashSet<object>();
        }

        public void Reset()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
        }
    }
}
