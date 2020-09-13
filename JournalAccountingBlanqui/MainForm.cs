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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {

        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

        }

        private void DtGdVBlanqUse_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void TxBxSearchNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxBxSearchNArray_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxBxSearchName_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxBxSearchAdress_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxBxSearchDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы действительно желаете закрыть приложение?", "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OKCancel) == DialogResult.OK)
                e.Cancel = false;
            else e.Cancel = true;
        }
    }
}
