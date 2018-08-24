using Spring.Context;
using Spring.Context.Support;
using System;

namespace SpringNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //IStudentDal studentDal = new StudentDal();
            //studentDal.show();

            //初始化容器
            IApplicationContext ctx = ContextRegistry.GetContext();
            IStudentDal dal = ctx.GetObject("StudentDal") as IStudentDal;
            dal.show();
            Console.Read();
        }
    }
}
