using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook
{
    public static class CacheHelper
    {
        public static void SetCache(string key,object obj){
            CacheItemPolicy policy = new CacheItemPolicy();
            ObjectCache oCache = MemoryCache.Default;
            oCache.Set(key,obj,policy);
        }

        public static object GetCache(string key)
        {
            ObjectCache oCache = MemoryCache.Default;
            return oCache.Get(key);
        }
    }
}
