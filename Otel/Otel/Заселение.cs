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
    public partial class zacelenie : Form
    {
        SqlConnection sqlConnection;

        public zacelenie()
        {
            InitializeComponent();
        }

        void AddGuest(string name, string fam, string patr, string docs)
        {

            int cnt_cols = -1;
            using (var comm = sqlConnection.CreateCommand())
            {
                string sql =
                        "INSERT INTO Guest " +
                        $"Values (@name,  @fam, @patr,  @docs)";
                comm.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = name;
                comm.Parameters.Add("@fam", SqlDbType.NVarChar, 50).Value = fam;
                comm.Parameters.Add("@patr", SqlDbType.NVarChar, 50).Value = patr;
                comm.Parameters.Add("@docs", SqlDbType.Text).Value = docs;
                comm.CommandText = sql;
                cnt_cols = comm.ExecuteNonQuery();
            }

            MessageBox.Show("Вставлено " + cnt_cols);
        }

        int GetGuestIdInBase(string name, string fam, string patr, string docs)
        {
            int guest_id = -1;
            using (var comm = sqlConnection.CreateCommand())
            {
                string sql = "SELECT * from  Guest"
                         + $" WHERE Name =@Nam AND Surname =@Fam ";
                //  +$"AND Patronymic =@Otch";

                comm.Parameters.Add("@Nam", SqlDbType.NVarChar, 50).Value = name;
                comm.Parameters.Add("@Fam", SqlDbType.NVarChar, 50).Value = fam;
                //comm.Parameters.Add("@Otch", SqlDbType.NVarChar, 50).Value = textBox1.Text;
                comm.CommandText = sql;
                using (var reader = comm.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        guest_id = (int)reader["Id"];
                    }
                    reader.Close();
                }
            }
            return guest_id;
        }

        bool IsGuestInBase(string name, string fam, string patr, string docs)
        {
            return GetGuestIdInBase(name, fam, patr, docs) != -1;
        }


        void InsertIntoAccommodation(int guest, int room, DateTime come, DateTime leave)
        {
            using (var comm = sqlConnection.CreateCommand())
            {
                string sql = "INSERT INTO accommodation " +
                     $"Values ( @guest, @room, @come, @leave)";

                comm.Parameters.Add("@guest", SqlDbType.Int).Value = guest;
                comm.Parameters.Add("@room", SqlDbType.Int).Value = room;
                comm.Parameters.Add("@come", SqlDbType.DateTime).Value = come;
                comm.Parameters.Add("@leave", SqlDbType.DateTime).Value = leave;

                comm.CommandText = sql;
                comm.ExecuteNonQuery();
            }
        }
        void UpdateRoom(int room)
        {
            using (var comm = sqlConnection.CreateCommand())
            {
                string sql = "UPDATE Appartaments " +
                     $"SET status= @state " +
                     "WHERE Id = @room";

                comm.Parameters.Add("@state", SqlDbType.NVarChar, 10).Value = "guest";
                comm.Parameters.Add("@room", SqlDbType.Int).Value = room;

                comm.CommandText = sql;
                comm.ExecuteNonQuery();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (room == -1)
            {
                MessageBox.Show("Не выбран номер");
                return;
            }

            string name = tbNaame.Text;
            string fam = tbFam.Text;
            string patr = tbOt.Text;
            string docs = textBox3.Text + " " + textBox8.Text + " "
                + textBox10.Text + " " + textBox9.Text;

            if (!IsGuestInBase(name, fam, patr, docs))
                AddGuest(name, fam, patr, docs);

            int guest = GetGuestIdInBase(name, fam, patr, docs);

            InsertIntoAccommodation(guest, room, DateTime.Now, leave);
            UpdateRoom(room);

        }

        private void Заселение_Load(object sender, EventArgs e)
        {
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


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void zacelenie_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            Form form1 = Application.OpenForms[0];
            form1.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Апартаменты апп = new Апартаменты(sqlConnection);
            апп.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Гости апп = new Гости(sqlConnection);
            апп.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Сведения_о_заселившихся апп = new Сведения_о_заселившихся(sqlConnection);
            апп.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            СвободныеНомера св = new СвободныеНомера(sqlConnection);

            if (св.ShowDialog() != DialogResult.OK)
                return;

            room = св.selectedRoom;
            leave = св.leave;

            label5.Text = room.ToString();
            dateTimePicker1.Value = leave;

        }
        int room = -1;
        DateTime leave = DateTime.Now;
    }
}
