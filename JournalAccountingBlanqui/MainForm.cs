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
        PropsFields propsFields = new PropsFields(); //экземпляр класса с настройками
        CLSDB clsdb = new CLSDB();
        public int idtab = 0;
        int rowID = 0;

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
            using (Authorization Form = new Authorization())
            {
                Form.ShowDialog();
            }

            btnCancel.Visible = false;
            btnSave.Visible = false;
            SetAutoComplete();

            lblFIO.Text = propsFields.UserFIO;
            if (propsFields.UserStatus == "admin")
            {
                btnAdm.Visible = true;
            }

            UpdSpisok();
        }

        private void UpdSpisok()
        {
            dtGdVBlanqUse.DataSource = clsdb.UpdSpisokFullUser(propsFields.UserID);

            if (dtGdVBlanqUse.RowCount > 0)
            {
                dtGdVBlanqUse.Columns[6].Visible = false;
                rowID = 0;
                idtab = Convert.ToInt32(dtGdVBlanqUse.Rows[0].Cells[0].Value);
                dateTimePickerDispatch.Value = Convert.ToDateTime(dtGdVBlanqUse.Rows[0].Cells[1].Value.ToString());
                cmBxAdress.Text = dtGdVBlanqUse.Rows[0].Cells[2].Value.ToString();
                cmBxName.Text = dtGdVBlanqUse.Rows[0].Cells[3].Value.ToString();
                txBxN_Array.Text = dtGdVBlanqUse.Rows[0].Cells[4].Value.ToString();
                txBxNum.Text = dtGdVBlanqUse.Rows[0].Cells[5].Value.ToString();
            }
            else
            {
                MessageBox.Show("В журнале нет ни одной записи.");
            }
        }

        private void SetAutoComplete()
        {
            cmBxName.AutoCompleteCustomSource.Clear();
            cmBxName.AutoCompleteCustomSource = clsdb.UpdAutoComplName();
            for (int i = 0; i < cmBxName.AutoCompleteCustomSource.Count; i++)
            {
                cmBxName.Items.Insert(i, cmBxName.AutoCompleteCustomSource[i]);
            }

            cmBxAdress.AutoCompleteCustomSource.Clear();
            cmBxAdress.AutoCompleteCustomSource = clsdb.UpdAutoComplAdress();
            for (int i = 0; i < cmBxAdress.AutoCompleteCustomSource.Count; i++)
            {
                cmBxAdress.Items.Insert(i, cmBxAdress.AutoCompleteCustomSource[i]);
            }
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
            txBxSearchDate.Text = "";
            txBxSearchAdress.Text = "";
            txBxSearchName.Text = "";
            txBxSearchNArray.Text = "";
            txBxSearchNum.Text = "";
            UpdSpisok();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы действительно желаете закрыть приложение?", "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OKCancel) == DialogResult.OK)
                e.Cancel = false;
            else e.Cancel = true;
        }
    }
}
