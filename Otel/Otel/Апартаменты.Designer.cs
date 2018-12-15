namespace Otel
{
    partial class Апартаменты
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Вместимость = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Состояние = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Стоимость = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Вместимость,
            this.Состояние,
            this.Стоимость});
            this.dataGridView1.Location = new System.Drawing.Point(23, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(758, 634);
            this.dataGridView1.TabIndex = 1;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            // 
            // Вместимость
            // 
            this.Вместимость.HeaderText = "Вместимость";
            this.Вместимость.Name = "Вместимость";
            // 
            // Состояние
            // 
            this.Состояние.HeaderText = "Состояние";
            this.Состояние.Name = "Состояние";
            // 
            // Стоимость
            // 
            this.Стоимость.HeaderText = "Стоимость";
            this.Стоимость.Name = "Стоимость";
            // 
            // Апартаменты
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(805, 681);
            this.Controls.Add(this.dataGridView1);
            this.MaximumSize = new System.Drawing.Size(821, 719);
            this.MinimumSize = new System.Drawing.Size(821, 719);
            this.Name = "Апартаменты";
            this.Text = "Апартаменты";
            this.Load += new System.EventHandler(this.Апартаменты_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Вместимость;
        private System.Windows.Forms.DataGridViewTextBoxColumn Состояние;
        private System.Windows.Forms.DataGridViewTextBoxColumn Стоимость;
    }
}