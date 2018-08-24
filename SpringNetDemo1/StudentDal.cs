using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo1
{
    class StudentDal : IStudentDal
    {

        public StudentDal(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public void show()
        {
            Console.WriteLine("ok"+Name);
        }
    }
}
