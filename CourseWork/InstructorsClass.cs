using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public class InstructorsClass:User
    {
        public int id_instructor { get; set; }
        public string experience;
        public string car;
        public string car_number;


        public InstructorsClass(User user, int id_instructor, string experience, string car, string car_number)
         : base(user.id_user, user.login, user.password, user.name, user.surname, user.patronymic, user.phone_number, user.date_birth)
        {
            this.id_instructor = id_instructor;
            this.experience = experience;
            this.car = car;
            this.car_number = car_number;
        }

        public string FullName
        {
            get { return $"{surname} {name[0]}. {patronymic[0]}."; }
        }
    }
}
