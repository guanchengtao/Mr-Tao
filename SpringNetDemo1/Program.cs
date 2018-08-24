using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            // IApplicationContext ctx = ContextRegistry.GetContext();
            //IStudentDal dal = ctx.GetObject("StudentDal") as IStudentDal;
            //   IStudentDal dal1 = ctx.GetObject("StudentDal1") as IStudentDal;
            //   dal.show();
            // dal1.show();
            //StudentService dal = ctx.GetObject("StudentService") as StudentService;
            //dal.Show();

            IApplicationContext applicationContext = ContextRegistry.GetContext();
            StudentService studentDal = (StudentService)applicationContext.GetObject("StudentService");
            studentDal.Show();
            Console.ReadKey();
                
        }
    }
}
