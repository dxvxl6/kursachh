using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class FromAuthorization : Form
    {
        BD Connection = new BD();
        public FromAuthorization()
        {
            InitializeComponent();
        }

        private void entrance_Click(object sender, EventArgs e)
        {
            int res = Connection.IsUserBD(login.Text, pass.Text);
            if (res == 1)
            {
                Student form = new Student(this, Connection);
                form.ShowDialog();
            }
            else if (res == 2)
            {
                Instructors form = new Instructors(this, Connection);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пользоватяля с такими данными не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //открытие 2 фрэйма
        }
    }
}
