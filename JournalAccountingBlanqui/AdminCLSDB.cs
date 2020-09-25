using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace JournalAccountingBlanqui
{
    class AdminCLSDB
    {
        Props props = new Props(); //экземпляр класса с настройками
        PropsFields propsFields = new PropsFields(); //экземпляр класса с настройками 
        DataTable bindingSource = new DataTable();
        //DataSet dataSet;

        private string pathSaveSetting { get { return Application.StartupPath + "\\Setting.xml"; } }
        static string ConnectFileBase = Application.StartupPath;

        /// <summary>
        /// Возвращает список использованных бланков
        /// </summary>
        /// <returns></returns>
        internal DataTable UpdSpisokFullUser()
        {
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                //Запрос обновлён
                FbCommand command = new FbCommand(@"SELECT JOURNAL_OF_USE.ID, JOURNAL_OF_USE.DATE_USE, USERS.FIO, NAME_BLANKS.NAME_BLANK, DESTINATION_ADRESS.ADRESS, JOURNAL_OF_USE.ARRAY_NUMBER, JOURNAL_OF_USE.NUM_BLANK, JOURNAL_OF_USE.PRINTED, JOURNAL_OF_USE.DATE_PRINT FROM JOURNAL_OF_USE INNER JOIN NAME_BLANKS ON (JOURNAL_OF_USE.ID_NAME_BLANK = NAME_BLANKS.ID) INNER JOIN DESTINATION_ADRESS ON (JOURNAL_OF_USE.ID_ADRESS = DESTINATION_ADRESS.ID) INNER JOIN USERS ON (JOURNAL_OF_USE.ID_FIO = USERS.ID) ORDER BY JOURNAL_OF_USE.ID DESC", con);
                
                FbDataAdapter adapter = new FbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                con.Close();
                if (dataset.Tables.Count == 0)
                {
                    MessageBox.Show("Ошибка, результат не содежит строк");
                }
                bindingSource = dataset.Tables[0];
                return bindingSource;
            }
        }

        /// <summary>
        /// Возвращает список пользователей
        /// </summary>
        /// <returns></returns>
        internal DataTable UpdSpisokUsers()
        {
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                //Запрос обновлён
                FbCommand command = new FbCommand(@"SELECT USERS.ID, USERS.FIO, USERS.OFFICE, USERS.LOGIN, USERS.PASSW, USERS.RIGHTS, USERS.DELETED FROM USERS", con);
                
                FbDataAdapter adapter = new FbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                con.Close();
                if (dataset.Tables.Count == 0)
                {
                    MessageBox.Show("Ошибка, результат не содежит строк");
                }
                bindingSource = dataset.Tables[0];
                return bindingSource;
            }           
        }

        /// <summary>
        /// Возвращает список бланков
        /// </summary>
        /// <returns></returns>
        internal DataTable UpdSpisokBlanq()
        {
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                //Запрос обновлён
                FbCommand command = new FbCommand(@"SELECT NAME_BLANKS.ID, NAME_BLANKS.NAME_BLANK FROM NAME_BLANKS", con);
                
                FbDataAdapter adapter = new FbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                con.Close();
                if (dataset.Tables.Count == 0)
                {
                    MessageBox.Show("Ошибка, результат не содежит строк");
                }
                bindingSource = dataset.Tables[0];
                return bindingSource;
            }           
        }
















        #region Заимствованный код

        

        #endregion
    }
}
