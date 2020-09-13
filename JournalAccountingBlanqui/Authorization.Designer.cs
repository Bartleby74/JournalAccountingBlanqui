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
            ((System.ComponentModel.ISupportInitialize)(this.picBxShow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(13, 78);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(210, 35);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Авторизация";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(230, 78);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(143, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль";
            // 
            // txBxLogin
            // 
            this.txBxLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txBxLogin.Location = new System.Drawing.Point(85, 12);
            this.txBxLogin.Name = "txBxLogin";
            this.txBxLogin.Size = new System.Drawing.Size(288, 26);
            this.txBxLogin.TabIndex = 4;
            // 
            // txBxPass
            // 
            this.txBxPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txBxPass.Location = new System.Drawing.Point(85, 44);
            this.txBxPass.Name = "txBxPass";
            this.txBxPass.Size = new System.Drawing.Size(288, 26);
            this.txBxPass.TabIndex = 5;
            this.txBxPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txBxPass_KeyDown);
            // 
            // picBxShow
            // 
            this.picBxShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picBxShow.BackColor = System.Drawing.Color.White;
            this.picBxShow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBxShow.BackgroundImage")));
            this.picBxShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBxShow.ErrorImage = null;
            this.picBxShow.InitialImage = null;
            this.picBxShow.Location = new System.Drawing.Point(337, 44);
            this.picBxShow.Name = "picBxShow";
            this.picBxShow.Size = new System.Drawing.Size(36, 26);
            this.picBxShow.TabIndex = 6;
            this.picBxShow.TabStop = false;
            this.picBxShow.MouseLeave += new System.EventHandler(this.picBxShow_MouseLeave);
            this.picBxShow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBxShow_MouseMove);
            // 
            // Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 125);
            this.Controls.Add(this.picBxShow);
            this.Controls.Add(this.txBxPass);
            this.Controls.Add(this.txBxLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(401, 164);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(401, 164);
            this.Name = "Authorization";
            this.Text = "Авторизация пользователя";
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
    }
}