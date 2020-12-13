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
        bool editStatusIssue = true;
        bool editStatusUsers = true;
        bool editStatusBlank = true;
        //public int idtab = 0;
        int rowIDuse = 0;
        int rowIDissue = 0;
        int rowIDusers = 0;
        int rowIDblank = 0;
        int idTabuse = 0;
        int idTabissue = 0;
        int idTabusers = 0;
        int idTabblank = 0;

        public AdminForm()
        {
            InitializeComponent();
            label13.Visible = false;
            lblDatePrint.Visible = false;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaintRowsUsers();
            PaintRowsUse();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            DisableEditModeUsers();
            DisableEditModeBlank();
            DisableEditModeIssuingBlank();
            UpdSpisokFullUse();
            UpdSpisokUsers(); 
            PaintRowsUsers();
            RowsUsersViev();
            UpdSpisokBlanq(); 
            UpdIssuanceBlanq();
            SetAutoCompleteFIO();
            SetAutoCompleteBlanq();
        }

        private void SetAutoCompleteBlanq()
        {
            cmBxIssueNBlank.AutoCompleteCustomSource.Clear();
            cmBxIssueNBlank.AutoCompleteCustomSource = adminclsdb.UpdAutoComplName();
            for (int i = 0; i < cmBxIssueNBlank.AutoCompleteCustomSource.Count; i++)
            {
                cmBxIssueNBlank.Items.Insert(i, cmBxIssueNBlank.AutoCompleteCustomSource[i]);
            }
        }

        private void SetAutoCompleteFIO()
        {
            cmBxIssueFIO.AutoCompleteCustomSource.Clear();
            cmBxIssueFIO.AutoCompleteCustomSource = adminclsdb.UpdAutoComplUser();
            for (int i = 0; i < cmBxIssueFIO.AutoCompleteCustomSource.Count; i++)
            {
                cmBxIssueFIO.Items.Insert(i, cmBxIssueFIO.AutoCompleteCustomSource[i]);
            }
        }

        #region Вкладка Список использованных бланков

        private void UpdSpisokFullUse()
        {
            dtGdVBlanqUseAll.DataSource = adminclsdb.GetSpisokFullUse();
            PaintRowsUse();
            if (dtGdVBlanqUseAll.RowCount > 0)
            {
                dtGdVBlanqUseAll.Columns[7].Visible = false;
                dtGdVBlanqUseAll.Columns[8].Visible = false;
                rowIDuse = 0;
                idTabuse = Convert.ToInt32(dtGdVBlanqUseAll.Rows[rowIDuse].Cells[0].Value);
                lblDateUse.Text = Convert.ToDateTime(dtGdVBlanqUseAll.Rows[rowIDuse].Cells[1].Value.ToString()).ToLongDateString();
                lblFIO.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[2].Value.ToString();
                lblNameBlank.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[3].Value.ToString();
                lblAdress.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[4].Value.ToString();
                lblArrayNumber.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[5].Value.ToString();
                lblNumBlank.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[6].Value.ToString();
                if (dtGdVBlanqUseAll.Rows[rowIDuse].Cells[7].Value.ToString() == "1")
                {
                    lblDatePrint.Visible = true;
                    lblDatePrint.Text = Convert.ToDateTime(dtGdVBlanqUseAll.Rows[rowIDuse].Cells[8].Value.ToString()).ToLongDateString();
                }
                else
                {
                    lblDatePrint.Visible = false;
                    lblDatePrint.Text = "";
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
            rowIDuse = e.RowIndex;
            idTabuse = Convert.ToInt32(dtGdVBlanqUseAll.Rows[rowIDuse].Cells[0].Value);
            lblDateUse.Text = Convert.ToDateTime(dtGdVBlanqUseAll.Rows[rowIDuse].Cells[1].Value.ToString()).ToLongDateString();
            lblFIO.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[2].Value.ToString();
            lblNameBlank.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[3].Value.ToString();
            lblAdress.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[4].Value.ToString();
            lblArrayNumber.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[5].Value.ToString();
            lblNumBlank.Text = dtGdVBlanqUseAll.Rows[rowIDuse].Cells[6].Value.ToString();
            if (dtGdVBlanqUseAll.Rows[rowIDuse].Cells[7].Value.ToString() == "1")
            {
                label13.Visible = true;
                lblDatePrint.Visible = true;
                lblDatePrint.Text = Convert.ToDateTime(dtGdVBlanqUseAll.Rows[rowIDuse].Cells[8].Value.ToString()).ToLongDateString();
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
            UpdSpisokFullUse();
        }

        #endregion

        #region Журнал учёта выдачи бланков
        private void DtGridViewIssueBlanq_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rowIDissue = e.RowIndex;
            idTabissue = Convert.ToInt32(dtGridViewIssueBlanq.Rows[rowIDissue].Cells[0].Value);
            dtTimePickerIssueDate.Value = Convert.ToDateTime(dtGridViewIssueBlanq.Rows[rowIDissue].Cells[1].Value.ToString());
            cmBxIssueFIO.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[2].Value.ToString();
            cmBxIssueNBlank.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[3].Value.ToString();
            txBxFirstNum.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[4].Value.ToString();
            txBxLastNum.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[5].Value.ToString();
        }

        private void UpdIssuanceBlanq()
        {
            dtGridViewIssueBlanq.DataSource = adminclsdb.GetIssuanceBlanq();
            if (dtGridViewIssueBlanq.RowCount > 0)
            {
                dtGridViewIssueBlanq.Columns[0].Visible = false;
                rowIDissue = 0;
                idTabissue = Convert.ToInt32(dtGridViewIssueBlanq.Rows[rowIDissue].Cells[0].Value);
                dtTimePickerIssueDate.Value = Convert.ToDateTime(dtGridViewIssueBlanq.Rows[rowIDissue].Cells[1].Value.ToString());
                cmBxIssueFIO.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[2].Value.ToString();
                cmBxIssueNBlank.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[3].Value.ToString();
                txBxFirstNum.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[4].Value.ToString();
                txBxLastNum.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[5].Value.ToString();
                
            }
            else
            {
                MessageBox.Show("В журнале нет ни одной записи.");
            }
            //PaintRowsUsers();
        }

        private void BtnAddIssuing_Click(object sender, EventArgs e)
        {
            editStatusIssue = false;
            dtTimePickerIssueDate.Value = DateTime.Now;
            cmBxIssueFIO.Text = "";
            cmBxIssueNBlank.Text = "";
            txBxFirstNum.Text = "";
            txBxLastNum.Text = "";
            EnableEditModeIssuingBlank();
        }
        private void BtnEditIssuing_Click(object sender, EventArgs e)
        {
            editStatusIssue = true;
            EnableEditModeIssuingBlank();
        }

        private void BtnDelIssuing_Click(object sender, EventArgs e)
        {
            if (adminclsdb.CheckingBlanqForUsing(cmBxIssueNBlank.Text, txBxFirstNum.Text, txBxLastNum.Text))
            {
                MessageBox.Show("Бланк с таким названием уже использовался, нельзя его удалить.");
            }
            else
            {
                adminclsdb.DeleteBlanq(cmBxIssueNBlank.Text, txBxFirstNum.Text, txBxLastNum.Text);
            }
        }

        private void BtnSaveIssuing_Click(object sender, EventArgs e)
        {
            if (dtTimePickerIssueDate.Text == "" || cmBxIssueFIO.Text == "" || cmBxIssueNBlank.Text == "" || txBxFirstNum.Text == "" || txBxLastNum.Text == "")
            {
                MessageBox.Show("Не должно быть пустых полей!");
            }
            else
            {
                SaveBlanq();
            }
        }

        /// <summary>
        /// Выполняется сохранение информации о выданных бланках
        /// </summary>
        private void SaveBlanq()
        {
            if (editStatusIssue)
            {
                adminclsdb.BlanqIssueEdit(idTabissue, dtTimePickerIssueDate.Text, cmBxIssueFIO.Text, cmBxIssueNBlank.Text, txBxFirstNum.Text, txBxLastNum.Text);
                editStatusIssue = false;
                UpdIssuanceBlanq();
                DisableEditModeIssuingBlank();
            }
            else
            {
                if (!adminclsdb.BlanqIssueControl(cmBxIssueNBlank.Text, txBxFirstNum.Text, txBxLastNum.Text))
                {
                    adminclsdb.BlanqIssueAdd(dtTimePickerIssueDate.Text, cmBxIssueFIO.Text, cmBxIssueNBlank.Text, txBxFirstNum.Text, txBxLastNum.Text);
                    editStatusIssue = false;
                    UpdIssuanceBlanq();
                    DisableEditModeIssuingBlank();
                }
                else
                {
                    MessageBox.Show("Бланки с таким названием и номерами уже выдавались!\nИзмените номера или название бланка", "Журнал учёта бланков и распорядительных документов суда");
                }
            }
        }

        private void BtnUndoIssuing_Click(object sender, EventArgs e)
        {
            dtTimePickerIssueDate.Value = Convert.ToDateTime(dtGridViewIssueBlanq.Rows[rowIDissue].Cells[1].Value.ToString());
            cmBxIssueFIO.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[2].Value.ToString();
            cmBxIssueNBlank.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[3].Value.ToString();
            txBxFirstNum.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[4].Value.ToString();
            txBxLastNum.Text = dtGridViewIssueBlanq.Rows[rowIDissue].Cells[5].Value.ToString();
            DisableEditModeIssuingBlank();
        }

        /// <summary>
        /// Отключение режима редактирования 
        /// </summary>
        private void DisableEditModeIssuingBlank()
        {
            dtTimePickerIssueDate.Enabled = false;
            cmBxIssueFIO.Enabled = false;
            cmBxIssueNBlank.Enabled = false;
            txBxFirstNum.Enabled = false;
            txBxLastNum.Enabled = false;
            dtGridViewIssueBlanq.Enabled = true;
            btnAddIssuing.Visible = true;
            btnEditIssuing.Visible = true;
            btnDelIssuing.Visible = true;
            btnSaveIssuing.Visible = false;
            btnUndoIssuing.Visible = false;
        }

        /// <summary>
        /// Включение режима редактирования 
        /// </summary>
        private void EnableEditModeIssuingBlank()
        {
            dtTimePickerIssueDate.Enabled = true;
            cmBxIssueFIO.Enabled = true;
            cmBxIssueNBlank.Enabled = true;
            txBxFirstNum.Enabled = true;
            txBxLastNum.Enabled = true;
            dtGridViewIssueBlanq.Enabled = false;
            btnAddIssuing.Visible = false;
            btnEditIssuing.Visible = false;
            btnDelIssuing.Visible = false;
            btnSaveIssuing.Visible = true;
            btnUndoIssuing.Visible = true;
        }

        private void TxBxSearchIssueDate_TextChanged(object sender, EventArgs e)
        {
            (dtGridViewIssueBlanq.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(DATEOFISSUE, System.String) LIKE '%" + (txBxSearchIssueDate.Text.Trim()) + "%' AND FIO LIKE '%" + txBxSearchIssueFIO.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchIssueNBlank.Text.Trim() + "%'"; // AND CONVERT(FIRST_BLANK, System.Int32) <= '" + Convert.ToInt32(txBxSearchIssueNum.Text.Trim()) + "' AND CONVERT(LAST_BLANK, System.Int32) >= '" + Convert.ToInt32(txBxSearchIssueNum.Text.Trim()) + "'";
        }

        private void TxBxSearchIssueFIO_TextChanged(object sender, EventArgs e)
        {
            (dtGridViewIssueBlanq.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(DATEOFISSUE, System.String) LIKE '%" + (txBxSearchIssueDate.Text.Trim()) + "%' AND FIO LIKE '%" + txBxSearchIssueFIO.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchIssueNBlank.Text.Trim() + "%'"; // AND FIRST_BLANK <= " + Convert.ToInt32(txBxSearchIssueNum.Text.Trim() ?? "0") + " AND LAST_BLANK >= " + Convert.ToInt32(txBxSearchIssueNum.Text.Trim() ?? "0"); //System.
            
        }

        private void TxBxSearchIssueNBlank_TextChanged(object sender, EventArgs e)
        {
            (dtGridViewIssueBlanq.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(DATEOFISSUE, System.String) LIKE '%" + (txBxSearchIssueDate.Text.Trim()) + "%' AND FIO LIKE '%" + txBxSearchIssueFIO.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchIssueNBlank.Text.Trim() + "%'"; // AND CONVERT(FIRST_BLANK, System.Int32) <= '" + Convert.ToInt32(txBxSearchIssueNum.Text.Trim()) + "' AND CONVERT(LAST_BLANK, System.Int32) >= '" + Convert.ToInt32(txBxSearchIssueNum.Text.Trim()) + "'";
        }

        private void TxBxSearchIssueNum_TextChanged(object sender, EventArgs e)
        {
            if (txBxSearchIssueNum.TextLength >= 3)
            {
                (dtGridViewIssueBlanq.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(DATEOFISSUE, System.String) LIKE '%" + txBxSearchIssueDate.Text.Trim() + "%' AND FIO LIKE '%" + txBxSearchIssueFIO.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchIssueNBlank.Text.Trim() + "%' AND CONVERT(FIRST_BLANK, System.Int32) <= '" + Convert.ToInt32(txBxSearchIssueNum.Text.Trim()) + "' AND CONVERT(LAST_BLANK, System.Int32) >= '" + Convert.ToInt32(txBxSearchIssueNum.Text.Trim()) + "'";
                //(dtGridViewIssueBlanq.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(FIRST_BLANK, System.Int32) <= '" + Convert.ToInt32(txBxSearchIssueNum.Text.Trim()) + "' AND CONVERT(LAST_BLANK, System.Int32) >= '" + Convert.ToInt32(txBxSearchIssueNum.Text.Trim()) + "'"; 
            }
            else
            {
                (dtGridViewIssueBlanq.DataSource as DataTable).DefaultView.RowFilter = "CONVERT(DATEOFISSUE, System.String) LIKE '%" + (txBxSearchIssueDate.Text.Trim()) + "%' AND FIO LIKE '%" + txBxSearchIssueFIO.Text.Trim() + "%' AND NAME_BLANK LIKE '%" + txBxSearchIssueNBlank.Text.Trim() + "%'";
            }
        }

        private void BtnSearchResetIssuing_Click(object sender, EventArgs e)
        {
            txBxSearchIssueDate.Text = "";
            txBxSearchIssueFIO.Text = "";
            txBxSearchIssueNBlank.Text = "";
            txBxSearchIssueNum.Text = "";
        }

        #endregion

        #region Настройки системы

        #region Список пользователей

        private void UpdSpisokUsers()
        {
            dtGridViewUsers.DataSource = adminclsdb.GetSpisokUsers();
            RowsUsersViev();
            PaintRowsUsers();
            if (dtGridViewUsers.RowCount > 0)
            {
                dtGridViewUsers.Columns[0].Visible = false;
                dtGridViewUsers.Columns[6].Visible = false;
                rowIDusers = 0;
                idTabusers = Convert.ToInt32(dtGridViewUsers.Rows[rowIDusers].Cells[0].Value);
                txBxUserFIO.Text = dtGridViewUsers.Rows[rowIDusers].Cells[1].Value.ToString();
                txBxUserOffice.Text = dtGridViewUsers.Rows[rowIDusers].Cells[2].Value.ToString();
                txBxUserLogin.Text = dtGridViewUsers.Rows[rowIDusers].Cells[3].Value.ToString();
                txBxUserPassword.Text = dtGridViewUsers.Rows[rowIDusers].Cells[4].Value.ToString();
                cmBxUserRight.Text = dtGridViewUsers.Rows[rowIDusers].Cells[5].Value.ToString();
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
            //PaintRowsUsers();
        }

        void PaintRowsUsers()
        {
            foreach (DataGridViewRow row in dtGridViewUsers.Rows)
            {
                if (row.Cells[6].Value.ToString() == "1")                
                    row.DefaultCellStyle.BackColor = Color.MistyRose;              
            }
        }

        private void dtGridViewUsers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PaintRowsUsers();
        }


        private void DtGridViewUsers_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rowIDusers = e.RowIndex;
            idTabusers = Convert.ToInt32(dtGridViewUsers.Rows[rowIDusers].Cells[0].Value);
            txBxUserFIO.Text = dtGridViewUsers.Rows[rowIDusers].Cells[1].Value.ToString();
            txBxUserOffice.Text = dtGridViewUsers.Rows[rowIDusers].Cells[2].Value.ToString();
            txBxUserLogin.Text = dtGridViewUsers.Rows[rowIDusers].Cells[3].Value.ToString();
            txBxUserPassword.Text = dtGridViewUsers.Rows[rowIDusers].Cells[4].Value.ToString();
            cmBxUserRight.Text = dtGridViewUsers.Rows[rowIDusers].Cells[5].Value.ToString();
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
            RowsUsersViev(); PaintRowsUsers();
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

            if (txBxUserFIO.Text == "" || txBxUserOffice.Text == "" || txBxUserLogin.Text == "" || txBxUserPassword.Text == "" || cmBxUserRight.Text == "")
            {
                MessageBox.Show("Не должно быть пустых полей!");
            }
            else if (adminclsdb.UserLoginControl(txBxUserLogin.Text) && !editStatusUsers)
            {
                MessageBox.Show("Логин должен быть уникальным!\nВведите другой Логин!");
            }
            else
            {
                if (adminclsdb.UserControl(txBxUserFIO.Text) && !editStatusUsers)
                {
                    DialogResult result = MessageBox.Show("Пользователь с такими Фамилией Именем и Отчеством уже зарегистрирован в системе!\nВы уверены, что хотите добавить пользователя с такими же данными", "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
                    if (result == DialogResult.Yes)
                    {
                        SaveUser();
                    }
                    else
                    {
                        MessageBox.Show("Введите другие Фамилию Имя и Отчество!");
                    }
                }
                else
                {
                    SaveUser();
                }
            }
        }

        /// <summary>
        /// Выполняется сохранение информации о пользователе
        /// </summary>
        private void SaveUser()
        {
            if (editStatusUsers)
            {
                adminclsdb.UserEdit(idTabusers, txBxUserFIO.Text, txBxUserOffice.Text, txBxUserLogin.Text, txBxUserPassword.Text, cmBxUserRight.Text);
            }
            else
            {
                adminclsdb.UserAdd(txBxUserFIO.Text, txBxUserOffice.Text, txBxUserLogin.Text, txBxUserPassword.Text, cmBxUserRight.Text);
            }
            UpdSpisokUsers();
            DisableEditModeUsers();
            editStatusUsers = false;
        }

        private void BtnUndoUser_Click(object sender, EventArgs e)
        {
            editStatusUsers = false;
            DisableEditModeUsers();
            idTabusers = Convert.ToInt32(dtGridViewUsers.Rows[rowIDusers].Cells[0].Value);
            txBxUserFIO.Text = dtGridViewUsers.Rows[rowIDusers].Cells[1].Value.ToString();
            txBxUserOffice.Text = dtGridViewUsers.Rows[rowIDusers].Cells[2].Value.ToString();
            txBxUserLogin.Text = dtGridViewUsers.Rows[rowIDusers].Cells[3].Value.ToString();
            txBxUserPassword.Text = dtGridViewUsers.Rows[rowIDusers].Cells[4].Value.ToString();
            cmBxUserRight.Text = dtGridViewUsers.Rows[rowIDusers].Cells[5].Value.ToString();
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
            editStatusUsers = false;
            EnableEditModeUsers();
            idTabusers = 0;
            txBxUserFIO.Text = "";
            txBxUserOffice.Text = "";
            txBxUserLogin.Text = "";
            txBxUserPassword.Text = "";
            cmBxUserRight.Text = "user";
        }

        private void BtnEditUser_Click(object sender, EventArgs e)
        {
            editStatusUsers = true;
            EnableEditModeUsers();
        }        
        private void BtnDelUser_Click(object sender, EventArgs e)
        {
            if (btnDelUser.Text == "Удалить")
            {
                adminclsdb.UserDeleted(idTabusers); 
                UpdSpisokUsers();
            }
            else if (btnDelUser.Text == "Восстановить")
            {
                adminclsdb.UserRecovery(idTabusers);
                UpdSpisokUsers();
            }

            
        }

        /// <summary>
        /// Отключение режима редактирования пользователей
        /// </summary>
        private void DisableEditModeUsers()
        {
            txBxUserLogin.Enabled = false; 
            txBxUserPassword.Enabled = false;
            cmBxUserRight.Enabled = false;
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
        private void EnableEditModeUsers()
        {
            txBxUserLogin.Enabled = true;
            txBxUserPassword.Enabled = true;
            cmBxUserRight.Enabled = true;
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

        private void UpdSpisokBlanq()
        {
            dtGridViewBlanq.DataSource = adminclsdb.GetSpisokBlanq();
            
            if (dtGridViewUsers.RowCount > 0)
            {
                rowIDblank = 0;
                idTabblank = Convert.ToInt32(dtGridViewBlanq.Rows[rowIDblank].Cells[0].Value);
                txBxNameBlank.Text = dtGridViewBlanq.Rows[rowIDblank].Cells[1].Value.ToString();
            }
            else
            {
                MessageBox.Show("В журнале нет ни одной записи.");
            }
        }

        
        private void DtGridViewBlanq_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rowIDblank = e.RowIndex;
            idTabblank = Convert.ToInt32(dtGridViewBlanq.Rows[rowIDblank].Cells[0].Value);
            txBxNameBlank.Text = dtGridViewBlanq.Rows[rowIDblank].Cells[1].Value.ToString();
        }
        private void BtnSaveBlank_Click(object sender, EventArgs e)
        {
            SaveBlank();
            DisableEditModeBlank();
            editStatusBlank = false;
        }

        private void SaveBlank()
        {
            if (editStatusBlank) // Сохраняем изменения
            {
                adminclsdb.BlanqSaveEdit(idTabblank, txBxNameBlank.Text);
            }
            else // Добавляем бланк
            {
                adminclsdb.BlanqSaveAdd(txBxNameBlank.Text);
            }
            UpdSpisokBlanq();
        }

        private void BtnUndoBlank_Click(object sender, EventArgs e)
        {
            txBxNameBlank.Text = dtGridViewBlanq.Rows[rowIDblank].Cells[1].Value.ToString();            

            DisableEditModeBlank();
            editStatusBlank = false;
        }

        private void BtnAddBlank_Click(object sender, EventArgs e)
        {
            editStatusBlank = false;
            EnableEditModeBlank();
            txBxNameBlank.Text = "";
        }

        private void BtnDelBlank_Click(object sender, EventArgs e)
        {
            if (adminclsdb.BlanqDeleted(Convert.ToInt32(dtGridViewBlanq.Rows[rowIDblank].Cells[0].Value), dtGridViewBlanq.Rows[rowIDblank].Cells[1].Value.ToString()))
            {
                MessageBox.Show("Запись удалена.");
            }
            else
            {
                MessageBox.Show("Не удалось удалить запись.");
            }
            UpdSpisokBlanq();
        }

        private void BtnEditBlank_Click(object sender, EventArgs e)
        {
            editStatusBlank = true;
            EnableEditModeBlank();
        }

        /// <summary>
        /// Отключение режима редактирования бланков
        /// </summary>
        private void DisableEditModeBlank()
        {
            txBxNameBlank.Enabled = false;

            dtGridViewBlanq.Enabled = true;
            btnSaveBlank.Visible = false;
            btnUndoBlank.Visible = false;
            btnAddBlank.Visible = true;
            btnDelBlank.Visible = true;
            btnEditBlank.Visible = true;
        }

        /// <summary>
        /// Включение режима редактирования бланков
        /// </summary>
        private void EnableEditModeBlank()
        {
            txBxNameBlank.Enabled = true;

            dtGridViewBlanq.Enabled = false;
            btnSaveBlank.Visible = true;
            btnUndoBlank.Visible = true;
            btnAddBlank.Visible = false;
            btnDelBlank.Visible = false;
            btnEditBlank.Visible = false;
        }

        private void dtGdVBlanqUseAll_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PaintRowsUse();
        }






        #endregion

        #endregion


    }
}
