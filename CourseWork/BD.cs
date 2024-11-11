using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Drawing;

namespace CourseWork
{
    public class BD
    {
        string connectString = "Host=localhost;Username=postgres;Password=1234;Database=Professional";

        NpgsqlConnection connect;

        string query;

        public User user;

        public StudentClass student;

        public InstructorsClass instructor;

        public BD()
        {
            try
            {
                connect = new NpgsqlConnection(connectString);
                connect.Open();
                Console.WriteLine("Подключение к базе данных открыто");
            }
            catch (Exception ex)
            {
                Console.WriteLine("При подключение к базе данных ввозникла ошибка: {ex.Message}");
            }
            finally
            {
                Console.Read();
            }
        }

        public bool IsStudent(User user, int id_user)
        {
            query = "SELECT * FROM students WHERE id_user = @id";

            using (var cmd = new NpgsqlCommand(query, connect))
            {
                cmd.Parameters.AddWithValue("id", id_user);


                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Извлекаем данные из результата запроса и создаем объект User
                        int id_student = reader.GetInt32(reader.GetOrdinal("id_student"));
                        string category = reader.GetString(reader.GetOrdinal("category"));
                        string name_group = reader.GetString(reader.GetOrdinal("name_group"));

                        student = new StudentClass(user, id_student, category, name_group);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public List<InfStudent> GetStudentInfo() // More descriptive name
        {
            List<InfStudent> students = new List<InfStudent>();

            query = @"
                    SELECT * FROM lessons l
                    INNER JOIN students s ON l.id_student = s.id_student
                    INNER JOIN users u ON s.id_user = u.id_user;
                    ";
            using (var command = new NpgsqlCommand(query, connect))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_user = reader.GetInt32(reader.GetOrdinal("id_user"));
                        string login = reader.GetString(reader.GetOrdinal("login"));
                        string password = reader.GetString(reader.GetOrdinal("pass"));
                        string name = reader.GetString(reader.GetOrdinal("nam"));
                        string surname = reader.GetString(reader.GetOrdinal("surname"));
                        string patronymic = reader.GetString(reader.GetOrdinal("patronymic"));
                        string phone_number = reader.GetString(reader.GetOrdinal("phone_number"));
                        string date_birth = reader.GetString(reader.GetOrdinal("date_birth"));

                        int id_student = reader.GetInt32(reader.GetOrdinal("id_student"));
                        string category = reader.GetString(reader.GetOrdinal("category"));
                        string name_group = reader.GetString(reader.GetOrdinal("name_group"));

                        StudentClass student = new StudentClass(new User(id_user, login, password, name, surname, patronymic, phone_number, date_birth), id_student, category, name_group);

                        int id_lesson = reader.GetInt32(reader.GetOrdinal("id_lesson"));
                        int id_instructor = reader.GetInt32(reader.GetOrdinal("id_instructor"));
                        string date = reader.GetString(reader.GetOrdinal("date"));
                        string time = reader.GetString(reader.GetOrdinal("lesson_time"));


                        students.Add(new InfStudent(student, new Lesson(id_lesson, id_instructor, date, time, id_student)));
                    }
                }
            }
            return students;
        }


