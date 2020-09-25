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
        private int rowIDusers;
        private int idtabusers;

        public AdminForm()
        {
            InitializeComponent();
            label13.Visible = false;
            lblDatePrint.Visible = false;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            DisableEditModeUser();
            UpdSpisok();
            UpdSpisokUsers();
        }

        #region Вкладка Список использованных бланков

        private void UpdSpisok()
        {
            dtGdVBlanqUseAll.DataSource = adminclsdb.UpdSpisokFullUser();
            PaintRowsUse();
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

        void PaintRowsUse()
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

        #endregion

        #region Журнал учёта выдачи бланков
        private void DtGridViewIssueBlanq_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }


        #endregion

        #region Настройки системы

        #region Список пользователей

        void PaintRowsUsers()
        {
            foreach (DataGridViewRow row in dtGridViewUsers.Rows)
            {
                if (row.Cells[6].Value.ToString() == "1")                
                    row.DefaultCellStyle.BackColor = Color.MistyRose;              
            }
        }


        private void UpdSpisokUsers()
        {
            dtGridViewUsers.DataSource = adminclsdb.UpdSpisokUsers();
            //RowsUsersViev();
            PaintRowsUsers();
            if (dtGridViewUsers.RowCount > 0)
            {
                dtGridViewUsers.Columns[0].Visible = false;
                dtGridViewUsers.Columns[6].Visible = false;
                rowIDusers = 0;
                idtabusers = Convert.ToInt32(dtGridViewUsers.Rows[rowIDusers].Cells[0].Value);
                txBxUserFIO.Text = dtGridViewUsers.Rows[rowIDusers].Cells[1].Value.ToString();
                txBxUserOffice.Text = dtGridViewUsers.Rows[rowIDusers].Cells[2].Value.ToString();
                txBxUserLogin.Text = dtGridViewUsers.Rows[rowIDusers].Cells[3].Value.ToString();
                txBxUserPassword.Text = dtGridViewUsers.Rows[rowIDusers].Cells[4].Value.ToString();
                txBxUserRight.Text = dtGridViewUsers.Rows[rowIDusers].Cells[5].Value.ToString();
                if (dtGridViewUsers.Rows[rowIDusers].Cells[6].Value.ToString() == "1")
                {
                    btnDelUser.Text = "Восстановить";
                    dtGridViewUsers.Rows[rowIDusers].DefaultCellStyle.BackColor = Color.MistyRose;
                }
                else
                {
                    btnDelUser.Text = "Удалить";
                    dtGridViewUsers.Rows[rowIDusers].DefaultCellStyle.BackColor = Color.White;
                }
            }
            else
            {
                MessageBox.Show("В журнале нет ни одной записи.");
            }
        }

        private void DtGridViewUsers_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rowIDusers = e.RowIndex;
            idtabusers = Convert.ToInt32(dtGridViewUsers.Rows[rowIDusers].Cells[0].Value);
            txBxUserFIO.Text = dtGridViewUsers.Rows[rowIDusers].Cells[1].Value.ToString();
            txBxUserOffice.Text = dtGridViewUsers.Rows[rowIDusers].Cells[2].Value.ToString();
            txBxUserLogin.Text = dtGridViewUsers.Rows[rowIDusers].Cells[3].Value.ToString();
            txBxUserPassword.Text = dtGridViewUsers.Rows[rowIDusers].Cells[4].Value.ToString();
            txBxUserRight.Text = dtGridViewUsers.Rows[rowIDusers].Cells[5].Value.ToString();
            if (dtGridViewUsers.Rows[rowIDusers].Cells[6].Value.ToString() == "1")
            {
                btnDelUser.Text = "Восстановить";
                dtGridViewUsers.Rows[rowIDusers].DefaultCellStyle.BackColor = Color.MistyRose;
            }
            else
            {
                btnDelUser.Text = "Удалить";
                dtGridViewUsers.Rows[rowIDusers].DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void ChckBoxDeletedUsers_CheckStateChanged(object sender, EventArgs e)
        {
            RowsUsersViev();
        }

        private void RowsUsersViev()
        {
            if (!chckBoxDeletedUsers.Checked)
            {
                (dtGridViewUsers.DataSource as DataTable).DefaultView.RowFilter = "DELETED = 0";
            }
            else
            {
                (dtGridViewUsers.DataSource as DataTable).DefaultView.RowFilter = "DELETED = 1 OR DELETED = 0";
            }
        }

        private void BtnSaveUser_Click(object sender, EventArgs e)
        {
            DisableEditModeUser();
        }

        private void BtnUndoUser_Click(object sender, EventArgs e)
        {
            DisableEditModeUser();
            idtabusers = Convert.ToInt32(dtGridViewUsers.Rows[rowIDusers].Cells[0].Value);
            txBxUserFIO.Text = dtGridViewUsers.Rows[rowIDusers].Cells[1].Value.ToString();
            txBxUserOffice.Text = dtGridViewUsers.Rows[rowIDusers].Cells[2].Value.ToString();
            txBxUserLogin.Text = dtGridViewUsers.Rows[rowIDusers].Cells[3].Value.ToString();
            txBxUserPassword.Text = dtGridViewUsers.Rows[rowIDusers].Cells[4].Value.ToString();
            txBxUserRight.Text = dtGridViewUsers.Rows[rowIDusers].Cells[5].Value.ToString();
            if (dtGridViewUsers.Rows[rowIDusers].Cells[6].Value.ToString() == "1")
            {
                btnDelUser.Text = "Восстановить";
            }
            else
            {
                btnDelUser.Text = "Удалить";
            }
        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            EnableEditModeUser();
            idtabusers = 0;
            txBxUserFIO.Text = "";
            txBxUserOffice.Text = "";
            txBxUserLogin.Text = "";
            txBxUserPassword.Text = "";
            txBxUserRight.Text = "user";
        }

        private void BtnEditUser_Click(object sender, EventArgs e)
        {
            EnableEditModeUser();
        }        
        private void BtnDelUser_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Отключение режима редактирования пользователей
        /// </summary>
        private void DisableEditModeUser()
        {
            txBxUserLogin.Enabled = false; 
            txBxUserPassword.Enabled = false;
            txBxUserRight.Enabled = false;
            txBxUserFIO.Enabled = false;
            txBxUserOffice.Enabled = false;
            chckBoxDeletedUsers.Enabled = true;            
            dtGridViewUsers.Enabled = true;
            btnSaveUser.Visible = false;
            btnUndoUser.Visible = false;
            btnAddUser.Visible = true;
            btnEditUser.Visible = true;
        }

        /// <summary>
        /// Включение режима редактирования пользователей
        /// </summary>
        private void EnableEditModeUser()
        {
            txBxUserLogin.Enabled = true;
            txBxUserPassword.Enabled = true;
            txBxUserRight.Enabled = true;
            txBxUserFIO.Enabled = true;
            txBxUserOffice.Enabled = true;
            chckBoxDeletedUsers.Enabled = false;
            dtGridViewUsers.Enabled = false;
            btnSaveUser.Visible = true;
            btnUndoUser.Visible = true;
            btnAddUser.Visible = false;
            btnEditUser.Visible = false;
        }

        #endregion

        #region Список бланков
        private void DtGridViewBlanq_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void BtnSaveBlank_Click(object sender, EventArgs e)
        {
            DisableEditModeBlank();
        }

        private void BtnUndoBlank_Click(object sender, EventArgs e)
        {
            DisableEditModeBlank();
        }

        private void BtnAddBlank_Click(object sender, EventArgs e)
        {
            EnableEditModeBlank();
        }

        private void BtnDelBlank_Click(object sender, EventArgs e)
        {

        }

        private void BtnEditBlank_Click(object sender, EventArgs e)
        {
            EnableEditModeBlank();
        }

        /// <summary>
        /// Отключение режима редактирования пользователей
        /// </summary>
        private void DisableEditModeBlank()
        {
            txBxUserNameBlank.Enabled = false;

            dtGridViewBlanq.Enabled = true;
            btnSaveBlank.Visible = false;
            btnUndoBlank.Visible = false;
            btnAddBlank.Visible = true;
            btnDelBlank.Visible = true;
            btnEditBlank.Visible = true;
        }

        /// <summary>
        /// Включение режима редактирования пользователей
        /// </summary>
        private void EnableEditModeBlank()
        {
            txBxUserNameBlank.Enabled = true;

            dtGridViewBlanq.Enabled = false;
            btnSaveBlank.Visible = true;
            btnUndoBlank.Visible = true;
            btnAddBlank.Visible = false;
            btnDelBlank.Visible = false;
            btnEditBlank.Visible = false;
        }

        #endregion

        #endregion


    }
}
