using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public class User
    {
        public int id_user;
        public string login;
        public string password;
        public string name;
        public string surname;
        public string patronymic;
        public string phone_number;
        public string date_birth;

        public User(int id_user, string login, string password, string name, string surname, string patronymic, string phone_number, string date_birth)
        {
            this.id_user = id_user;
            this.login = login;
            this.password = password;
            this.name = name;
            this.surname = surname;
            this.patronymic = patronymic;
            this.phone_number = phone_number;
            this.date_birth = date_birth;
        }
    }
}
