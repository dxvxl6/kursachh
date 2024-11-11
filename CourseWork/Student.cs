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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CourseWork
{
    public partial class Student : Form
    {
        BD Connection;
        List<Lesson> lessons;
        public Student(FromAuthorization autho, BD Connection)
        {
            this.Connection = Connection;
            autho.Hide();
            InitializeComponent();
            label8.Text = Connection.student.surname + ' ' + Connection.student.name + ' ' + Connection.student.patronymic;
            label14.Text = Connection.student.login;
            label16.Text = Connection.student.name_group;
            label18.Text = Connection.student.category;
            label9.Text = Connection.student.date_birth;
            label20.Text = Connection.student.phone_number;

            comboBox1.DataSource = Connection.AllInstructors();

            comboBox1.DisplayMember = "FullName";

            comboBox1.ValueMember = "id_instructor";


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstructorsClass selectedInstructor = (InstructorsClass)comboBox1.SelectedItem;

            if (selectedInstructor != null)
            {
                label5.Text = selectedInstructor.experience;
                label6.Text = selectedInstructor.car;
                label23.Text = selectedInstructor.car_number;
                label21.Text = selectedInstructor.phone_number;

                lessons = Connection.AllLessonFree(selectedInstructor.id_instructor);

                HashSet<string> uniqueDates = new HashSet<string>();
                List<Lesson> lesson_date = new List<Lesson>();
                foreach (Lesson less in lessons)
                {
                    if (uniqueDates.Add(less.date))
                    {
                        lesson_date.Add(less);
                    }
                }

                comboBox3.DataSource = lesson_date;

                comboBox3.DisplayMember = "date";

                comboBox3.ValueMember = "date";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lesson lesson = (Lesson)comboBox2.SelectedItem;
           
            if(Connection.UpdateLesson(lesson.id_lesson, Connection.student.id_student))
            {
                MessageBox.Show("Запись создана", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Запись уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lesson lesson = (Lesson)comboBox3.SelectedItem;

            List<Lesson> lesson_time = new List<Lesson>();
            foreach (Lesson less in lessons)
            {
                if(less.date == lesson.date)
                {
                    lesson_time.Add(less);
                }
            }

            comboBox2.DataSource = lesson_time;

            comboBox2.DisplayMember = "time";

            comboBox2.ValueMember = "time";
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
