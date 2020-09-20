using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JournalAccountingBlanqui
{
    public partial class Authorization : Form
    {
        CLSDB clsdb = new CLSDB(); 
        Props props = new Props(); //экземпляр класса с настройками
        public Authorization()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            PerformInput();
        }

        private void PerformInput()
        {
            bool a = clsdb.SqlPassword(txBxLogin.Text, txBxPass.Text);
            if (a)
            {
                if (chckBxRemember.Checked == true)
                {
                    props.Fields.UserLogin = txBxLogin.Text; 
                    props.Fields.UserPassw = txBxPass.Text;
                }
                else
                {
                    props.Fields.UserLogin = ""; 
                    props.Fields.UserPassw = "";
                }
                props.WriteXml();

                using (MainForm Form = new MainForm())
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Неверный \"Пароль\"");
                return;
            }
        }

        private void PicBxShow_MouseMove(object sender, MouseEventArgs e)
        {
            txBxPass.UseSystemPasswordChar = false;
        }

        private void PicBxShow_MouseLeave(object sender, EventArgs e)
        {
            txBxPass.UseSystemPasswordChar = true;
        }

        private void TxBxPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformInput();
            }
        }

        private void Authorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
            props.ReadXml();
            txBxPass.UseSystemPasswordChar = true;
            txBxLogin.Text = props.Fields.UserLogin;
            txBxPass.Text = props.Fields.UserPassw;
            if (txBxLogin.Text.Length > 0)
                chckBxRemember.Checked = true;
            else
                chckBxRemember.Checked = false;
        }
    }
}
