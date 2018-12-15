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
            //dtCome.Value = DateTime.Now;
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
                comm.Parameters.Add("@docs", SqlDbType.Text).Value = docs;// textBox3.Text + textBox8.Text + textBox10.Text + textBox9.Text;
                comm.CommandText = sql;
                cnt_cols = comm.ExecuteNonQuery();
            }
            //var ;



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

        private void button1_Click(object sender, EventArgs e)
        {
            //var comm = sqlConnection.CreateCommand();
            //SqlDataReader reader = null;

            ///*zacelenie zacelenieForm = new zacelenie();
            //zacelenieForm.Show();
            //this.Close();*/
            //DateTime com, leave;
            //int id = comboBox1.SelectedIndex;
            //if (id == -1)
            //{
            //    MessageBox.Show("Не выбран номер");
            //    return;
            //}
            ////  if(DateTime.TryParse(text))
            //com = dtCome.Value;
            //leave = dtLeave.Value;

            //int room = room_ids[id];

            //try
            //{
            //    string sql = "UPDATE Appartaments " +
            //        "Set isFree = 0 , dateOut=null " +
            //        "WHERE Id = " + id;


            //    comm.CommandType = CommandType.Text;
            //    comm.CommandText = sql;
            //    comm.ExecuteNonQuery();


            //    comm.Parameters.Clear();

            //    sql = "SELECT * from  Guest"
            //        + $" WHERE Name =@Nam AND Surname =@Fam ";
            //    //  +$"AND Patronymic =@Otch";

            //    comm.Parameters.Add("@Nam", SqlDbType.NVarChar, 50).Value = tbNaame.Text;
            //    comm.Parameters.Add("@Fam", SqlDbType.NVarChar, 50).Value = tbFam.Text;
            //    //comm.Parameters.Add("@Otch", SqlDbType.NVarChar, 50).Value = textBox1.Text;
            //    int max_id = 0;
            //    comm.CommandText = sql;
            //    reader = comm.ExecuteReader();
            //    int guest_id = -1;
            //    if (reader.Read())
            //    {
            //        guest_id = (int)reader["Id"];
            //        reader.Close();
            //    }
            //    else
            //    {
            //        reader.Close();
            //        comm.Parameters.Clear();

            //        sql =
            //            "INSERT INTO Guest " +
            //            $"Values (@name,  @fam, @patr,  @docs)";

            //        comm.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = tbNaame.Text;
            //        comm.Parameters.Add("@fam", SqlDbType.NVarChar, 50).Value = tbFam.Text;
            //        comm.Parameters.Add("@patr", SqlDbType.NVarChar, 50).Value = tbOt.Text;
            //        comm.Parameters.Add("@docs", SqlDbType.NVarChar, 50).Value = textBox3.Text + textBox8.Text + textBox10.Text + textBox9.Text;


            //        comm.CommandText = sql;
            //        int cnt_cols = comm.ExecuteNonQuery();
            //        MessageBox.Show("Вставлено " + cnt_cols);

            //        guest_id = max_id + 1;
            //    }

            //    comm.Parameters.Clear();

            //    sql = "INSERT INTO accommodation " +
            //         $"Values ( @guest, @room, @come, @leave)";

            //    comm.Parameters.Add("@guest", SqlDbType.Int).Value = guest_id;
            //    comm.Parameters.Add("@room", SqlDbType.Int).Value = room;
            //    comm.Parameters.Add("@come", SqlDbType.DateTime).Value = com;
            //    comm.Parameters.Add("@leave", SqlDbType.DateTime).Value = leave;

            //    comm.CommandText = sql;
            //    comm.ExecuteNonQuery();


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    if (reader != null && !reader.IsClosed)
            //        reader.Close();
            //}

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

        }

        private async void Заселение_Load(object sender, EventArgs e)
        {
            // room_ids = new List<int>();
            // room_size = new List<int>();


            //тут надо будет менять путь к файлу на твой
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;"
+ @"AttachDbFilename=C:\Users\Михаил\Desktop\ПЧМИ\Pchmi\Otel\Otel\NewDatabase.mdf;" +
@"Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            //SqlDataReader sqlDataReader = null;

            //if (sqlDataReader != null)
            //    sqlDataReader.Close();
            //sqlDataReader = null;
        }

        // List<int> room_ids;
        //List<int> room_size;


        //  private void button2_Click(object sender, EventArgs e)
        // {

        //DateTime start, finish;
        //int size;
        //try
        //{
        //    start = dtCome.Value;
        //    finish = dtLeave.Value;
        //    size = int.Parse(textBox11.Text);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message);
        //    return;
        //}

        //var comm = sqlConnection.CreateCommand();
        //string SQLcode = "SELECT Appartaments.Id, size " +
        //    "FROM Appartaments " +
        //    "WHERE Appartaments.size >= @size AND Appartaments.isFree = 1";

        //comm.CommandText = SQLcode;
        //comm.Parameters.Add("@size", SqlDbType.Int).Value = size;
        //var reader = comm.ExecuteReader();
        //room_ids = new List<int>();
        //room_size = new List<int>();
        //comboBox1.Items.Clear();
        //while (reader.Read())
        //{
        //    room_ids.Add(reader.GetInt32(0));
        //    room_size.Add(reader.GetInt32(1));
        //}

        //textBox6.Text = "";

        //for (int i = 0; i < room_ids.Count; i++)
        //{
        //    textBox6.Text += room_ids[i] + " ";
        //    comboBox1.Items.Add(room_ids[i]);
        //}
        //reader.Close();

        //}

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Form form1 = Application.OpenForms[0];
            // form1.Show();
            this.Close();
        }

        private void zacelenie_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form form1 = Application.OpenForms[0];
            // if (!form1.Visible)
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
