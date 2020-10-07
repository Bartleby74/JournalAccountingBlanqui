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


        private string pathSaveSetting { get { return Application.StartupPath + "\\Setting.xml"; } }
        static string ConnectFileBase = Application.StartupPath;





        #region Для работы с вкладкой Список использованных бланков

        /// <summary>
        /// Возвращает список использованных бланков
        /// </summary>
        /// <returns></returns>
        internal DataTable GetSpisokFullUse()
        {
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                DataTable dataTableSpisokFullUser = new DataTable();
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
                dataTableSpisokFullUser = dataset.Tables[0];
                return dataTableSpisokFullUser;
            }
        }



        #endregion


        #region Для работы с вкладкой Журнал учёта выдачи бланков

        /// <summary>
        /// Формирует список названий бланков для автозаполнения поля с названием бланков
        /// </summary>
        /// <returns>Список названий бланков в оболочке AutoCompleteStringCollection</returns>
        internal AutoCompleteStringCollection UpdAutoComplName()
        {
            AutoCompleteStringCollection list = new AutoCompleteStringCollection();
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {   //Запрос обновлён
                con.Open();
                FbCommand command = new FbCommand(@"SELECT DISTINCT NAME_BLANKS.NAME_BLANK FROM NAME_BLANKS ORDER BY NAME_BLANKS.NAME_BLANK", con);
                FbDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        list.Insert(i, dr[i].ToString());
                    }
                }
                con.Close();
            }
            return list;
        }

        /// <summary>
        /// Формирует список названий бланков для автозаполнения поля с названием бланков
        /// </summary>
        /// <returns>Список названий бланков в оболочке AutoCompleteStringCollection</returns>
        internal AutoCompleteStringCollection UpdAutoComplUser()
        {
            AutoCompleteStringCollection list = new AutoCompleteStringCollection();
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {   //Запрос обновлён
                con.Open();
                FbCommand command = new FbCommand(@"SELECT DISTINCT USERS.FIO FROM USERS WHERE USERS.DELETED = 0 ORDER BY USERS.ID", con);
                FbDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        list.Insert(i, dr[i].ToString());
                    }
                }
                con.Close();
            }
            return list;
        }



        /// <summary>
        /// Возвращает список бланков выданных пользователям
        /// </summary>
        /// <returns></returns>
        internal DataTable GetIssuanceBlanq()
        {
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                DataTable dataTableIssuanceBlanq = new DataTable();
                //Запрос обновлён
                FbCommand command = new FbCommand(@"SELECT JOURNAL_ISSUANCE.ID, JOURNAL_ISSUANCE.DATEOFISSUE, USERS.FIO, NAME_BLANKS.NAME_BLANK, JOURNAL_ISSUANCE.FIRST_BLANK, JOURNAL_ISSUANCE.LAST_BLANK FROM JOURNAL_ISSUANCE INNER JOIN NAME_BLANKS ON (JOURNAL_ISSUANCE.ID_NAME_BLANK = NAME_BLANKS.ID) INNER JOIN USERS ON (JOURNAL_ISSUANCE.ID_FIO = USERS.ID) ORDER BY JOURNAL_ISSUANCE.DATEOFISSUE DESC", con);
                FbDataAdapter adapter = new FbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                con.Close();
                if (dataset.Tables.Count == 0)
                {
                    MessageBox.Show("Ошибка, результат не содежит строк");
                }
                dataTableIssuanceBlanq = dataset.Tables[0];
                return dataTableIssuanceBlanq;
            }
        }

        /// <summary>
        /// Возвращает true если бланки с таким названием уже использовались
        /// </summary>
        /// <param name="nameBlanq">Название бланка</param>
        /// <param name="firstBlanq">Название бланка</param>
        /// <param name="lastBlanq">Название бланка</param>
        /// <returns></returns>
        public bool CheckingBlanqForUsing(string nameBlanq, string firstBlanq, string lastBlanq)
        {
            bool p = false;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT COUNT(*) FROM JOURNAL_OF_USE INNER JOIN NAME_BLANKS ON (JOURNAL_OF_USE.ID_NAME_BLANK = NAME_BLANKS.ID) INNER JOIN JOURNAL_ISSUANCE ON (NAME_BLANKS.ID = JOURNAL_ISSUANCE.ID_NAME_BLANK) WHERE NAME_BLANKS.NAME_BLANK = @NAME_BLANK AND JOURNAL_OF_USE.NUM_BLANK >= @FIRST_BLANK AND JOURNAL_OF_USE.NUM_BLANK <= @LAST_BLANK", con);
                    command.Parameters.Add("@NAME_BLANK", FbDbType.VarChar).Value = nameBlanq;
                    command.Parameters.Add("@FIRST_BLANK", FbDbType.VarChar).Value = firstBlanq;
                    command.Parameters.Add("@LAST_BLANK", FbDbType.VarChar).Value = lastBlanq;


                    if ((int)command.ExecuteScalar() == 0)
                    {
                        p = false;
                    }
                    else
                    {
                        p = true;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось выполнить запрос.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                con.Close();
            }
            return p;
        }

        /// <summary>
        /// Удаляет из базы запись о выдаче бланков пользователю
        /// </summary>
        /// <param name="nameBlanq">Название бланка</param>
        /// <param name="firstBlanq">Начальный номер бланков</param>
        /// <param name="lastBlanq">Конечный номер бланков</param>
        /// <returns></returns>
        public bool DeleteBlanq(string nameBlanq, string firstBlanq, string lastBlanq)
        {
            bool p = false;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"DELETE FROM JOURNAL_ISSUANCE INNER JOIN JOURNAL_ISSUANCE ON (NAME_BLANKS.ID = JOURNAL_ISSUANCE.ID_NAME_BLANK) WHERE NAME_BLANKS.NAME_BLANK = @NAME_BLANK AND JOURNAL_OF_USE.NUM_BLANK >= @FIRST_BLANK AND JOURNAL_OF_USE.NUM_BLANK <= @LAST_BLANK", con);
                    command.Parameters.Add("@NAME_BLANK", FbDbType.VarChar).Value = nameBlanq;
                    command.Parameters.Add("@FIRST_BLANK", FbDbType.VarChar).Value = firstBlanq;
                    command.Parameters.Add("@LAST_BLANK", FbDbType.VarChar).Value = lastBlanq;

                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                    MessageBox.Show("Запись о выданных бланках удалена!");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось выполнить запрос.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                con.Close();
            }
            return p;
        }

        /// <summary>
        /// Возвращает true если бланки с таким названием и номерами выдавались ранее
        /// </summary>
        /// <param name="nameBlanq">Название бланка</param>
        /// <param name="firstBlanq">Начальный номер бланков</param>
        /// <param name="lastBlanq">Конечный номер бланков</param>
        /// <returns></returns>
        public bool BlanqIssueControl(string nameBlanq, string firstBlanq, string lastBlanq)
        {
            bool p = false;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT COUNT(*) FROM JOURNAL_ISSUANCE INNER JOIN NAME_BLANKS ON (JOURNAL_ISSUANCE.ID_NAME_BLANK = NAME_BLANKS.ID) WHERE NAME_BLANKS.NAME_BLANK = @NAME_BLANK AND ((JOURNAL_ISSUANCE.FIRST_BLANK <= @FIRST_BLANK AND JOURNAL_ISSUANCE.LAST_BLANK >= @FIRST_BLANK) OR (JOURNAL_ISSUANCE.FIRST_BLANK <= @LAST_BLANK AND JOURNAL_ISSUANCE.LAST_BLANK >= @LAST_BLANK))", con);
                    command.Parameters.Add("@NAME_BLANK", FbDbType.VarChar).Value = nameBlanq;
                    command.Parameters.Add("@FIRST_BLANK", FbDbType.VarChar).Value = firstBlanq;
                    command.Parameters.Add("@LAST_BLANK", FbDbType.VarChar).Value = lastBlanq;


                    if ((int)command.ExecuteScalar() == 0)
                    {
                        p = false;
                    }
                    else
                    {
                        p = true;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось выполнить запрос.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                con.Close();
            }
            return p;
        }

        /// <summary>
        /// Редактируетзапись о выдаче бланка пользователю в базу данных
        /// </summary>
        /// <param name="idIssue">ID записи</param>
        /// <param name="dateIssue">Дата выдачи</param>
        /// <param name="userFIO">Ф.И.О. пользователя</param>
        /// <param name="nameBlanq">Название бланка</param>
        /// <param name="firstBlanq">Начальный номер бланков</param>
        /// <param name="lastBlanq">Конечный номер бланков</param>
        /// <returns></returns>
        public bool BlanqIssueEdit(int idIssue, string dateIssue, string userFIO, string nameBlanq, string firstBlanq, string lastBlanq)
        {
            bool p = false;

            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"UPDATE JOURNAL_ISSUANCE SET JOURNAL_ISSUANCE.DATEOFISSUE = @DATEOFISSUE, JOURNAL_ISSUANCE.ID_FIO = (SELECT USERS.ID FROM USERS WHERE USERS.FIO = @FIO), JOURNAL_ISSUANCE.ID_NAME_BLANK = (SELECT NAME_BLANKS.ID FROM NAME_BLANKS WHERE NAME_BLANKS.NAME_BLANK = @NAME_BLANK), JOURNAL_ISSUANCE.FIRST_BLANK = @FIRST_BLANK, JOURNAL_ISSUANCE.LAST_BLANK = @LAST_BLANK WHERE JOURNAL_ISSUANCE.ID = @ID", con);
                    command.Parameters.Add("@ID", FbDbType.Integer).Value = idIssue;
                    command.Parameters.Add("@DATEOFISSUE", FbDbType.VarChar).Value = dateIssue;
                    command.Parameters.Add("@FIO", FbDbType.VarChar).Value = userFIO;
                    command.Parameters.Add("@NAME_BLANK", FbDbType.VarChar).Value = nameBlanq;
                    command.Parameters.Add("@FIRST_BLANK", FbDbType.VarChar).Value = firstBlanq;
                    command.Parameters.Add("@LAST_BLANK", FbDbType.VarChar).Value = lastBlanq;

                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    p = false;
                }
            }
            return p;
        }

        /// <summary>
        /// Добавляет запись о выдаче бланка пользователю в базу данных
        /// </summary>
        /// <param name="dateIssue">Дата выдачи</param>
        /// <param name="userFIO">Ф.И.О. пользователя</param>
        /// <param name="nameBlanq">Название бланка</param>
        /// <param name="firstBlanq">Начальный номер бланков</param>
        /// <param name="lastBlanq">Конечный номер бланков</param>
        /// <returns></returns>
        public bool BlanqIssueAdd(string dateIssue, string userFIO, string nameBlanq, string firstBlanq, string lastBlanq)
        {
            bool p = false;
            int idIssue = ReturnMaxIdIssue() + 1;

            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"INSERT INTO JOURNAL_ISSUANCE(JOURNAL_ISSUANCE.ID, JOURNAL_ISSUANCE.DATEOFISSUE, JOURNAL_ISSUANCE.ID_FIO, JOURNAL_ISSUANCE.ID_NAME_BLANK, JOURNAL_ISSUANCE.FIRST_BLANK, JOURNAL_ISSUANCE.LAST_BLANK) VALUES(@ID, @DATEOFISSUE, (SELECT USERS.ID FROM USERS WHERE USERS.FIO = @FIO), (SELECT NAME_BLANKS.ID FROM NAME_BLANKS WHERE NAME_BLANKS.NAME_BLANK = @NAME_BLANK), @FIRST_BLANK, @LAST_BLANK)", con);
                    command.Parameters.Add("@ID", FbDbType.Integer).Value = idIssue;
                    command.Parameters.Add("@DATEOFISSUE", FbDbType.VarChar).Value =dateIssue ;
                    command.Parameters.Add("@FIO", FbDbType.VarChar).Value = userFIO;
                    command.Parameters.Add("@NAME_BLANK", FbDbType.VarChar).Value = nameBlanq;
                    command.Parameters.Add("@FIRST_BLANK", FbDbType.VarChar).Value = firstBlanq;
                    command.Parameters.Add("@LAST_BLANK", FbDbType.VarChar).Value = lastBlanq;

                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    p = false;
                }
            }
            return p;
        }

        /// <summary>
        /// Возвращает максимальное значение поля ID 
        /// </summary>
        /// <returns></returns>
        private int ReturnMaxIdIssue()
        {
            int idIssue = 0;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT MAX(JOURNAL_ISSUANCE.ID) FROM JOURNAL_ISSUANCE", con);
                    idIssue = Convert.ToInt32(command.ExecuteScalar().ToString());
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось получить максимальное значение ID.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            return idIssue;
        }

        #endregion


        #region Для работы с вкладкой Настройки системы

        #region Пользователи

        /// <summary>
        /// Возвращает список пользователей
        /// </summary>
        /// <returns></returns>
        internal DataTable GetSpisokUsers()
        {
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                DataTable dataTableSpisokUser = new DataTable();
                //Запрос обновлён
                FbCommand command = new FbCommand(@"SELECT USERS.ID, USERS.FIO, USERS.OFFICE, USERS.LOGIN, USERS.PASSW, USERS.RIGHTS, USERS.DELETED FROM USERS ORDER BY USERS.ID", con);
                
                FbDataAdapter adapter = new FbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                con.Close();
                if (dataset.Tables.Count == 0)
                {
                    MessageBox.Show("Ошибка, результат не содежит строк");
                }
                dataTableSpisokUser = dataset.Tables[0];
                return dataTableSpisokUser;
            }           
        }

        /// <summary>
        /// Удаляет пользователя из базы данных
        /// </summary>
        /// <param name="idTabUser">ID бланка</param>
        /// <returns>true если удалилось, иначе false</returns>
        internal bool UserDeleted(int idTabUser)
        {
            bool p = false;
            if (CheckingUserForUsing(idTabUser))
            {
                using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        FbCommand command = new FbCommand(@"UPDATE USERS SET USERS.DELETED = '1' WHERE USERS.ID = @IDUSER", con);
                        command.Parameters.Add("@IDUSER", FbDbType.Integer).Value = idTabUser;
                        
                        command.ExecuteScalar();

                        MessageBox.Show("Пользователь удалён логически!");
                        p = true;
                        con.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        p = false;
                    }
                }
            }
            else
            {
                using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        FbCommand command = new FbCommand(@"DELETE FROM USERS WHERE USERS.ID = @IDUSER", con);
                        command.Parameters.Add("@IDUSER", FbDbType.Integer).Value = idTabUser;
                        
                        command.ExecuteScalar();

                        p = true;
                        con.Close();
                        MessageBox.Show("Пользователь удалён!");
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Не удалось удалить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        p = false;
                    }
                }
            }
            return p;
        }



        /// <summary>
        /// Восстанавливает пользователя, изменяет признак логического удаления на 0
        /// </summary>
        /// <param name="idTabUser">ID бланка</param>
        /// <returns>true если удалилось, иначе false</returns>
        internal bool UserRecovery(int idTabUser)
        {
            bool p = false;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"UPDATE USERS SET USERS.DELETED = '0' WHERE USERS.ID = @IDUSER", con);
                    command.Parameters.Add("@IDUSER", FbDbType.Integer).Value = idTabUser;
                        
                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    p = false;
                }
            }

            return p;
        }




        /// <summary>
        /// Возвращает true если имеются записи о выдаче бланков пользователю
        /// </summary>
        /// <param name="idTabUser">ID пользователя</param>
        /// <returns></returns>
        private bool CheckingUserForUsing(int idTabUser)
        {
            bool p = false;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT COUNT(*) FROM JOURNAL_ISSUANCE WHERE JOURNAL_ISSUANCE.ID_FIO = @IDUSER", con);
                    command.Parameters.Add("@IDUSER", FbDbType.VarChar).Value = idTabUser;


                    if ((int)command.ExecuteScalar() == 0)
                    {
                        p = false;
                    }
                    else
                    {
                        p = true;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось выполнить запрос.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                con.Close();
            }
            return p;
        }

        /// <summary>
        /// Добавляет запись пользователя в базу
        /// </summary>
        /// <param name="fioUser">Ф.И.О. пользователя </param>
        /// <param name="officeUser">Должность пользователя</param>
        /// <param name="loginUser">Логин</param>
        /// <param name="passwfioUser">Пароль</param>
        /// <param name="rightsUser">Права пользователя в системе. По умолчанию "user"</param>
        /// <returns>true если сохранилось, иначе false</returns>
        internal bool UserAdd(string fioUser, string officeUser, string loginUser, string passwfioUser, string rightsUser)// <returns>
        {
            bool p = false;
            int idUser = ReturnMaxIdUser() + 1;

            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"INSERT INTO USERS(USERS.ID, USERS.FIO, USERS.OFFICE, USERS.LOGIN, USERS.PASSW, USERS.RIGHTS, USERS.DELETED) VALUES(@ID, @FIO, @OFFICE, @LOGIN, @PASSW, @RIGHTS, @DELETED)", con);
                    command.Parameters.Add("@ID", FbDbType.Integer).Value = idUser;
                    command.Parameters.Add("@FIO", FbDbType.VarChar).Value = fioUser;
                    command.Parameters.Add("@OFFICE", FbDbType.VarChar).Value = officeUser;
                    command.Parameters.Add("@LOGIN", FbDbType.VarChar).Value = loginUser;
                    command.Parameters.Add("@PASSW", FbDbType.VarChar).Value = passwfioUser;
                    command.Parameters.Add("@RIGHTS", FbDbType.VarChar).Value = rightsUser;
                    command.Parameters.Add("@DELETED", FbDbType.Integer).Value = 0;

                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    p = false;
                }
            }
            return p;
        }

        /// <summary>
        /// Редактирует запись пользователя в базе
        /// </summary>
        /// <param name="idUser">ID пользователя</param>
        /// <param name="fioUser">Ф.И.О. пользователя </param>
        /// <param name="officeUser">Должность пользователя</param>
        /// <param name="loginUser">Логин</param>
        /// <param name="passwfioUser">Пароль</param>
        /// <param name="rightsUser">Права пользователя в системе. По умолчанию "user"</param>
        /// <returns>true если сохранилось, иначе false</returns>
        internal bool UserEdit(int idUser, string fioUser, string officeUser, string loginUser, string passwfioUser, string rightsUser)
        {
            bool p = false;
            
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"UPDATE USERS SET USERS.FIO = @FIO, USERS.OFFICE = @OFFICE, USERS.LOGIN = @LOGIN, USERS.PASSW = @PASSW, USERS.RIGHTS = @RIGHTS WHERE USERS.ID = @ID", con);
                    command.Parameters.Add("@ID", FbDbType.Integer).Value = idUser;
                    command.Parameters.Add("@FIO", FbDbType.VarChar).Value = fioUser;
                    command.Parameters.Add("@OFFICE", FbDbType.VarChar).Value = officeUser;
                    command.Parameters.Add("@LOGIN", FbDbType.VarChar).Value = loginUser;
                    command.Parameters.Add("@PASSW", FbDbType.VarChar).Value = passwfioUser;
                    command.Parameters.Add("@RIGHTS", FbDbType.VarChar).Value = rightsUser;
                    //command.Parameters.Add("@DELETED", FbDbType.Integer).Value = 0;

                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    p = false;
                }
            }
            return p;
        }

        /// <summary>
        /// Возвращает максимальный ID из таблицы пользователей
        /// </summary>
        /// <returns></returns>
        private int ReturnMaxIdUser()
        {
            int idadress = 0;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT MAX(USERS.ID) FROM USERS", con);
                    idadress = Convert.ToInt32(command.ExecuteScalar().ToString());
                    //FbDataReader dr = command.ExecuteReader();

                    //dr.Read();
                    //idadress = dr.GetInt32(0);
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось получить максимальное значение ID.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            return idadress;
        }

        /// <summary>
        /// Проверяет - есть ли пользователь с таким именем в базе
        /// </summary>
        /// <param name="userFIO">Ф.И.О. ползователя</param>
        /// <returns></returns>
        internal bool UserControl(string userFIO)
        {
            bool p = false;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT COUNT(*) FROM USERS WHERE USERS.FIO = @USERFIO", con);
                    command.Parameters.Add("@USERFIO", FbDbType.VarChar).Value = userFIO;


                    if ((int)command.ExecuteScalar() == 0)
                    {
                        p = false;
                    }
                    else
                    {
                        p = true;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось выполнить запрос.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                con.Close();
            }
            return p;
        }

        /// <summary>
        /// Проверяет - есть ли пользователь с таким Логином в базе
        /// </summary>
        /// <param name="login">Ф.И.О. ползователя</param>
        /// <returns></returns>
        internal bool UserLoginControl(string login)
        {
            bool p = false;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT COUNT(*) FROM USERS WHERE USERS.LOGIN = @LOGIN", con);
                    command.Parameters.Add("@LOGIN", FbDbType.VarChar).Value = login;


                    if ((int)command.ExecuteScalar() == 0)
                    {
                        p = false;
                    }
                    else
                    {
                        p = true;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось выполнить запрос.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                con.Close();
            }
            return p;
        }



        #endregion

        #region Бланки

        /// <summary>
        /// Возвращает список бланков 
        /// </summary>
        /// <returns></returns>
        internal DataTable GetSpisokBlanq()
        {
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                DataTable dataTableSpisokBlanq = new DataTable();
                //Запрос обновлён
                FbCommand command = new FbCommand(@"SELECT NAME_BLANKS.ID, NAME_BLANKS.NAME_BLANK FROM NAME_BLANKS ORDER BY NAME_BLANKS.ID", con);
                FbDataAdapter adapter = new FbDataAdapter(command);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                con.Close();
                if (dataset.Tables.Count == 0)
                {
                    MessageBox.Show("Ошибка, результат не содежит строк");
                }
                dataTableSpisokBlanq = dataset.Tables[0];
                return dataTableSpisokBlanq;
            }
        }

        /// <summary>
        /// Удаляет бланк из базы данных
        /// </summary>
        /// <param name="idTabblank">ID бланка</param>
        /// <param name="nameBlank">Название бланка</param>
        /// <returns>true если удалилось, иначе false</returns>
        internal bool BlanqDeleted(int idTabblank, string nameBlank)
        {
            bool p = false;
            if (CheckingBlankForUse(idTabblank))
            {
                MessageBox.Show("Нельзя удалить запись.\nЭти бланки выдавались сотрудникам!", "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                p = false;
            }
            else
            {
                using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        FbCommand command = new FbCommand(@"DELETE FROM NAME_BLANKS WHERE NAME_BLANKS.NAME_BLANK = @NAME_BLANK AND NAME_BLANKS.ID = @IDBLANK", con);
                        command.Parameters.Add("@IDBLANK", FbDbType.Integer).Value = idTabblank;
                        command.Parameters.Add("@NAME_BLANK", FbDbType.VarChar).Value = nameBlank;

                        command.ExecuteScalar();

                        p = true;
                        con.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        p = false;
                    }
                }
            }            
            return p;
        }

        /// <summary>
        /// Проверяем: использовался ли бланк
        /// </summary>
        /// <param name="idTabblank">ID бланка</param>
        /// <returns></returns>
        private bool CheckingBlankForUse(int idTabblank)
        {
            bool p = false;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT COUNT(*) FROM JOURNAL_ISSUANCE WHERE JOURNAL_ISSUANCE.ID_NAME_BLANK = @IDBLANK", con);
                    command.Parameters.Add("@IDBLANK", FbDbType.VarChar).Value = idTabblank;
                    

                    if ((int)command.ExecuteScalar() == 0)
                    {
                        p = false;
                    }
                    else
                    {
                        p = true;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось выполнить запрос.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                con.Close();
            }
            return p;
        }

        /// <summary>
        /// Сохраняет изменения в названии бланка
        /// </summary>
        /// <param name="idTabblank">ID бланка</param>
        /// <param name="nameBlank">Новое название бланка</param>
        /// <returns>true если сохранилось, иначе false</returns>
        internal bool BlanqSaveEdit(int idTabblank, string nameBlank)
        {
            bool p = false;

            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"UPDATE NAME_BLANKS SET NAME_BLANKS.NAME_BLANK = @NAME_BLANK WHERE NAME_BLANKS.ID = @IDBLANK", con);
                    command.Parameters.Add("@IDBLANK", FbDbType.Integer).Value = idTabblank;
                    command.Parameters.Add("@NAME_BLANK", FbDbType.VarChar).Value = nameBlank;

                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    p = false;
                }
            }
            return p;
        }

        /// <summary>
        /// Добавляет запись бланка в базу
        /// </summary>
        /// <param name="nameBlank">Название бланка</param>
        /// <returns>true если сохранилось, иначе false</returns>
        internal bool BlanqSaveAdd(string nameBlank)
        {
            bool p = false;
            int idTabblank = ReturnMaxIdBlanq() + 1; 

            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"INSERT INTO NAME_BLANKS(NAME_BLANKS.ID, NAME_BLANKS.NAME_BLANK) VALUES(@IDBLANK, @NAME_BLANK)", con);
                    command.Parameters.Add("@IDBLANK", FbDbType.Integer).Value = idTabblank;
                    command.Parameters.Add("@NAME_BLANK", FbDbType.VarChar).Value = nameBlank;

                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    p = false;
                }
            }
            return p;
        }

        /// <summary>
        /// Возвращает максимальное значение ID из таблицы NAME_BLANKS
        /// </summary>
        /// <returns></returns>
        public int ReturnMaxIdBlanq()
        {
            int idadress = 0;
            using (FbConnection con = new FbConnection(props.Fields.ConnectionString))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT MAX(NAME_BLANKS.ID) FROM NAME_BLANKS", con);
                    FbDataReader dr = command.ExecuteReader();

                    dr.Read();
                    idadress = dr.GetInt32(0);
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось получить максимальное значение ID.\nПодробности: " + error.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            return idadress;
        }
        #endregion



        #endregion
    }
}
