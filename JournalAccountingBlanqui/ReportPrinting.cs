using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassWord;

namespace JournalAccountingBlanqui
{
    class ReportPrinting
    {
        Props props = new Props(); //экземпляр класса с настройками
        CLSDB clsdb = new CLSDB();
        DataSet dataset = new DataSet();
        string statusPrint = "FALSE";

        //private string file = "Журнал учёта бланков строгой отчётности";
        private string PathToTemplate
        {
            get
            {
                return Application.StartupPath + "\\Журнал учёта бланков и распорядительных документов.dot";
            }
        }

        public void Printing(int userID)
        {
            dataset = clsdb.GetPrint(userID);
            if (dataset.Tables.Count == 0)
            {
                MessageBox.Show("Ошибка, результат не содежит строк");
            }
            else
            {
                WordDocument wordDocument;
                try
                {
                    wordDocument = new WordDocument(this.PathToTemplate);
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show("Ошибка при открытии шаблона Word. Подробности " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                wordDocument.Visible = true;

                int rowIndex = 2, i = 1, idjournal = 0;
                DataTable table = dataset.Tables[0];

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        wordDocument.SelectTable(1);
                        if (rowIndex >= 3) wordDocument.AddRowToTable();
                        ++rowIndex;
                        
                        idjournal = Convert.ToInt32(row[0]);
                        
                        wordDocument.SetSelectionToCell(rowIndex, 1);
                        wordDocument.Selection.Text = i.ToString();
                        wordDocument.Selection.Aligment = TextAligment.Center;
                        i++;
                        wordDocument.SetSelectionToCell(rowIndex, 2);
                        wordDocument.Selection.Text = DateTime.Parse(row[1].ToString()).ToShortDateString();
                        wordDocument.Selection.Aligment = TextAligment.Center;
                        wordDocument.SetSelectionToCell(rowIndex, 3);
                        wordDocument.Selection.Text = row[2].ToString();
                        wordDocument.Selection.Aligment = TextAligment.Left;
                        wordDocument.SetSelectionToCell(rowIndex, 4);
                        wordDocument.Selection.Text = row[3].ToString();
                        wordDocument.Selection.Aligment = TextAligment.Left;
                        wordDocument.SetSelectionToCell(rowIndex, 5);
                        wordDocument.Selection.Text = row[4].ToString();
                        wordDocument.Selection.Aligment = TextAligment.Center;
                        wordDocument.SetSelectionToCell(rowIndex, 6);
                        wordDocument.Selection.Text = row[5].ToString();
                        wordDocument.Selection.Aligment = TextAligment.Center;

                        clsdb.SetPrint(idjournal, DateTime.Now);
                    }
                    
                }
                wordDocument.SetSelectionToBookmark("fio");
                wordDocument.Selection.Text = ShortName(props.Fields.UserFIO).ToString();
                wordDocument.Selection.Aligment = TextAligment.Center;
                wordDocument.SetSelectionToBookmark("dolg");
                wordDocument.Selection.Text = props.Fields.UserOffice;
                wordDocument.Selection.Aligment = TextAligment.Center;
                
                MessageBox.Show("Лист(ы) журнала сформирован(ы).\nСоответствующие отметки внесены в базу данных.\nЕсли захотите распечатать сформированные страницы в будущем,\nсохраните документ");
                
            }
        }

        private object ShortName(string userFIO)
        {
            string[] str = userFIO.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (str.Length != 3) throw new ArgumentException("Ф.И.О. задано в неверном формате");
            return string.Format("{0} {1}.{2}.", str[0], str[1][0], str[2][0]);
        }
    }
}
