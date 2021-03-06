﻿using System;
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

    public partial class СвободныеНомера : Form
    {
        SqlConnection sqlConnection;
        public СвободныеНомера(SqlConnection conn)
        {
            InitializeComponent();
            sqlConnection = conn;
            this.DialogResult = DialogResult.Cancel;

        }


        void GetRoomsFromDB()
        {
            int size = 0;
            if (int.TryParse(tbSize.Text, out size)) ;
            DateTime leave = DateTime.Now.AddDays(1);
            leave = dtLeave.Value;
 var comm = sqlConnection.CreateCommand();
            comboBox1.Items.Clear();
            if(size!=0)
            {
 string sql = "SELECT * FROM GetFreeRooms(@come, @leave, @size)"
                    + "WHERE status = @State";//
           
            comm.CommandText = sql;
            comm.Parameters.Add("@come", SqlDbType.DateTime).Value = DateTime.Now;
            comm.Parameters.Add("@leave", SqlDbType.DateTime).Value = leave;
            comm.Parameters.Add("@size", SqlDbType.Int).Value = size;
            comm.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = "free";//
            }
            else
            {
                string sql = "SELECT[Appartaments].Id as room, size, cost, [status]" +
                   "FROM [Appartaments] " +
                   "WHERE status = @State";
                comm.CommandText = sql;
                comm.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = "free";//
            }

            var reader = comm.ExecuteReader();
            //dataGridView1.ColumnCount = reader.FieldCount;
            dataGridView1.Rows.Clear();
            while (reader.Read())
            {
                object[] vals = new object[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                    vals[i] = reader[i];
                comboBox1.Items.Add((int)reader["room"]);
                dataGridView1.Rows.Add(vals);
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetRoomsFromDB();
        }

        public int selectedRoom = -1;
        public DateTime leave;
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("Не загружена информация или нет свободных номеров");
                return;
            }
            selectedRoom = (int)comboBox1.SelectedItem;
            if (selectedRoom == -1)
            {
                MessageBox.Show("Не выбран номер");
            }
            leave = dtLeave.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
