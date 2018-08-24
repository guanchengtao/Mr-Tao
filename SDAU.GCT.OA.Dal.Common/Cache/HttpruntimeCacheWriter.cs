using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SDAU.GCT.OA.Common.Cache
{
    public class HttpruntimeCacheWriter : ICacheWriter
    {
        public void Addcache(string key, object value, DateTime exadate)
        {
            HttpRuntime.Cache.Insert(key, value, null, exadate, TimeSpan.Zero);
        }

        public void Addcache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value);
        }

        public object getCache(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        public T getCache<T>(string key)
        {
            return (T)HttpRuntime.Cache.Get(key);
        }

        public void SetCache(string key, object value, DateTime exadate)
        {
            HttpRuntime.Cache.Remove(key);
            Addcache(key,value,exadate);
        }

        public void SetCache(string key, object value)
        {
            HttpRuntime.Cache.Remove(key);
            Addcache(key,value);
        }
    }
}
