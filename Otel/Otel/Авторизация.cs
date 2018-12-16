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
using System.IO;


namespace Otel
{
    public partial class Авторизация : Form
    {
        SqlConnection sqlConnection;
        public Авторизация()
        {
            InitializeComponent();

            var curDir = Directory.GetCurrentDirectory();
            var projDir = Directory.GetParent(curDir).Parent.FullName;

            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = projDir + @"\NewDatabase.mdf",
                IntegratedSecurity = true
            };

            sqlConnection = new SqlConnection
            {
                ConnectionString = sb.ConnectionString
            };

            sqlConnection.Open();
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Ошибка подключения к БД. Приложение будет закрыто");
                Application.Exit();
            }
        }

        string GetPersonal(string login, string pass)
        {
            string position = "";
            using (var comm = sqlConnection.CreateCommand())
            {
                string sql = "SELECT position "
                    + "FROM Personal "
                    + "WHERE login = @log AND password = @pass";
                comm.Parameters.Add("@log", SqlDbType.NVarChar, 30).Value = login;
                comm.Parameters.Add("@pass", SqlDbType.NVarChar, 30).Value = pass;

                comm.CommandText = sql;

                using (var reader = comm.ExecuteReader())
                {
                    if (reader.Read())
                        position = (string)reader["position"];
                }
            }
            return position;
        }

        void GoNextForm()
        {
            var log = login.Text;
            var pass = password.Text;
            var pos = GetPersonal(log, pass);

            switch (pos)
            {
                case "":
                    MessageBox.Show("Неверный логин/пароль");
                    break;
                case "Администратор":
                    zacelenie zacelenieForm = new zacelenie(sqlConnection);
                    zacelenieForm.Show();
                    this.Hide();
                    break;
                case "Техперсонал":
                    Техперсонал Form = new Техперсонал(sqlConnection);
                    Form.Show();
                    this.Hide();
                    break;
                default:
                    MessageBox.Show("Незивестный тип персонала");
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GoNextForm();
        }

        private void Авторизация_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (sqlConnection != null)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            Application.Exit();
        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && password.Text != "")
                GoNextForm();
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && login.Text != "")
                GoNextForm();
        }
    }
}
