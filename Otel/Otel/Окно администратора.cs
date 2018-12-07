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
    public partial class bron : Form
    {
        SqlConnection sqlConn;
        int sutki = 0;
        int nb = 0;
        int na = 0;
        public bron()
        {
            InitializeComponent();
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label12.Text == "свободен")
            {
                nb += 1;
                SqlCommand SQLcomm;
                SQLcomm = new SqlCommand("INSER INTO [Table_client] (Id, Номер_апартаментов, Имя, Фамилия, Паспортные_данные) VALUES(@Id, @Номер_апартаментов, @Имя, @Фамилия, @Паспортные_данные)", sqlConn);
                SQLcomm.Parameters.Add("@Id", SqlDbType.NVarChar, 20).Value = nb;
                SQLcomm.Parameters.Add("@Номер_апартаментов", SqlDbType.NVarChar, 20).Value = na;
                SQLcomm.Parameters.Add("@Имя", SqlDbType.NVarChar, 20).Value = textBox1.Text;
                SQLcomm.Parameters.Add("@Фамилия", SqlDbType.NVarChar, 20).Value = textBox2.Text;
                SQLcomm.Parameters.Add("@Паспортные_данные", SqlDbType.NVarChar, 10).Value = textBox3.Text;
                await SQLcomm.ExecuteNonQueryAsync();

                label8.Text = Convert.ToString(nb);



            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            SqlDataReader sqlDataReader = null;
            SqlCommand sqlComm;
            string dz = null, dv = null;

            sqlComm = new SqlCommand("SELECT [Дата_заезда] FROM (Table_room) WHERE [Тип_номера]=@Тип_номера AND [количество_мест]=@количество_мест", sqlConn);
            sqlComm.Parameters.Add("@Тип_номера", SqlDbType.NVarChar, 20).Value = comboBox2.Text;
            sqlComm.Parameters.Add("@количество_мест", SqlDbType.NVarChar, 20).Value = comboBox1.Text;
            sqlDataReader = await sqlComm.ExecuteReaderAsync();
            if (await sqlDataReader.ReadAsync())
            {
                dz = Convert.ToString(sqlDataReader["Дата_заезда"]);

            }

            sqlComm = new SqlCommand("SELECT [Дата_выселения] FROM (Table_room) WHERE [Тип_номера]=@Тип_номера AND [количество_мест]=@количество_мест", sqlConn);
            sqlComm.Parameters.Add("@Тип_номера", SqlDbType.NVarChar, 20).Value = comboBox2.Text;
            sqlComm.Parameters.Add("@количество_мест", SqlDbType.NVarChar, 20).Value = comboBox1.Text;
            sqlDataReader = await sqlComm.ExecuteReaderAsync();
            if (await sqlDataReader.ReadAsync())
            {
                dv = Convert.ToString(sqlDataReader["Дата_выселения"]);

            }
            if ((dz == "NULL") && (dv == "NULL"))
            {
                {
                    sqlComm = new SqlCommand("INSER INTO [Table_room] (Дата_заезда, Дата_выселения) VALUES(@Дата_заезда, @Дата_выселения) ", sqlConn);
                    sqlComm.Parameters.Add("@Дата_заезда", SqlDbType.NVarChar, 20).Value = dateTimePicker1;
                    sqlComm.Parameters.Add("@Дата_выселения", SqlDbType.NVarChar, 20).Value = dateTimePicker2;

                    sqlDataReader = await sqlComm.ExecuteReaderAsync();
                    await sqlComm.ExecuteNonQueryAsync();

                }

            }
            else {
                sqlComm = new SqlCommand("SELECT [Статус] AND [Апартаменты] FROM (Table_room) WHERE  [Датазаезда]=@Дата_заезда BETWEEN  [Дата_заезда]  AND [Дата_выселения]=@Дата_выселения", sqlConn);
                sqlComm.Parameters.Add("@Дата_заезда", SqlDbType.NVarChar, 20).Value = dateTimePicker1;
               
                sqlDataReader = await sqlComm.ExecuteReaderAsync();
                if (await sqlDataReader.ReadAsync())
                {
                    label12.Text = Convert.ToString(sqlDataReader["Статус"]);
                    na = Convert.ToInt32(sqlDataReader["Апартаменты"]);


                }

            }
        }

        private async void Окно_администратора_Load(object sender, EventArgs e)
        {
            string ProjectWay = System.IO.Directory.GetCurrentDirectory().Remove(System.IO.Directory.GetCurrentDirectory().Length - "bin.Debug".Length);
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ProjectWay + @"Database.mdf;Integrated Security=True";
            sqlConn = new SqlConnection(connStr);
            await sqlConn.OpenAsync();
            SqlDataReader sqlDataReader = null;
            
            
            if (sqlDataReader != null)
                sqlDataReader.Close();
            sqlDataReader = null;


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form form1 = Application.OpenForms[0];
            form1.Show();
            this.Close();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader sqlDataReader = null;
            SqlCommand sqlComm;
            string stoimost = null;

            sqlComm = new SqlCommand("SELECT DISTINCT [Стоимость_в_сутки] FROM (Table_room)  WHERE [Тип_номера]=@Тип_номера AND [количество_мест]=@количество_мест", sqlConn);
            sqlComm.Parameters.Add("@Тип_номера", SqlDbType.NVarChar, 20).Value = comboBox2.Text;
            sqlComm.Parameters.Add("@количество_мест", SqlDbType.NVarChar, 20).Value = comboBox1.Text;


            sqlDataReader = await sqlComm.ExecuteReaderAsync();
            if (await sqlDataReader.ReadAsync())
            {
                stoimost = Convert.ToString(sqlDataReader["Стоимость_в_сутки"]);
                label12.Text = stoimost;
            }
        }

        private async void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader sqlDataReader = null;
            SqlCommand sqlComm;
            string stoimost = null;

            sqlComm = new SqlCommand("SELECT DISTINCT [Стоимость_в_сутки] FROM (Table_room)  WHERE [Тип_номера]=@Тип_номера AND [количество_мест]=@количество_мест", sqlConn);
            sqlComm.Parameters.Add("@Тип_номера", SqlDbType.NVarChar, 20).Value = comboBox2.Text;
            sqlComm.Parameters.Add("@количество_мест", SqlDbType.NVarChar, 20).Value = comboBox1.Text;

            sqlDataReader = await sqlComm.ExecuteReaderAsync();
            if (await sqlDataReader.ReadAsync())
            {
                stoimost = Convert.ToString(sqlDataReader["Стоимость_в_сутки"]);
                label12.Text = stoimost;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Status_Click(object sender, EventArgs e)
        {

        }
    }
}
