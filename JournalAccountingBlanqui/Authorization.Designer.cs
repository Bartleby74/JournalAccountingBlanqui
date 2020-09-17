using System.Drawing;

namespace JournalAccountingBlanqui
{
    partial class Authorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Authorization));
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txBxLogin = new System.Windows.Forms.TextBox();
            this.txBxPass = new System.Windows.Forms.TextBox();
            this.picBxShow = new System.Windows.Forms.PictureBox();
            this.chckBxRemember = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBxShow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.Location = new System.Drawing.Point(188, 137);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(131, 35);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Авторизация";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(326, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль";
            // 
            // txBxLogin
            // 
            this.txBxLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txBxLogin.Location = new System.Drawing.Point(86, 71);
            this.txBxLogin.Name = "txBxLogin";
            this.txBxLogin.Size = new System.Drawing.Size(324, 26);
            this.txBxLogin.TabIndex = 0;
            this.txBxLogin.Text = "Konnov";
            // 
            // txBxPass
            // 
            this.txBxPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txBxPass.Location = new System.Drawing.Point(85, 103);
            this.txBxPass.Name = "txBxPass";
            this.txBxPass.Size = new System.Drawing.Size(324, 26);
            this.txBxPass.TabIndex = 1;
            this.txBxPass.Text = "5449";
            this.txBxPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxBxPass_KeyDown);
            // 
            // picBxShow
            // 
            this.picBxShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picBxShow.BackColor = System.Drawing.Color.White;
            this.picBxShow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBxShow.BackgroundImage")));
            this.picBxShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBxShow.ErrorImage = null;
            this.picBxShow.InitialImage = null;
            this.picBxShow.Location = new System.Drawing.Point(375, 104);
            this.picBxShow.Margin = new System.Windows.Forms.Padding(0);
            this.picBxShow.Name = "picBxShow";
            this.picBxShow.Size = new System.Drawing.Size(35, 24);
            this.picBxShow.TabIndex = 6;
            this.picBxShow.TabStop = false;
            this.picBxShow.MouseLeave += new System.EventHandler(this.PicBxShow_MouseLeave);
            this.picBxShow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicBxShow_MouseMove);
            // 
            // chckBxRemember
            // 
            this.chckBxRemember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chckBxRemember.AutoSize = true;
            this.chckBxRemember.BackColor = System.Drawing.Color.Transparent;
            this.chckBxRemember.Location = new System.Drawing.Point(12, 143);
            this.chckBxRemember.Name = "chckBxRemember";
            this.chckBxRemember.Size = new System.Drawing.Size(152, 24);
            this.chckBxRemember.TabIndex = 7;
            this.chckBxRemember.Text = "запомнить меня";
            this.chckBxRemember.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(80, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(260, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Авторизация пользователя";
            // 
            // Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(421, 184);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chckBxRemember);
            this.Controls.Add(this.picBxShow);
            this.Controls.Add(this.txBxPass);
            this.Controls.Add(this.txBxLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(421, 184);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(421, 184);
            this.Name = "Authorization";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация пользователя";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Authorization_FormClosing);
            this.Load += new System.EventHandler(this.Authorization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBxShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txBxLogin;
        private System.Windows.Forms.TextBox txBxPass;
        private System.Windows.Forms.PictureBox picBxShow;
        private System.Windows.Forms.CheckBox chckBxRemember;
        private System.Windows.Forms.Label label3;
    }
}