using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.Common.Cache
{
    public class MemCacheWriter : ICacheWriter
    {
        public MemcachedClient memcachedClient { get; set; }
        public MemCacheWriter()
        {
            //string[] servers = { "192.168.1.109:11211" };
            string[] servers = System.Configuration.ConfigurationManager.AppSettings["MencacheServerList"].Split(',');
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient
            {
                EnableCompression = false
            };
            memcachedClient = mc; 
        }
        public void Addcache(string key, object value, DateTime exadate)
        {
            memcachedClient.Add(key, value, exadate);
        }

        public void Addcache(string key, object value)
        {
            memcachedClient.Add(key, value);
        }

        public object getCache(string key)
        {
          return  memcachedClient.Get(key);
        }

        public T getCache<T>(string key)
        {
            return (T)memcachedClient.Get(key);

        }

        public void SetCache(string key, object value, DateTime exadate)
        {
            memcachedClient.Set(key, value, exadate);
        }

        public void SetCache(string key, object value)
        {
            memcachedClient.Set(key, value);
        }
    }
}
