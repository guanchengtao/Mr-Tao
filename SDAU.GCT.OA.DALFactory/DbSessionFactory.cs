using SDAU.GCT.OA.IDal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.DALFactory
{
  public  class DbSessionFactory
    {
        public static IDbSession GetCurrentSession()
        {
            // return new MVCTestEntities();
            if (!(CallContext.GetData("DbSession") is IDbSession db))
            {
                db = new DbSession();
                CallContext.SetData("DbSession", db);
            }
            return db;
        }
    }
}
