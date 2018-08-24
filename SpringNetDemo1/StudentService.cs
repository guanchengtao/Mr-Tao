using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo1
{
   public class StudentService
    {
        public IStudentDal StudentDal { get; set; }
        public void Show()
        {
            StudentDal.show();
            Console.WriteLine("StudentService");
        }
    }
}
