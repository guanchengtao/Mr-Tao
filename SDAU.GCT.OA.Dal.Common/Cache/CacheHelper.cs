using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.Common.Cache
{
   public  class CacheHelper
    {
        public static ICacheWriter CacheWriter { get; set; }
        static CacheHelper()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            //ctx.GetObject("CacheHelper");
            CacheWriter = ctx.GetObject("CacheWriter") as ICacheWriter;
        }
        public static void AddCache(string key,object value ,DateTime exadate)
        {
            CacheWriter.Addcache(key, value, exadate);
        }
        public static void AddCache(string key, object value)
        {
            CacheWriter.Addcache(key, value);
        }
        public static object GetCache(string key)
        {
         return CacheWriter.getCache(key);
            
        }
        public static void SetCache(string key, object value,DateTime exadate)
        {
            CacheWriter.SetCache(key, value,exadate);
        }
        public static void SetCache(string key, object value)
        {
            CacheWriter.SetCache(key, value);
        }
    }
}
