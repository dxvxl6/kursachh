using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public class StudentClass : User
    {
        public int id_student;
        public string category;
        public string name_group;

        public StudentClass(User user, int id_student, string category, string nameGroup)
         : base(user.id_user, user.login, user.password, user.name, user.surname, user.patronymic, user.phone_number, user.date_birth)
        {
            this.id_student = id_student;
            this.category = category;
            this.name_group = nameGroup;
        }
    }
}
