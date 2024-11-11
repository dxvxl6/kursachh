using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public class Lesson
    {
        public int id_lesson {  get; set; }
        public int id_instructor;
        public string date { get; set; }
        public string time { get; set; }
        public int id_student;

        public Lesson(int id_lesson, int id_instructor, string date, string time, int id_student)
        {
            this.id_lesson = id_lesson;
            this.id_instructor = id_instructor;
            this.date = date;
            this.time = time;
            this.id_student = id_student;
        }
    }
}
