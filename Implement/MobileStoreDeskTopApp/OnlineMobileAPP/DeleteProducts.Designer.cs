namespace OnlineMobileAPP
{
    partial class DeleteProducts
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
            label_idproduct = new Label();
            textBox_prodid = new TextBox();
            btn_deleteproduct = new Button();
            dataGridView_products = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView_products).BeginInit();
            SuspendLayout();
            // 
            // label_idproduct
            // 
            label_idproduct.AutoSize = true;
            label_idproduct.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_idproduct.Location = new Point(255, 33);
            label_idproduct.Name = "label_idproduct";
            label_idproduct.Size = new Size(149, 25);
            label_idproduct.TabIndex = 0;
            label_idproduct.Text = "Enter Product Id :";
            // 
            // textBox_prodid
            // 
            textBox_prodid.Location = new Point(430, 31);
            textBox_prodid.Name = "textBox_prodid";
            textBox_prodid.Size = new Size(159, 27);
            textBox_prodid.TabIndex = 1;
            // 
            // btn_deleteproduct
            // 
            btn_deleteproduct.FlatAppearance.MouseDownBackColor = Color.Red;
            btn_deleteproduct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn_deleteproduct.Location = new Point(616, 25);
            btn_deleteproduct.Name = "btn_deleteproduct";
            btn_deleteproduct.Size = new Size(106, 38);
            btn_deleteproduct.TabIndex = 2;
            btn_deleteproduct.Text = "Delete";
            btn_deleteproduct.UseVisualStyleBackColor = true;
            btn_deleteproduct.Click += btn_deleteproduct_Click;
            // 
            // dataGridView_products
            // 
            dataGridView_products.BackgroundColor = SystemColors.Control;
            dataGridView_products.BorderStyle = BorderStyle.None;
            dataGridView_products.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_products.Location = new Point(39, 146);
            dataGridView_products.Name = "dataGridView_products";
            dataGridView_products.RowHeadersWidth = 51;
            dataGridView_products.Size = new Size(922, 245);
            dataGridView_products.TabIndex = 3;
            
            // 
            // DeleteProducts
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 462);
            Controls.Add(dataGridView_products);
            Controls.Add(btn_deleteproduct);
            Controls.Add(textBox_prodid);
            Controls.Add(label_idproduct);
            Name = "DeleteProducts";
            Text = "DeleteProducts";
            Load += DeleteProducts_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_products).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_idproduct;
        private TextBox textBox_prodid;
        private Button btn_deleteproduct;
        private DataGridView dataGridView_products;
    }
}