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
                comm.CommandText = sql;
                comm.ExecuteNonQuery();

                sql = "SELECT * from  Guest"
                    + $" WHERE Name = {textBox7.Text} AND Surname = {textBox2.Text} AND patromic = {textBox1.Text}";


                comm.CommandText = sql;
                var reader = comm.ExecuteReader();
                int guest_id = -1;
                if (reader.HasRows)
                {
                    guest_id = reader.GetInt16(0);
                }
                else
                {
                    sql =
                        "INSERT INTO Guest" +
                        $"Values ( {textBox7.Text},  {textBox2.Text} {textBox1.Text},  { textBox3.Text + textBox8.Text + textBox10.Text + textBox9.Text })";

                    comm.CommandText = sql;
                    comm.ExecuteNonQuery();
                }


                sql = "SELECT * from  Guest"
        + $" WHERE Name = {textBox7.Text} AND Surname = {textBox2.Text} AND patromic = {textBox1.Text}";


                comm.CommandText = sql;
                reader = comm.ExecuteReader();
                guest_id = -1;
                if (reader.HasRows)
                {
                    guest_id = reader.GetInt16(0);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            Application.Exit();
        }
    }
}
