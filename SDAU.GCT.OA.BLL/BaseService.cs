using SDAU.GCT.OA.DALFactory;
using SDAU.GCT.OA.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.BLL
{
    public abstract class BaseService<T> where T : class, new()

    {
        /// <summary>
        /// 父类要逼迫子类给父类的一个属性赋值
        /// 而且是在调用父类方法之前赋值
        /// </summary>
        public IBaseDal<T> CurrentDal { get; set; }
        //public BaseService(IDbSession dbSession)
        //{
        //    this.DbSession = dbSession;
        //    GetCurrentDal();
        //}

        public IDbSession DbSession
        {
            get;set;
            //{
            //    return DbSessionFactory.GetCurrentSession();
            //}
        }
        public abstract void GetCurrentDal();
        public T Add(T t)
        {
             CurrentDal.Add(t);
            DbSession.Savechanges();
            return t;
        }

        public int deleteList(List<int> ids)
        {
            foreach (var id in ids)
            {
                CurrentDal.Delete(id);
            }
            return DbSession.Savechanges();
        }
        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            return DbSession.Savechanges() > 0;
        }
        public bool Delete(T t)
        {
             CurrentDal.Delete(t);
            return DbSession.Savechanges() > 0;
        }
        //对数据进行逻辑删除
        public int DeleteListByLogicl(List<int> ids)
        {
            CurrentDal.DeleteListByLogicl(ids);
            return DbSession.Savechanges();

        }
        public  bool Update(T t)
        {
             CurrentDal.Update(t);
            return DbSession.Savechanges() > 0;
        }
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> WhereLambda)
        {
            return CurrentDal.GetEntities(WhereLambda);
        }
        public IQueryable<T> GetTInfoPage<S>(int pageSize, int pageIndex, out int total,
           Expression<Func<T, bool>> WhereLambda, Expression<Func<T, S>> OrderbyLambda, bool isAsc)
        {
            return CurrentDal.GetTInfoPage(pageSize, pageIndex, out total, WhereLambda, OrderbyLambda, isAsc);
        }
    }
}
