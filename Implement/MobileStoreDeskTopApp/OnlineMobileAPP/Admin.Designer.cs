namespace OnlineMobileAPP
{
    partial class Admin
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
            label_welocme_admin = new Label();
            pictureBox1 = new PictureBox();
            btn_deleteproduct = new Button();
            btn_delete_User = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label_welocme_admin
            // 
            label_welocme_admin.AutoSize = true;
            label_welocme_admin.Font = new Font("Sitka Small", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_welocme_admin.Location = new Point(292, 43);
            label_welocme_admin.Name = "label_welocme_admin";
            label_welocme_admin.Size = new Size(202, 33);
            label_welocme_admin.TabIndex = 0;
            label_welocme_admin.Text = "Welcome Admin";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.womentechtable;
            pictureBox1.Location = new Point(500, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(58, 59);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // btn_deleteproduct
            // 
            btn_deleteproduct.FlatAppearance.BorderSize = 0;
            btn_deleteproduct.FlatAppearance.MouseDownBackColor = Color.Red;
            btn_deleteproduct.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 0, 0);
            btn_deleteproduct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn_deleteproduct.Location = new Point(461, 183);
            btn_deleteproduct.Name = "btn_deleteproduct";
            btn_deleteproduct.Size = new Size(170, 60);
            btn_deleteproduct.TabIndex = 3;
            btn_deleteproduct.Text = "Delete Product";
            btn_deleteproduct.UseVisualStyleBackColor = true;
            btn_deleteproduct.Click += btn_deleteproduct_Click;
            // 
            // btn_delete_User
            // 
            btn_delete_User.FlatAppearance.BorderSize = 0;
            btn_delete_User.FlatAppearance.MouseDownBackColor = Color.Red;
            btn_delete_User.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 0, 0);
            btn_delete_User.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn_delete_User.Location = new Point(189, 183);
            btn_delete_User.Name = "btn_delete_User";
            btn_delete_User.Size = new Size(170, 60);
            btn_delete_User.TabIndex = 4;
            btn_delete_User.Text = "Delete User";
            btn_delete_User.UseVisualStyleBackColor = true;
            btn_delete_User.Click += btn_delete_User_Click;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_delete_User);
            Controls.Add(btn_deleteproduct);
            Controls.Add(pictureBox1);
            Controls.Add(label_welocme_admin);
            Name = "Admin";
            Text = "Admin";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_welocme_admin;
        private PictureBox pictureBox1;
        private Button btn_deleteproduct;
        private Button btn_delete_User;
    }
}