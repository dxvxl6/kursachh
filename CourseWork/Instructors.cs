using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace CourseWork
{
    public partial class Instructors : Form
    {
        BD Connection;

        List<InfStudent> infStudent;
        public Instructors(FromAuthorization autho, BD Connection)
        {
            this.Connection = Connection;
            InitializeComponent();
            autho.Hide();
            label2.Text = Connection.instructor.surname + ' ' + Connection.instructor.name + ' ' + Connection.instructor.patronymic;
            label5.Text = Connection.instructor.experience;
            label6.Text = Connection.instructor.car;
            label23.Text = Connection.instructor.car_number;
            label21.Text = Connection.instructor.phone_number;

            comboBox3.DataSource = new List<string>
            {
                "11.11.2024", "12.11.2024", "13.11.2024", "14.11.2024", "15.11.2024", "16.11.2024", "17.11.2024", "18.11.2024", "19.11.2024", "20.11.2024"
            };

            //Connection.CreateLess()

            List<string> dt = new List<string>
            {
                "8:00", "9:30", "11:00", "13:00", "14:30", "16:00", "18:00", "19:30"
            };

            comboBox1.DataSource = dt;


            infStudent = Connection.GetStudentInfo();


            List<string> dateList = infStudent.Select(s => s.lesson.date).Distinct().ToList();

            comboBox4.DataSource = dateList;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDate = comboBox4.SelectedItem as string;

            List<string> timeList = infStudent.Where(s => s.lesson.date == selectedDate).Select(s => s.lesson.time).Distinct().ToList();

            comboBox2.DataSource = timeList;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTime = comboBox2.SelectedItem as string;
            string selectedDate = comboBox4.SelectedItem as string;
            InfStudent selectedStudent = infStudent.FirstOrDefault(s => s.lesson.date == selectedDate && s.lesson.time == selectedTime);

            label15.Text = selectedStudent.student.surname;
            label16.Text = selectedStudent.student.name;
            label17.Text = selectedStudent.student.patronymic;
            label18.Text = selectedStudent.student.patronymic;
            label19.Text = selectedStudent.student.name_group;
            label20.Text = selectedStudent.student.phone_number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedDate = comboBox3.SelectedItem as string;
            string selectedTime = comboBox1.SelectedItem as string;
            if(Connection.CreateLess(Connection.instructor.id_instructor, comboBox3.SelectedItem as string, comboBox1.SelectedItem as string))
            {
                MessageBox.Show("Запись создана", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Запись уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
