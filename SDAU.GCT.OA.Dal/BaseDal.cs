using SDAU.GCT.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.Dal
{
   public  class BaseDal<T> where T:class ,new ()//必须是类，
    {
       public DbContext Context { get {
                return DbContextFactory.GetCurrentContext();
            } }
            #region rud
        public T Add(T t)
        {
            Context.Set<T>().Add(t);
          //  Context.SaveChanges();
            return t;
        }
        public bool Delete(T entity)
        {
            //Context.Entry(t).State = EntityState.Deleted;
            // return Context.SaveChanges() > 0;
            Context.Entry(entity).Property("DelFlag").CurrentValue = (short)Model.Enum.DelFlagEnum.Deleted;
            Context.Entry(entity).Property("DelFlag").IsModified = true;
            return true;
        }
        public bool Delete(int id)
        {
            var entity = Context.Set<T>().Find(id);
            //Context.Set<T>().Remove(entity);
            Context.Entry(entity).Property("DelFlag").CurrentValue = (short)Model.Enum.DelFlagEnum.Deleted;
            Context.Entry(entity).Property("DelFlag").IsModified = true;
            return true;
        }
        public int DeleteListByLogicl(List<int> ids)
        {
            foreach (var id in ids)
            {
               var entity= Context.Set<T>().Find(id);
                Context.Entry(entity).Property("DelFlag").CurrentValue = (short)Model.Enum.DelFlagEnum.Deleted;           
                Context.Entry(entity).Property("DelFlag").IsModified = true; 
            }
            return ids.Count;

        }
        public bool Update(T t)
        {
            Context.Entry(t).State = EntityState.Modified;
            return true;
            //  return Context.SaveChanges() > 0;
        }

        #endregion
        #region 查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> WhereLambda)
        {
            return Context.Set<T>().Where(WhereLambda).AsQueryable();
        }
        public IQueryable<T> GetTInfoPage<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> WhereLambda, Expression<Func<T, S>> OrderbyLambda, bool isAsc)
        {
            total = Context.Set<T>().Where(WhereLambda).Count();
            if (isAsc)
            {
                var temp = Context.Set<T>().Where(WhereLambda)
             .OrderBy<T, S>(OrderbyLambda)
             .Skip(pageSize * (pageIndex - 1))
             .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = Context.Set<T>().Where(WhereLambda)
               .OrderByDescending<T, S>(OrderbyLambda)
               .Skip(pageSize * (pageIndex - 1))
               .Take(pageSize).AsQueryable();
                return temp;
            }

        }
        #endregion

    }
}
