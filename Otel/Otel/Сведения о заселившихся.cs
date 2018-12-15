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
    public partial class Сведения_о_заселившихся : Form
    {
        SqlConnection sqlConnection;
        public Сведения_о_заселившихся(SqlConnection conn)
        {
            InitializeComponent();
            sqlConnection = conn;
        }

        private void Сведения_о_заселившихся_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM AccomodView";
            var comm = sqlConnection.CreateCommand();
            comm.CommandText = sql;
            var reader = comm.ExecuteReader();
            dataGridView1.ColumnCount = reader.FieldCount;

            while (reader.Read())
            {
                object[] vals = new object[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                    //    if (i == 4)
                    //    {
                    //        vals[i] = (bool)reader[i] ? "свободен" : "занят";
                    //    }
                    //    else
                    vals[i] = reader[i];

                dataGridView1.Rows.Add(vals);
            }
            reader.Close();
        }
    }
}

