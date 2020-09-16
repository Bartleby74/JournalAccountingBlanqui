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
        //PropsFields propsFields = new PropsFields(); //экземпляр класса с настройками
        Props props = new Props(); //экземпляр класса с настройками
        CLSDB clsdb = new CLSDB();
        public int idtab = 0;
        int rowID = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {
            EnableEditMode();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DisableEditMode();
            idtab = Convert.ToInt32(dtGdVBlanqUse.Rows[rowID].Cells[0].Value);
            dateTimePickerDispatch.Value = Convert.ToDateTime(dtGdVBlanqUse.Rows[rowID].Cells[1].Value);
            cmBxAdress.Text = dtGdVBlanqUse.Rows[rowID].Cells[2].Value.ToString();
            cmBxName.Text = dtGdVBlanqUse.Rows[rowID].Cells[3].Value.ToString();
            txBxN_Array.Text = dtGdVBlanqUse.Rows[rowID].Cells[4].Value.ToString();
            txBxNum.Text = dtGdVBlanqUse.Rows[rowID].Cells[5].Value.ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (idtab == 0)
            {
                if (dateTimePickerDispatch.Value == null | cmBxName.Text == "" | cmBxAdress.Text == "" | txBxN_Array.Text == "" | txBxNum.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }
                if (clsdb.GetUsedNomBlanq(cmBxName.Text, txBxNum.Text))
                {
                    MessageBox.Show("Бланк с таким номером уже добавлен в журнал.\nВведите другой номер или измените название бланка.");
                    return;
                }

                if (!clsdb.GetBlanqUser(props.Fields.UserID, cmBxName.Text, txBxNum.Text))
                {
                    MessageBox.Show("Бланк с таким номером не выдавался данному сотруднику. Введите другой номер.");
                    return;
                }

                if (clsdb.AddBlank(dateTimePickerDispatch.Value, props.Fields.UserID, cmBxAdress.Text, cmBxName.Text, txBxN_Array.Text, txBxNum.Text))
                {
                    rowID = 0;
                    dtGdVBlanqUse.Rows.Add(1);
                    dtGdVBlanqUse.Rows[rowID].Cells[0].Value = idtab;
                    dtGdVBlanqUse.Rows[rowID].Cells[1].Value = dateTimePickerDispatch.Value;
                    dtGdVBlanqUse.Rows[rowID].Cells[2].Value = cmBxAdress.Text;
                    dtGdVBlanqUse.Rows[rowID].Cells[3].Value = cmBxName.Text;
                    dtGdVBlanqUse.Rows[rowID].Cells[4].Value = txBxN_Array.Text;
                    dtGdVBlanqUse.Rows[rowID].Cells[5].Value = txBxNum.Text;
                }
            }
            else
            {
                if (dateTimePickerDispatch.Value == null | cmBxName.Text == "" | cmBxAdress.Text == "" | txBxN_Array.Text == "" | txBxNum.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }

                if (clsdb.GetPrintedNomBlanq(cmBxName.Text, txBxNum.Text) == "TRUE")
                {
                    MessageBox.Show("Невозможно внести изменения в журнал.\nИнформация о движении бланка была распечатана на бумажном носителе.\nВведите другой номер или измените название бланка.");
                    return;
                }

                if (!clsdb.GetBlanqUser(props.Fields.UserID, cmBxName.Text, txBxNum.Text))
                {
                    MessageBox.Show("Бланк с таким номером не выдавался данному сотруднику. Введите другой номер.");
                    return;
                }

                if (clsdb.GetUsedNomBlanq(cmBxName.Text, txBxNum.Text) && (txBxNum.Text != dtGdVBlanqUse.Rows[rowID].Cells[5].Value.ToString() || cmBxName.Text != dtGdVBlanqUse.Rows[rowID].Cells[3].Value.ToString()))
                {
                    MessageBox.Show("Бланк с таким номером уже добавлен в журнал.\nВведите другой номер или измените название бланка.");
                    return;
                }

                if (clsdb.BlanqSave(idtab, dateTimePickerDispatch.Value, cmBxName.Text, cmBxAdress.Text, txBxN_Array.Text, txBxNum.Text))
                {
                    dtGdVBlanqUse.Rows[rowID].Cells[0].Value = idtab;
                    dtGdVBlanqUse.Rows[rowID].Cells[1].Value = dateTimePickerDispatch.Value;
                    dtGdVBlanqUse.Rows[rowID].Cells[2].Value = cmBxAdress.Text;
                    dtGdVBlanqUse.Rows[rowID].Cells[3].Value = cmBxName.Text;
                    dtGdVBlanqUse.Rows[rowID].Cells[4].Value = txBxN_Array.Text;
                    dtGdVBlanqUse.Rows[rowID].Cells[5].Value = txBxNum.Text;
                }
            }

            DisableEditMode();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            EnableEditMode();
            idtab = 0;
            dateTimePickerDispatch.Value = DateTime.Now;
            cmBxAdress.Text = "";
            cmBxName.Text = "";
            txBxN_Array.Text = "";
            txBxNum.Text = "";
        }

        /// <summary>
        /// Щелчёк по строке в таблице DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtGdVBlanqUse_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rowID = e.RowIndex;
            idtab = Convert.ToInt32(dtGdVBlanqUse.Rows[rowID].Cells[0].Value);
            dateTimePickerDispatch.Value = Convert.ToDateTime(dtGdVBlanqUse.Rows[rowID].Cells[1].Value);
            cmBxAdress.Text = dtGdVBlanqUse.Rows[rowID].Cells[2].Value.ToString();
            cmBxName.Text = dtGdVBlanqUse.Rows[rowID].Cells[3].Value.ToString();
            txBxN_Array.Text = dtGdVBlanqUse.Rows[rowID].Cells[4].Value.ToString();
            txBxNum.Text = dtGdVBlanqUse.Rows[rowID].Cells[5].Value.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            props.ReadXml();
            using (Authorization Form = new Authorization())
            {
                Form.ShowDialog();
            }

            btnCancel.Visible = false;
            btnSave.Visible = false;
            SetAutoComplete();

            lblFIO.Text = props.Fields.UserFIO;
            if (props.Fields.UserStatus == "admin")
            {
                btnAdm.Visible = true;
            }

            UpdSpisok();
        }

        private void UpdSpisok()
        {
            dtGdVBlanqUse.DataSource = clsdb.UpdSpisokFullUser(props.Fields.UserID);

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

        private void UpdSearchSpisok()
        {
            dtGdVBlanqUse.DataSource = clsdb.UpdSpisokSearchUser(props.Fields.UserID, txBxSearchDate.Text, txBxSearchAdress.Text, txBxSearchName.Text, txBxSearchNArray.Text, txBxSearchNum.Text);

            if (dtGdVBlanqUse.RowCount > 0)
            {
                dtGdVBlanqUse.Columns[6].Visible = false;
                rowID = 0;
                idtab = Convert.ToInt32(dtGdVBlanqUse.Rows[rowID].Cells[0].Value);
                dateTimePickerDispatch.Value = Convert.ToDateTime(dtGdVBlanqUse.Rows[rowID].Cells[1].Value.ToString());
                cmBxAdress.Text = dtGdVBlanqUse.Rows[rowID].Cells[2].Value.ToString();
                cmBxName.Text = dtGdVBlanqUse.Rows[rowID].Cells[3].Value.ToString();
                txBxN_Array.Text = dtGdVBlanqUse.Rows[rowID].Cells[4].Value.ToString();
                txBxNum.Text = dtGdVBlanqUse.Rows[rowID].Cells[5].Value.ToString();
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
            UpdSearchSpisok();
        }

        private void TxBxSearchNArray_TextChanged(object sender, EventArgs e)
        {
            UpdSearchSpisok();
        }

        private void TxBxSearchName_TextChanged(object sender, EventArgs e)
        {
            UpdSearchSpisok();
        }

        private void TxBxSearchAdress_TextChanged(object sender, EventArgs e)
        {
            UpdSearchSpisok();
        }

        private void TxBxSearchDate_TextChanged(object sender, EventArgs e)
        {
            UpdSearchSpisok();
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

        /// <summary>
        /// Отключение режима редактирования
        /// </summary>
        private void DisableEditMode()
        {
            btnCancel.Visible = false;
            btnSave.Visible = false;
            btnPrint.Visible = true;
            btnModify.Visible = true;
            btnAdd.Visible = true;
            dateTimePickerDispatch.Enabled = false;
            txBxN_Array.Enabled = false;
            txBxNum.Enabled = false;
            cmBxName.Enabled = false;
            cmBxAdress.Enabled = false;
            dtGdVBlanqUse.Enabled = true;
            txBxSearchDate.Enabled = true;
            txBxSearchAdress.Enabled = true;
            txBxSearchName.Enabled = true;
            txBxSearchNArray.Enabled = true;
            txBxSearchNum.Enabled = true;
            btnReset.Enabled = true;
        }

        /// <summary>
        /// Включение режима редактирования
        /// </summary>
        private void EnableEditMode()
        {
            btnCancel.Visible = true;
            btnSave.Visible = true;
            btnPrint.Visible = false;
            btnModify.Visible = false;
            btnAdd.Visible = false;
            dateTimePickerDispatch.Enabled = true;
            txBxN_Array.Enabled = true;
            txBxNum.Enabled = true;
            cmBxName.Enabled = true;
            cmBxAdress.Enabled = true;
            dtGdVBlanqUse.Enabled = false;
            txBxSearchDate.Enabled = false;
            txBxSearchAdress.Enabled = false;
            txBxSearchName.Enabled = false;
            txBxSearchNArray.Enabled = false;
            txBxSearchNum.Enabled = false;
            btnReset.Enabled = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы действительно желаете закрыть приложение?", "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OKCancel) == DialogResult.OK)
                e.Cancel = false;
            else e.Cancel = true;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            cmBxAdress.SelectionLength = 0;
        }
    }
}
