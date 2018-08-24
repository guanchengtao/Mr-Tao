using SDAU.GCT.OA.IDal;
using SDAU.GCT.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.NHDal
{
    public class StudentDal : IStudentDal
    {
        public Student Add(OA.Model.Student t)
        {
            throw new NotImplementedException();
        }


        public bool Delete(OA.Model.Student t)
        {
            throw new NotImplementedException();
        }



        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public IQueryable<OA.Model.Student> GetEntities(System.Linq.Expressions.Expression<Func<OA.Model.Student, bool>> WhereLambda)
        {
            throw new NotImplementedException();
        }


        public IQueryable<OA.Model.Student> GetTInfoPage<S>(int pageSize, int pageIndex, out int total, System.Linq.Expressions.Expression<Func<OA.Model.Student, bool>> WhereLambda, System.Linq.Expressions.Expression<Func<OA.Model.Student, S>> OrderbyLambda, bool isAsc)
        {
            throw new NotImplementedException();
        }


        public bool Update(OA.Model.Student t)
        {
            throw new NotImplementedException();
        }


    }
}
