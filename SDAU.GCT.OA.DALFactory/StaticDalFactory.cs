using SDAU.GCT.OA.Dal;
using SDAU.GCT.OA.IDal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.DALFactory
{
    /// <summary>
    /// 由简单工厂转换为抽象工厂
    /// </summary>
    public partial class StaticDalFactory
    {
        //通过改配置文件来导致创建实例的变化
       // public static string assemblyname = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
        //public static IStudentDal getStudentDal()
        //{      
        //return Assembly.Load(assemblyname).CreateInstance(assemblyname+".StudentDal") as IStudentDal;
        //}
        //public static ITeacherDal getTeacherDal()
        //{
        //    return Assembly.Load(assemblyname).CreateInstance(assemblyname + ".TeacherDal") as ITeacherDal;
        //}
    }
}
