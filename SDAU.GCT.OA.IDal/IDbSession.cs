using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.IDal
{
    public partial interface IDbSession
    {
        //IStudentDal StudentDal { get; }
        //ITeacherDal TeacherDal { get; }
        int Savechanges();
    }
}
