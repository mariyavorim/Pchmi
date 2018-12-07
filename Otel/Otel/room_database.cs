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
    public partial class room_database : Form
    {
        public room_database()
        {
            InitializeComponent();
        }

        private void table_roomBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.table_roomBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet);

        }

        private void room_database_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseDataSet.Table_room' table. You can move, or remove it, as needed.
            this.table_roomTableAdapter.Fill(this.databaseDataSet.Table_room);

        }

        private void table_roomDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
