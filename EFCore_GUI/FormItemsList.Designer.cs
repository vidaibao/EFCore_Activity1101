namespace EFCore_GUI
{
    partial class frmItemsList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtId = new TextBox();
            label3 = new Label();
            txtName = new TextBox();
            label4 = new Label();
            txtDescription = new TextBox();
            label5 = new Label();
            txtQuantity = new TextBox();
            label6 = new Label();
            txtNotes = new TextBox();
            checkBox1 = new CheckBox();
            label7 = new Label();
            cbxCategory = new ComboBox();
            label8 = new Label();
            lbxPlayer = new ListBox();
            dgvItems = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(0, 192, 0);
            label1.Location = new Point(271, 18);
            label1.Name = "label1";
            label1.Size = new Size(131, 31);
            label1.TabIndex = 0;
            label1.Text = "Items List";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 68);
            label2.Name = "label2";
            label2.Size = new Size(17, 15);
            label2.TabIndex = 1;
            label2.Text = "Id";
            // 
            // txtId
            // 
            txtId.Location = new Point(123, 65);
            txtId.Name = "txtId";
            txtId.Size = new Size(140, 23);
            txtId.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 97);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 1;
            label3.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(123, 94);
            txtName.Name = "txtName";
            txtName.Size = new Size(140, 23);
            txtName.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(44, 155);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 1;
            label4.Text = "Description";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(123, 152);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(140, 23);
            txtDescription.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(44, 126);
            label5.Name = "label5";
            label5.Size = new Size(53, 15);
            label5.TabIndex = 1;
            label5.Text = "Quantity";
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(123, 123);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(140, 23);
            txtQuantity.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(44, 184);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 1;
            label6.Text = "Notes";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(123, 181);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(140, 23);
            txtNotes.TabIndex = 2;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(44, 210);
            checkBox1.Name = "checkBox1";
            checkBox1.RightToLeft = RightToLeft.Yes;
            checkBox1.Size = new Size(71, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "IsOnSale";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(44, 236);
            label7.Name = "label7";
            label7.Size = new Size(54, 15);
            label7.TabIndex = 1;
            label7.Text = "Category";
            // 
            // cbxCategory
            // 
            cbxCategory.FormattingEnabled = true;
            cbxCategory.Location = new Point(123, 236);
            cbxCategory.Name = "cbxCategory";
            cbxCategory.Size = new Size(140, 23);
            cbxCategory.TabIndex = 4;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(44, 274);
            label8.Name = "label8";
            label8.Size = new Size(44, 15);
            label8.TabIndex = 1;
            label8.Text = "Players";
            // 
            // lbxPlayer
            // 
            lbxPlayer.FormattingEnabled = true;
            lbxPlayer.ItemHeight = 15;
            lbxPlayer.Location = new Point(123, 274);
            lbxPlayer.Name = "lbxPlayer";
            lbxPlayer.Size = new Size(140, 34);
            lbxPlayer.TabIndex = 5;
            // 
            // dgvItems
            // 
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Location = new Point(310, 65);
            dgvItems.Name = "dgvItems";
            dgvItems.RowTemplate.Height = 25;
            dgvItems.Size = new Size(837, 243);
            dgvItems.TabIndex = 6;
            // 
            // frmItemsList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1256, 450);
            Controls.Add(dgvItems);
            Controls.Add(lbxPlayer);
            Controls.Add(cbxCategory);
            Controls.Add(checkBox1);
            Controls.Add(txtQuantity);
            Controls.Add(label5);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(txtNotes);
            Controls.Add(label6);
            Controls.Add(txtDescription);
            Controls.Add(label4);
            Controls.Add(txtName);
            Controls.Add(label3);
            Controls.Add(txtId);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmItemsList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Items List";
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtId;
        private Label label3;
        private TextBox txtName;
        private Label label4;
        private TextBox txtDescription;
        private Label label5;
        private TextBox txtQuantity;
        private Label label6;
        private TextBox txtNotes;
        private CheckBox checkBox1;
        private Label label7;
        private ComboBox cbxCategory;
        private Label label8;
        private ListBox lbxPlayer;
        private DataGridView dgvItems;
    }
}
