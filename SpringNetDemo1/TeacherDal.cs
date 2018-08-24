using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo1
{
    class TeacherDal : IStudentDal
    {
        public string Name { get; set; }

        public void show()
        {
            Console.WriteLine("This is "+Name);
        }
    }
}
