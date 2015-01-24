namespace MotorProtection.UI
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.picUsername = new System.Windows.Forms.PictureBox();
            this.picPassword = new System.Windows.Forms.PictureBox();
            this.lblCopyRight = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picUsername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(37, 28);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(185, 21);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(37, 55);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(185, 21);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // btnSignIn
            // 
            this.btnSignIn.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignIn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSignIn.Location = new System.Drawing.Point(147, 82);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(75, 25);
            this.btnSignIn.TabIndex = 4;
            this.btnSignIn.Text = "登录";
            this.btnSignIn.UseVisualStyleBackColor = false;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(13, 8);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 12);
            this.lblMsg.TabIndex = 5;
            // 
            // picUsername
            // 
            this.picUsername.Image = ((System.Drawing.Image)(resources.GetObject("picUsername.Image")));
            this.picUsername.Location = new System.Drawing.Point(12, 30);
            this.picUsername.Name = "picUsername";
            this.picUsername.Size = new System.Drawing.Size(16, 16);
            this.picUsername.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picUsername.TabIndex = 0;
            this.picUsername.TabStop = false;
            // 
            // picPassword
            // 
            this.picPassword.Image = ((System.Drawing.Image)(resources.GetObject("picPassword.Image")));
            this.picPassword.Location = new System.Drawing.Point(12, 57);
            this.picPassword.Name = "picPassword";
            this.picPassword.Size = new System.Drawing.Size(16, 16);
            this.picPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPassword.TabIndex = 6;
            this.picPassword.TabStop = false;
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.AutoSize = true;
            this.lblCopyRight.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCopyRight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCopyRight.Location = new System.Drawing.Point(12, 118);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(216, 11);
            this.lblCopyRight.TabIndex = 7;
            this.lblCopyRight.Text = "Copyright 2015 大连环宇佳机电有限公司";
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnSignIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 139);
            this.Controls.Add(this.lblCopyRight);
            this.Controls.Add(this.picPassword);
            this.Controls.Add(this.picUsername);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统登录";
            ((System.ComponentModel.ISupportInitialize)(this.picUsername)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox picUsername;
        private System.Windows.Forms.PictureBox picPassword;
        private System.Windows.Forms.Label lblCopyRight;
    }
}