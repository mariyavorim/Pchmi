using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel
{
    public partial class start : Form
    {
        public start()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zacelenie zacelenieForm = new zacelenie();
            zacelenieForm.Show();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bron bronForm = new bron();
            bronForm.Show();
            this.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form form1 = Application.OpenForms[0];
            form1.Show();
            this.Close();
        }
    }
}
