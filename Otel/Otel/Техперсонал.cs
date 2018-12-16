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
    public partial class Техперсонал : Form
    {
        SqlConnection sqlConnection;
        public Техперсонал(SqlConnection connection)
        {
            sqlConnection = connection;

            InitializeComponent();

            CheckNumbers();
        }


        void CheckNumbers()
        {
            using (var comm = sqlConnection.CreateCommand())
            {
                string sql = "UpdateRooms";
                comm.CommandText = sql;
                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
               // comm.sql = 
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void Техперсонал_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form form1 = Application.OpenForms[0];
            form1.Show();
        }

        void GetRooms()
        {
            comboBox1.Items.Clear();
            string sql = "SELECT * FROM [Appartaments] WHERE status = @state";
            using (var comm = sqlConnection.CreateCommand())
            {
                comm.CommandText = sql;
                comm.Parameters.Add("@state", SqlDbType.NVarChar, 10).Value = "dirty";

                using (var reader = comm.ExecuteReader())
                {
                    //dataGridView1.ColumnCount = reader.FieldCount;
                    dataGridView1.Rows.Clear();
                    while (reader.Read())
                    {
                        object[] vals = new object[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                            vals[i] = reader[i];
                        comboBox1.Items.Add((int)reader["Id"]);
                        dataGridView1.Rows.Add(vals);
                    }
                    reader.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckNumbers();
            GetRooms();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex==-1)
            {
                MessageBox.Show("Не выбрана команата или на загруженыданные");
                return;
            }
            int room = (int)comboBox1.SelectedItem;

            using (var comm = sqlConnection.CreateCommand())
            {
                var sql = "UPDATE Appartaments " +
                    "SET status = @stat " +
                    "WHERE Id = @room";

                comm.Parameters.Add("@stat", SqlDbType.NVarChar, 10).Value = "free";
                comm.Parameters.Add("@room", SqlDbType.Int).Value = room;

                comm.CommandText = sql;
                comm.ExecuteNonQuery();
            }
            CheckNumbers();
            GetRooms();
        }
    }
}
