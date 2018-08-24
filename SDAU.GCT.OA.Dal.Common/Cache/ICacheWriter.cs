using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.Common.Cache
{
    public interface ICacheWriter
    {
        void Addcache(string key, object value, DateTime exadate);
        void Addcache(string key, object value);
        object getCache(string key);
        T getCache<T>(string key);
        void SetCache(string key, object value, DateTime exadate);
        void SetCache(string key, object value);

    }
}
