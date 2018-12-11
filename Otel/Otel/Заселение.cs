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
    public partial class zacelenie : Form
    {
        SqlConnection sqlConnection;

        public zacelenie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            /*zacelenie zacelenieForm = new zacelenie();
            zacelenieForm.Show();
            this.Close();*/

            int id = comboBox1.SelectedIndex;
            if (id == -1)
            {
                MessageBox.Show("Не выбран номер");
                return;
            }


            int room = room_ids[id];

            try
            {
                string sql = "UPDATE Appartaments " +
                    "Set isFree = 0 , dateOut=null " +
                    "WHERE Id = " + id;

                var comm = sqlConnection.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = sql;
                comm.ExecuteNonQuery();

               // var sql2 = $"Select * from Guest WHERE Name =@Nam AND Surname =@Fam AND Patronymic =@Otch";/* +
               //     $"AND Patronymic =@Otch";*/
               // comm.CommandText = sql2;
               // comm.Parameters.Add("@Nam", SqlDbType.NVarChar, 50).Value = tbNaame.Text;
               // comm.Parameters.Add("@Fam", SqlDbType.NVarChar, 20).Value = tbFam.Text;
               ////comm.Parameters.Add("@Otch", SqlDbType.NVarChar, 50).Value = tbOt.Text;

               // var reader2 = comm.ExecuteReader();
               // List<int> ids = new List<int>();

               // while(reader2.Read())
               // {
               //     ids.Add((int)reader2["Id"]);
               // }
               // reader2.Close();
               // comm.Parameters.Clear();

                sql = "SELECT * from  Guest"
                    + $" WHERE Name =@Nam AND Surname =@Fam ";
                //  +$"AND Patronymic =@Otch";
               
                comm.Parameters.Add("@Nam", SqlDbType.NVarChar, 50).Value = tbNaame.Text;
                comm.Parameters.Add("@Fam", SqlDbType.NVarChar, 50).Value = tbFam.Text;
                //comm.Parameters.Add("@Otch", SqlDbType.NVarChar, 50).Value = textBox1.Text;
                int max_id = 0;
                comm.CommandText = sql;
                var reader = comm.ExecuteReader();
              
               // reader.Read();
                int guest_id = -1;
                //if (reader.FieldCount>0)
                //{
                //    guest_id = reader.GetInt32(0);
                //    reader.Close();
                //}
                if(reader.Read())
                {
                   // reader.Read();
                    guest_id = (int)reader["Id"];
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    //sql = " SELECT MAX(ID) FROM GUEST";
                    //comm.CommandText = sql;
                    //reader = comm.ExecuteReader();
                    //reader.Read();
                    // max_id = reader.GetInt32(0);
                    //reader.Close();
                    sql =
                        "INSERT INTO Guest " +
                        $"Values ('{tbNaame.Text}',  '{tbFam.Text}',' {tbOt.Text}',  '{ textBox3.Text + textBox8.Text + textBox10.Text + textBox9.Text }')";

                    comm.CommandText = sql;
                   int cnt_cols= comm.ExecuteNonQuery();
                    MessageBox.Show("Вставлено " + cnt_cols);

                    guest_id = max_id + 1;
                }


                //sql = " SELECT MAX(ID) FROM accommodation";
                //comm.CommandText = sql;
                //reader = comm.ExecuteReader();
                //reader.Read();
                //max_id = reader.GetInt32(0);
                //reader.Close();


                sql = "INSERT INTO accommodation " +
                     $"Values ( {guest_id}, {room}, \'{DateToString( DateTime.Now)}\'," +
                     $" \'{DateToString(DateTime.Now.AddDays(1))}\' )";
                comm.CommandText = sql;
                comm.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string DateToString(DateTime dt)
        {
            return
                $"{dt.Year}-{dt.Month}-{dt.Day} " +
                $"{dt.Hour}:{dt.Minute}:{dt.Second}";
        }

        private async void Заселение_Load(object sender, EventArgs e)
        {
            room_ids = new List<int>();
            room_size = new List<int>();


            //тут надо будет менять путь к файлу на твой
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;"
+ @"AttachDbFilename=C:\Users\Михаил\Desktop\ПЧМИ\Pchmi\Otel\Otel\NewDatabase.mdf;" +
@"Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlDataReader = null;
            //SqlCommand sqlComm = new SqlCommand("SELECT * FROM [Worker]", sqlConnection);


            if (sqlDataReader != null)
                sqlDataReader.Close();
            sqlDataReader = null;
        }

        List<int> room_ids;
        List<int> room_size;




        private void button2_Click(object sender, EventArgs e)
        {

            DateTime start, finish;
            int size;
            try
            {
                start = DateTime.Parse(textBox4.Text);
                finish = DateTime.Parse(textBox5.Text);
                size = int.Parse(textBox11.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            var comm = sqlConnection.CreateCommand();
            string SQLcode = "SELECT Appartaments.Id, size " +
                "FROM Appartaments " +
                "WHERE Appartaments.size >=" + size +
               " AND Appartaments.isFree = 1";

            comm.CommandText = SQLcode;

            var reader = comm.ExecuteReader();
            room_ids = new List<int>();
            room_size = new List<int>();
            comboBox1.Items.Clear();
            while (reader.Read())
            {
                room_ids.Add(reader.GetInt32(0));
                room_size.Add(reader.GetInt32(1));

            }

            textBox6.Text = "";

            for (int i = 0; i < room_ids.Count; i++)
            {
                textBox6.Text += room_ids[i] + " ";
                comboBox1.Items.Add(room_ids[i]);
            }
            reader.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form form1 = Application.OpenForms[0];
            form1.Show();
            this.Close();
        }

        private void zacelenie_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form form1 = Application.OpenForms[0];
            if(!form1.Visible)
            form1.Show();
            // Application.Exit();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

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
    }
}
