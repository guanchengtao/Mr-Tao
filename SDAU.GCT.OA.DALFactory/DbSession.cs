using SDAU.GCT.OA.Dal;
using SDAU.GCT.OA.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.DALFactory
{
    //是整个数据库访问层的入口
   public partial class DbSession:IDbSession
    {
        #region 由TT模板生成
        //public IStudentDal StudentDal { get
        //    {
        //        return StaticDalFactory.getStudentDal();
        //    }
        //}
        //public ITeacherDal TeacherDal
        //{
        //    get
        //    {
        //        return StaticDalFactory.getTeacherDal();
        //    }
        //} 
        #endregion


            //是整个数据库访问层和数据库之间一次回话的代表
        public int Savechanges()
            {
            //GetCurrentContext返回一个上下文
            return DbContextFactory.GetCurrentContext().SaveChanges();
        }

    }
}
