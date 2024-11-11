using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public class InfStudent
    {
        public StudentClass student;
        public Lesson lesson;

        public InfStudent(StudentClass student, Lesson lesson)
        {
            this.student = student;
            this.lesson = lesson;
        }
    }
}
