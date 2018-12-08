using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Otel
{
    public partial class Авторизация : Form
    {      
        public Авторизация()
        {
            InitializeComponent();
            login.Text = "Администратор";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (login.Text == "Администратор")
            {
                zacelenie zacelenieForm = new zacelenie();
                zacelenieForm.Show();
                this.Hide();
            }
            if (login.Text == "Техперсонал")
            {
                Техперсонал Form = new Техперсонал();
                Form.Show();
                this.Hide();
            }
           
        }

        private void Авторизация_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Application.Exit();
        }


    }
}
