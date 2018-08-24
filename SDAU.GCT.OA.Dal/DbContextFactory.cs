using SDAU.GCT.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.Dal
{
    public  class DbContextFactory
    {
        public static DbContext GetCurrentContext()
        {
            //一次请求公用一个实例，上下文也可可以随便切换
            // return new MVCTestEntities();
            if (!(CallContext.GetData("DbContext") is DbContext db))
            {
                db = new MVCTestEntities();
                CallContext.SetData("DbContext", db);
            }
            return db;
        }
    }
}
