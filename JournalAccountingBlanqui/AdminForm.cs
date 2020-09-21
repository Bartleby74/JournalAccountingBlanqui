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
    public partial class AdminForm : Form
    {
        AdminCLSDB adminclsdb = new AdminCLSDB();
        public int idtab = 0;
        int rowID = 0;

        public AdminForm()
        {
            InitializeComponent();
            label13.Visible = false;
            lblDatePrint.Visible = false;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            UpdSpisok();
        }

        private void UpdSpisok()
        {
            dtGdVBlanqUseAll.DataSource = adminclsdb.UpdSpisokFullUser();
            PaintRows();
            if (dtGdVBlanqUseAll.RowCount > 0)
            {
                dtGdVBlanqUseAll.Columns[7].Visible = false;
                dtGdVBlanqUseAll.Columns[8].Visible = false;
                rowID = 0;
                idtab = Convert.ToInt32(dtGdVBlanqUseAll.Rows[rowID].Cells[0].Value);
                lblDateUse.Text = Convert.ToDateTime(dtGdVBlanqUseAll.Rows[rowID].Cells[1].Value.ToString()).ToLongDateString();
                lblFIO.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[2].Value.ToString();
                lblNameBlank.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[3].Value.ToString();
                lblAdress.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[4].Value.ToString();
                lblArrayNumber.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[5].Value.ToString();
                lblNumBlank.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[6].Value.ToString();
                if (dtGdVBlanqUseAll.Rows[rowID].Cells[7].Value.ToString() == "1")
                {
                    lblDatePrint.Visible = true;
                    lblDatePrint.Text = Convert.ToDateTime(dtGdVBlanqUseAll.Rows[rowID].Cells[8].Value.ToString()).ToLongDateString();
                }
                else
                {
                    lblDatePrint.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("В журнале нет ни одной записи.");
            }
        }

        void PaintRows()
        {
            foreach (DataGridViewRow row in dtGdVBlanqUseAll.Rows)
            {
                if (row.Cells[7].Value.ToString() == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
                   
            }
        }

        private void DtGdVBlanqUseAll_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rowID = e.RowIndex;
            idtab = Convert.ToInt32(dtGdVBlanqUseAll.Rows[rowID].Cells[0].Value);
            lblDateUse.Text = Convert.ToDateTime(dtGdVBlanqUseAll.Rows[rowID].Cells[1].Value.ToString()).ToLongDateString();
            lblFIO.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[2].Value.ToString();
            lblNameBlank.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[3].Value.ToString();
            lblAdress.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[4].Value.ToString();
            lblArrayNumber.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[5].Value.ToString();
            lblNumBlank.Text = dtGdVBlanqUseAll.Rows[rowID].Cells[6].Value.ToString();
            if (dtGdVBlanqUseAll.Rows[rowID].Cells[7].Value.ToString() == "1")
            {
                label13.Visible = true;
                lblDatePrint.Visible = true;
                lblDatePrint.Text = Convert.ToDateTime(dtGdVBlanqUseAll.Rows[rowID].Cells[8].Value.ToString()).ToLongDateString();
            }
            else
            {
                label13.Visible = false;
                lblDatePrint.Visible = false;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxBxSearchDate_TextChanged(object sender, EventArgs e)
        {
            (dtGdVBlanqUseAll.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(NUM_BLANK, System.String) LIKE '%" + (txBxSearchNum.Text.Trim()) + "%' AND ARRAY_NUMBER LIKE '%" + txBxSearchNArray.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchName.Text.Trim() + "%' AND ADRESS LIKE '%" + txBxSearchAdress.Text.Trim() + "%' AND CONVERT(DATE_USE, System.String) LIKE '%" + txBxSearchDate.Text.Trim() + "%' AND CONVERT(FIO, System.String) LIKE '%" + txBxSearchFIO.Text.Trim() + "%'";
        }

        private void TxBxSearchName_TextChanged(object sender, EventArgs e)
        {
            (dtGdVBlanqUseAll.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(NUM_BLANK, System.String) LIKE '%" + (txBxSearchNum.Text.Trim()) + "%' AND ARRAY_NUMBER LIKE '%" + txBxSearchNArray.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchName.Text.Trim() + "%' AND ADRESS LIKE '%" + txBxSearchAdress.Text.Trim() + "%' AND CONVERT(DATE_USE, System.String) LIKE '%" + txBxSearchDate.Text.Trim() + "%' AND CONVERT(FIO, System.String) LIKE '%" + txBxSearchFIO.Text.Trim() + "%'";
        }

        private void TxBxSearchAdress_TextChanged(object sender, EventArgs e)
        {
            (dtGdVBlanqUseAll.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(NUM_BLANK, System.String) LIKE '%" + (txBxSearchNum.Text.Trim()) + "%' AND ARRAY_NUMBER LIKE '%" + txBxSearchNArray.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchName.Text.Trim() + "%' AND ADRESS LIKE '%" + txBxSearchAdress.Text.Trim() + "%' AND CONVERT(DATE_USE, System.String) LIKE '%" + txBxSearchDate.Text.Trim() + "%' AND CONVERT(FIO, System.String) LIKE '%" + txBxSearchFIO.Text.Trim() + "%'";
        }

        private void TxBxSearchNArray_TextChanged(object sender, EventArgs e)
        {
            (dtGdVBlanqUseAll.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(NUM_BLANK, System.String) LIKE '%" + (txBxSearchNum.Text.Trim()) + "%' AND ARRAY_NUMBER LIKE '%" + txBxSearchNArray.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchName.Text.Trim() + "%' AND ADRESS LIKE '%" + txBxSearchAdress.Text.Trim() + "%' AND CONVERT(DATE_USE, System.String) LIKE '%" + txBxSearchDate.Text.Trim() + "%' AND CONVERT(FIO, System.String) LIKE '%" + txBxSearchFIO.Text.Trim() + "%'";
        }

        private void TxBxSearchNum_TextChanged(object sender, EventArgs e)
        {
            (dtGdVBlanqUseAll.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(NUM_BLANK, System.String) LIKE '%" + (txBxSearchNum.Text.Trim()) + "%' AND ARRAY_NUMBER LIKE '%" + txBxSearchNArray.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchName.Text.Trim() + "%' AND ADRESS LIKE '%" + txBxSearchAdress.Text.Trim() + "%' AND CONVERT(DATE_USE, System.String) LIKE '%" + txBxSearchDate.Text.Trim() + "%' AND CONVERT(FIO, System.String) LIKE '%" + txBxSearchFIO.Text.Trim() + "%'";
        }

        private void TxBxSearchFIO_TextChanged(object sender, EventArgs e)
        {
            (dtGdVBlanqUseAll.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(NUM_BLANK, System.String) LIKE '%" + (txBxSearchNum.Text.Trim()) + "%' AND ARRAY_NUMBER LIKE '%" + txBxSearchNArray.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchName.Text.Trim() + "%' AND ADRESS LIKE '%" + txBxSearchAdress.Text.Trim() + "%' AND CONVERT(DATE_USE, System.String) LIKE '%" + txBxSearchDate.Text.Trim() + "%' AND CONVERT(FIO, System.String) LIKE '%" + txBxSearchFIO.Text.Trim() + "%'";
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txBxSearchDate.Text = ""; 
            txBxSearchFIO.Text = "";
            txBxSearchAdress.Text = "";
            txBxSearchName.Text = "";
            txBxSearchNArray.Text = "";
            txBxSearchNum.Text = "";
            UpdSpisok();
        }
    }
}