        public List<Lesson> AllLessonFree(int id_instructor)
        {
            List<Lesson> lessons = new List<Lesson>();

            query = "SELECT * FROM lessons WHERE id_instructor = @instructorId AND id_student IS NULL";
            using (var command = new NpgsqlCommand(query, connect))
            {
                command.Parameters.AddWithValue("@instructorId", id_instructor);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_lesson = reader.GetInt32(reader.GetOrdinal("id_lesson"));
                        int id_instruct = reader.GetInt32(reader.GetOrdinal("id_instructor"));
                        string date = reader.GetString(reader.GetOrdinal("date"));
                        string time = reader.GetString(reader.GetOrdinal("lesson_time"));
                        int id_student = 0;

                        Lesson lesson = new Lesson(id_lesson, id_instruct, date, time, id_student);

                        lessons.Add(lesson);
                    }
                    return lessons;
                }
            }
        }

        public bool UpdateLesson(int id_lesson, int id_student)
        {
            try
            {
                query = "UPDATE lessons SET id_student = @studentId WHERE id_lesson = @lessonId";

                using (var command = new NpgsqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@studentId", id_student);
                    command.Parameters.AddWithValue("@lessonId", id_lesson);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected == 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при изменении урока: {ex.Message}");
                return false;
            }
        }

        public bool IsInstructors(User user, int id_user)
        {
            query = "SELECT * FROM instructors WHERE id_user = @id";

            using (var cmd = new NpgsqlCommand(query, connect))
            {
                cmd.Parameters.AddWithValue("id", id_user);


                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Извлекаем данные из результата запроса и создаем объект User
                        int id_instructor = reader.GetInt32(reader.GetOrdinal("id_instructor"));
                        string experience = reader.GetString(reader.GetOrdinal("experience"));
                        string car = reader.GetString(reader.GetOrdinal("car"));
                        string car_number = reader.GetString(reader.GetOrdinal("car_number"));

                        instructor = new InstructorsClass(user, id_instructor, experience, car, car_number);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool CreateLess(int id_instructor, string date, string time)
        {
            try
            {
                query = "INSERT INTO lessons(id_instructor, date, lesson_time) VALUES (@id_instructor, @date, @time)";

                using (var cmd = new NpgsqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@id_instructor", id_instructor);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@time", time);

                    int answer = cmd.ExecuteNonQuery();
                    return answer > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении урока: {ex.Message}");
                return false;
            }

        }

        public List<InstructorsClass> AllInstructors()
        {
            List<InstructorsClass> instructorsList = new List<InstructorsClass>();

            query = "SELECT * FROM instructors i INNER JOIN users u ON i.id_user = u.id_user";

            using (var cmd = new NpgsqlCommand(query, connect))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Извлекаем данные из результата запроса и создаем объект User
                        int id_user = reader.GetInt32(reader.GetOrdinal("id_user"));
                        string loginDb = reader.GetString(reader.GetOrdinal("login"));
                        string passwordDb = reader.GetString(reader.GetOrdinal("pass"));
                        string name = reader.GetString(reader.GetOrdinal("nam"));
                        string surname = reader.GetString(reader.GetOrdinal("surname"));
                        string patronymic = reader.GetString(reader.GetOrdinal("patronymic"));
                        string phone_number = reader.GetString(reader.GetOrdinal("phone_number"));
                        string date_birth = reader.GetString(reader.GetOrdinal("date_birth"));
                        int id_instructor = reader.GetInt32(reader.GetOrdinal("id_instructor"));
                        string experience = reader.GetString(reader.GetOrdinal("experience"));
                        string car = reader.GetString(reader.GetOrdinal("car"));
                        string car_number = reader.GetString(reader.GetOrdinal("car_number"));

                        User userdop = new User(id_user, loginDb, passwordDb, name, surname, patronymic, phone_number, date_birth);
                        InstructorsClass instructordop = new InstructorsClass(userdop, id_instructor, experience, car, car_number);

                        instructorsList.Add(instructordop);
                    }
                    return instructorsList;
                }
            }
        }




        public int IsUserBD(string login, string password)
        {
            query = "SELECT * FROM users WHERE login = @login AND pass = @password";


            using (var cmd = new NpgsqlCommand(query, connect))
            {

                cmd.Parameters.AddWithValue("login", login.Trim());
                cmd.Parameters.AddWithValue("password", password.Trim());

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Извлекаем данные из результата запроса и создаем объект User
                            int id_user = reader.GetInt32(reader.GetOrdinal("id_user"));
                            string loginDb = reader.GetString(reader.GetOrdinal("login"));
                            string passwordDb = reader.GetString(reader.GetOrdinal("pass"));
                            string name = reader.GetString(reader.GetOrdinal("nam"));
                            string surname = reader.GetString(reader.GetOrdinal("surname"));
                            string patronymic = reader.GetString(reader.GetOrdinal("patronymic"));
                            string phone_number = reader.GetString(reader.GetOrdinal("phone_number"));
                            string date_birth = reader.GetString(reader.GetOrdinal("date_birth"));

                            user = new User(id_user, loginDb, passwordDb, name, surname, patronymic, phone_number, date_birth);
                            // Создаем и возвращаем объект User
                            //user = new User(id_user, loginDb, passwordDb, name, surname, patronymic, phone_number, date_birth);
                        }
                        else
                        {
                            // Если пользователь не найден, возвращаем null
                            return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Логируем ошибку и возвращаем null при возникновении исключения
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
            if (IsStudent(user, user.id_user))
            {
                return 1;
            }
            else if (IsInstructors(user, user.id_user))
            {
                return 2;
            }
            else { return 0; }
        }
    }
}
