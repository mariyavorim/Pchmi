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
        }

        private async void Заселение_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\312\Интерфейсы\Otel\Otel\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

           SqlDataReader sqlDataReader = null;
            //SqlCommand sqlComm = new SqlCommand("SELECT * FROM [Worker]", sqlConnection);
            if (sqlDataReader != null)
                sqlDataReader.Close();
            sqlDataReader = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form form1 = Application.OpenForms[0];
            form1.Show();
            this.Close();
        }
    }
}
