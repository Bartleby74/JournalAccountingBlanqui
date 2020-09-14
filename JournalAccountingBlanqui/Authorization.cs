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
        public Authorization()
        {
            InitializeComponent(); 
            txBxPass.UseSystemPasswordChar = true;
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
    }
}
