using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace JournalAccountingBlanqui
{
    class CLSDB
    {
        Props props = new Props(); //экземпляр класса с настройками
        PropsFields propsFields = new PropsFields(); //экземпляр класса с настройками 
        private DataTable bindingSource = new DataTable();
        //DataSet dataSet;

        private string pathSaveSetting { get { return Application.StartupPath + "\\Setting.xml"; } }
        static string ConnectFileBase = Application.StartupPath;
        StringBuilder SqlText = new StringBuilder();
        StringBuilder SqlText1 = new StringBuilder();
        StringBuilder SqlTextU = new StringBuilder();

        /// <summary>
        /// Возвращает список бланков не распечатанных в журнал
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <param name="statusPrint">Если бланк не распечатан, то "FALSE"</param>
        /// <returns></returns>
        internal DataSet GetPrint(int userID, string statusPrint)
        {
            DataSet dataSet = new DataSet();
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                con.Open();
                FbCommand command = new FbCommand(@"SELECT JOURNAL_OF_USE.ID, JOURNAL_OF_USE.DATEUSE, NAME_BLANKS.NBLANK, DESTINATION_ADRESS.ADRESS, JOURNAL_OF_USE.N_ARRAY, JOURNAL_OF_USE.NUM_BLANK FROM JOURNAL_OF_USE INNER JOIN USERS ON (JOURNAL_OF_USE.FIO = USERS.ID) INNER JOIN DESTINATION_ADRESS ON (JOURNAL_OF_USE.ID_ADRESS = DESTINATION_ADRESS.ID) INNER JOIN NAME_BLANKS ON (JOURNAL_OF_USE.BLANK_NAME = NAME_BLANKS.ID) WHERE ((USERS.ID = @USERID) AND (JOURNAL_OF_USE.PRINT = @STATUSPRINT)) ORDER BY JOURNAL_OF_USE.NUM_BLANK", con);
                command.Parameters.Add("@USERID", FbDbType.Integer).Value = userID;
                command.Parameters.Add("@STATUSPRINT", FbDbType.Text).Value = statusPrint;
                FbDataAdapter adapter = new FbDataAdapter(command);
                
                adapter.Fill(dataSet);
                con.Close();
            }
            return dataSet;
        }

        /// <summary>
        /// Устанавливает в строке по заданному ID значения для полей статуса печати и даты печати отчёта
        /// </summary>
        /// <param name="idjournal">ID записи</param>
        /// <param name="statusPrint">если рапечатываем, то соответственно "TRUE"</param>
        /// <param name="dateprint">Дата печати отчёта</param>
        /// <returns></returns>
        internal bool SetPrint(int idjournal, string statusPrint, DateTime dateprint)
        {
            bool p = false;
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"UPDATE JOURNAL_OF_USE SET JOURNAL_OF_USE.PRINT = @STATUSPRINT, JOURNAL_OF_USE.DATE_PRINT = @DATEPRINT WHERE JOURNAL_OF_USE.ID = @IDJOURNAL", con);
                    command.Parameters.Add("@STATUSPRINT", FbDbType.Text).Value = statusPrint;
                    command.Parameters.Add("@DATEPRINT", FbDbType.Date).Value = dateprint;
                    command.Parameters.Add("@IDJOURNAL", FbDbType.Integer).Value = idjournal;

                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    p = false;
                }
            }
            return p;
        }

        /// <summary>
        /// Возвращает да или нет и заполняет ID, ФИО, должность и статус пользователя если пароль верный
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>true или false в зависимости от результата авторизации</returns>
        public Boolean SqlPassword(string login, string password)
        {
            Boolean control = false;

            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT USERS.ID, USERS.FIO, USERS.OFFICE, USERS.STATUS FROM USERS WHERE USERS.LOGIN = @LOGIN AND USERS.PASSW = @PASSWORD", con);
                    command.Parameters.Add("@LOGIN", FbDbType.Text).Value = login;
                    command.Parameters.Add("@PASSWORD", FbDbType.Text).Value = password;

                    using (var dataReader = command.ExecuteReader())
                    {
                        control = dataReader.Read();
                        if (control)
                        {
                            propsFields.UserID = dataReader.GetInt32(0);
                            propsFields.UserFIO = dataReader.GetString(1);
                            propsFields.UserOffice = dataReader.GetString(2);
                            propsFields.UserStatus = dataReader.GetString(3);
                            dataReader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Неудаётся подключиться к базе данных: " + ex, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OKCancel);
                }
                finally
                {
                    con.Close();
                }

                return control;
            }
        }

        /// <summary>
        /// Возвращает список использованных бланков пользователем по его ID
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <returns></returns>
        internal DataTable UpdSpisokFullUser(int userID)
        {
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {   //Запрос обновлён
                FbCommand command = con.CreateCommand();
                command.CommandText = @"SELECT JOURNAL_OF_USE.ID, JOURNAL_OF_USE.DATEUSE, DESTINATION_ADRESS.ADRESS, NAME_BLANKS.NBLANK, JOURNAL_OF_USE.N_ARRAY, JOURNAL_OF_USE.NUM_BLANK, JOURNAL_OF_USE.DATE_PRINT FROM DESTINATION_ADRESS INNER JOIN (USERS INNER JOIN (NAME_BLANKS INNER JOIN JOURNAL_OF_USE ON NAME_BLANKS.ID = JOURNAL_OF_USE.BLANK_NAME) ON USERS.ID = JOURNAL_OF_USE.FIO) ON DESTINATION_ADRESS.ID = JOURNAL_OF_USE.ID_ADRESS WHERE (USERS.ID='" + userID + "') ORDER BY JOURNAL_OF_USE.ID DESC";
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
        /// Возвращает список использованных бланков пользователем по его ID
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <param name="dateotpr">Дата использования бланка</param>
        /// <param name="adress">Адрес направления</param>
        /// <param name="nblank">Название бланка</param>
        /// <param name="numarray">Номер наряда</param>
        /// <param name="num">Номер бланка</param>
        /// <returns></returns>
        internal object UpdSpisokSearchUser(int userID, string dateotpr, string adress, string nblank, string numarray, string num)
        {
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {   //Запрос обновлён
                FbCommand command = con.CreateCommand();
                command.CommandText = @"SELECT JOURNAL_OF_USE.ID, JOURNAL_OF_USE.DATEUSE, DESTINATION_ADRESS.ADRESS, NAME_BLANKS.NBLANK, JOURNAL_OF_USE.N_ARRAY, JOURNAL_OF_USE.NUM_BLANK, JOURNAL_OF_USE.PRINT FROM JOURNAL_OF_USE INNER JOIN DESTINATION_ADRESS ON (JOURNAL_OF_USE.ID_ADRESS = DESTINATION_ADRESS.ID) INNER JOIN NAME_BLANKS ON (JOURNAL_OF_USE.BLANK_NAME = NAME_BLANKS.ID) WHERE (JOURNAL_OF_USE.FIO = @IDFIO) AND (JOURNAL_OF_USE.DATEUSE LIKE '%" + dateotpr + "%') AND (UPPER(_WIN1251''||DESTINATION_ADRESS.ADRESS) LIKE UPPER(_WIN1251''||'%" + adress + "%')) AND (UPPER(_WIN1251''||NAME_BLANKS.NBLANK) LIKE UPPER(_WIN1251''||'%" + nblank + "%')) AND (JOURNAL_OF_USE.N_ARRAY LIKE '%" + numarray + "%') AND (JOURNAL_OF_USE.NUM_BLANK LIKE '%" + num + "%') ORDER BY JOURNAL_OF_USE.ID DESC";
                command.Parameters.Add("@IDFIO", FbDbType.VarChar).Value = userID;
                

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
        /// Возвращает true если бланк уже использовался
        /// </summary>
        /// <param name="name">Наименование бланка</param> 
        /// <param name="nn">Номер бланка</param> 
        /// <returns>true или false</returns>
        internal bool GetUsedNomBlanq(string name, string nn)
        {
            bool m = false;

            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                con.Open();
                FbCommand sqlReq = con.CreateCommand();
                sqlReq.CommandText = @"SELECT JOURNAL_OF_USE.ID FROM JOURNAL_OF_USE INNER JOIN NAME_BLANKS ON (JOURNAL_OF_USE.BLANK_NAME = NAME_BLANKS.ID) WHERE NAME_BLANKS.NBLANK = '" + name + "' AND JOURNAL_OF_USE.NUM_BLANK = " + Int32.Parse(nn) + "";
                FbDataReader dr = sqlReq.ExecuteReader();

                m = dr.Read();

                con.Close();
            }
            return m;
        }


        /// <summary>
        /// Возвращает true если информация о движении бланка уже распечатана
        /// </summary>
        /// <param name="name">Наименование бланка</param> 
        /// <param name="nn">Номер бланка</param> 
        /// <returns>true или false</returns>
        internal string GetPrintedNomBlanq(string name, string nn)
        {
            bool m = false;
            string print = "FALSE";

            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                con.Open();
                FbCommand sqlReq = con.CreateCommand();
                sqlReq.CommandText = @"SELECT JOURNAL_OF_USE.PRINT FROM JOURNAL_OF_USE INNER JOIN NAME_BLANKS ON (JOURNAL_OF_USE.BLANK_NAME = NAME_BLANKS.ID) WHERE NAME_BLANKS.NBLANK = '" + name + "' AND JOURNAL_OF_USE.NUM_BLANK = " + nn + "";
                FbDataReader dr = sqlReq.ExecuteReader();

                m = dr.Read();
                if (m)
                {
                    print = dr.GetString(0);
                    dr.Close();
                }
                con.Close();
            }
            return print;
        }

        /// <summary>
        /// Возвращает список адресов для автозаполнения поля с адресом направления
        /// </summary>
        /// <returns>список адресов в оболочке AutoCompleteStringCollection</returns>
        internal AutoCompleteStringCollection UpdAutoComplAdress()
        {
            AutoCompleteStringCollection list = new AutoCompleteStringCollection();
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {   //Запрос обновлён
                con.Open();
                FbCommand sqlReq = con.CreateCommand();
                sqlReq.CommandText = @"SELECT DISTINCT DESTINATION_ADRESS.ADRESS FROM DESTINATION_ADRESS ORDER BY DESTINATION_ADRESS.ADRESS";
                FbDataReader dr = sqlReq.ExecuteReader();

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
        internal AutoCompleteStringCollection UpdAutoComplName()
        {
            AutoCompleteStringCollection list = new AutoCompleteStringCollection();
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {   //Запрос обновлён
                con.Open();
                FbCommand sqlReq = con.CreateCommand();
                sqlReq.CommandText = @"SELECT DISTINCT NAME_BLANKS.NBLANK FROM NAME_BLANKS ORDER BY NAME_BLANKS.NBLANK";
                FbDataReader dr = sqlReq.ExecuteReader();

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
        /// Возвращает true если бланк выдавался сотруднику
        /// </summary>
        /// <param name="iduser">ID пользователя</param>
        /// <param name="namebl">Название бланка</param> 
        /// <param name="num">Номер бланка</param> 
        /// <returns></returns>
        internal bool GetBlanqUser(int iduser, string namebl, string num)
        {
            bool m = false;
            int numint = Convert.ToInt32(num), a = 0, b = 0;
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                con.Open();
                FbCommand command = new FbCommand(@"SELECT JOURNAL_ISSUANCE.FIRST_BLANK, JOURNAL_ISSUANCE.LAST_BLANK FROM NAME_BLANKS INNER JOIN JOURNAL_ISSUANCE ON (NAME_BLANKS.ID = JOURNAL_ISSUANCE.BLANK_NAME) INNER JOIN USERS ON (JOURNAL_ISSUANCE.FIO = USERS.ID) WHERE ((NAME_BLANKS.NBLANK = @NBLANK) AND (USERS.ID = @IDUSER))", con);
                command.Parameters.Add("@NBLANK", FbDbType.Text).Value = namebl;
                command.Parameters.Add("@IDUSER", FbDbType.Integer).Value = iduser;
                FbDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    a = dr.GetInt32(0);
                    b = dr.GetInt32(1);
                    if (numint >= a && numint <= b)
                    { m = true; break; }
                }
                con.Close();
            }
            return m;
        }


        /// <summary>
        /// Возвращает максимальное значение ID из таблицы JOURNAL_OF_USE
        /// </summary>
        /// <returns></returns>
        public int ReturnMaxIdJournal()
        {
            int idjournal = 0;
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                try
                {
                    con.Open();
                    FbCommand sqlReq = con.CreateCommand();
                    sqlReq.CommandText = @"SELECT MAX(JOURNAL_OF_USE.ID) FROM JOURNAL_OF_USE";
                    FbDataReader dr = sqlReq.ExecuteReader();

                    dr.Read();
                    idjournal = dr.GetInt32(0);
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось получить максимальное значение ID. Подробности ",
                        error.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            return idjournal;
        }

        /// <summary>
        /// Сохраняет изменения в записях о движении бланка
        /// </summary>
        /// <param name="idjournal">ID записи использованных бланков</param>
        /// <param name="dateotpr">Дата направления бланка</param>
        /// <param name="nblank">Название бланка</param>
        /// <param name="adress">Адрес назначения</param>
        /// <param name="numarray">Номер наряда</param>
        /// <param name="num">Номер бланка</param>
        /// <returns></returns>
        internal bool BlanqSave(int idjournal, DateTime dateotpr, string nblank, string adress, string numarray, string num)
        {
            bool p = false;
            int idadress = ReturnIdAdress(adress);
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"UPDATE JOURNAL_OF_USE SET JOURNAL_OF_USE.DATEUSE = @DATEOTPR, JOURNAL_OF_USE.BLANK_NAME = (SELECT NAME_BLANKS.ID FROM NAME_BLANKS WHERE NAME_BLANKS.NBLANK = @NBLANK), JOURNAL_OF_USE.ID_ADRESS = @IDADRESS, JOURNAL_OF_USE.N_ARRAY = @NUMARRAY, JOURNAL_OF_USE.NUM_BLANK = @NUMBLANK WHERE JOURNAL_OF_USE.ID = @IDJOURNAL", con);
                    command.Parameters.Add("@IDJOURNAL", FbDbType.Integer).Value = idjournal;
                    command.Parameters.Add("@IDADRESS", FbDbType.Integer).Value = idadress;
                    command.Parameters.Add("@DATEOTPR", FbDbType.Date).Value = dateotpr;
                    command.Parameters.Add("@NBLANK", FbDbType.VarChar).Value = nblank;
                    command.Parameters.Add("@NUMARRAY", FbDbType.VarChar).Value = numarray;
                    command.Parameters.Add("@NUMBLANK", FbDbType.Integer).Value = Int32.Parse(num);
                    command.Parameters.Add("@PRINT", FbDbType.VarChar).Value = "FALSE";

                    command.ExecuteScalar();

                    p = true;
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось изменить запись.\nПодробности: " + error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    p = false;
                }
            }
            return p;
        }

        /// <summary>
        /// Добавляет Использованный бланк в таблицу JOURNAL_OF_USE
        /// </summary>
        /// <param name="dateotpr">Дата отправления бланка</param>
        /// <param name="idUser">ID пользователя</param>
        /// <param name="nblank">Наименование бланка</param>
        /// <param name="adress">Адрес назначения бланк</param>
        /// <param name="num">Номер бланка</param>
        /// <param name="nomarray">Номер наряда</param>
        /// <returns></returns>
        public bool AddBlank(DateTime dateotpr, int idUser, string adress, string nblank, string nomarray, string num)
        {
            bool p = false;
            //int idjournal = ReturnMaxIdJournal() + 1;
            int idadress = ReturnIdAdress(adress);
            int idnblank = ReturnIdNblank(nblank);
            int numblank = Int32.Parse(num);
            if (idadress == -1)
            {
                MessageBox.Show("Не удалось добавить бланк", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                using (FbConnection con = new FbConnection(props.ConnectStr()))
                {
                    try
                    {
                        con.Open();
                        FbCommand command = new FbCommand(@"INSERT INTO JOURNAL_OF_USE(ID, DATEUSE, FIO, ID_ADRESS, BLANK_NAME, NUM_BLANK, N_ARRAY, PRINT) VALUES(@IDJOURNAL, @DATEOTPR, '" + idUser + "', '" + idadress + "', '" + idnblank + "', @NUMBLANK, '" + nomarray + "', @PRINT)", con);
                        command.Parameters.Add("@IDJOURNAL", FbDbType.Integer).Value = ReturnMaxIdJournal() + 1;
                        command.Parameters.Add("@DATEOTPR", FbDbType.Date).Value = dateotpr;
                        command.Parameters.Add("@NUMBLANK", FbDbType.Integer).Value = numblank;
                        command.Parameters.Add("@PRINT", FbDbType.VarChar).Value = "FALSE";

                        command.ExecuteScalar();
                        p = true;
                        con.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Подробности " + error.Message, "Не удалось добавить бланк", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        p = false;
                    }
                }
            }
            return p;
        }

        /// <summary>
        /// Возвращает ID названия бланка из таблицы NAME_BLANKS
        /// </summary>
        /// <param name="nblank">Название бланка</param>
        /// <returns></returns>
        private int ReturnIdNblank(string nblank)
        {
            int idnamebl = 0;
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                try
                {
                    con.Open();
                    FbCommand sqlReq = con.CreateCommand();
                    sqlReq.CommandText = @"SELECT ID FROM NAME_BLANKS WHERE NBLANK = '" + nblank + "'";
                    FbDataReader dr = sqlReq.ExecuteReader();

                    dr.Read();
                    idnamebl = dr.GetInt32(0);
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось получить ID названия бланка. Подробности ", error.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            return idnamebl;
        }

        /// <summary>
        /// Возвращает ID Адреса из таблицы Destination_Adress
        /// </summary>
        /// <param name="adress">Название пункта назначения</param> 
        /// <returns>ID Адреса</returns>
        public int ReturnIdAdress(string adress)
        {
            int idadress = 0;
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"SELECT DESTINATION_ADRESS.ID FROM DESTINATION_ADRESS WHERE DESTINATION_ADRESS.ADRESS = @ADRESS", con);
                    command.Parameters.Add("@ADRESS", FbDbType.VarChar).Value = adress;
                    FbDataReader dr = command.ExecuteReader();

                    
                    if (dr.Read())
                    {
                        idadress = dr.GetInt32(0);
                    }
                    else
                    {
                        idadress = AddAdress(adress);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось получить ID Адреса. Подробности ", error.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                con.Close();
            }
            return idadress;
        }

        /// <summary>
        /// Добавляет новое значение Адреса в таблицу Destination_Adress
        /// </summary>
        /// <param name="adress">Название пункта назначения</param> 
        /// <returns>ID нового адреса</returns>
        public int AddAdress(string adress)
        {
            int idadress = ReturnMaxIdAdress() + 1;
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                try
                {
                    con.Open();
                    FbCommand command = new FbCommand(@"INSERT INTO DESTINATION_ADRESS(DESTINATION_ADRESS.ID, DESTINATION_ADRESS.ADRESS) VALUES(@IDADRESS, @ADRESS)", con);
                    command.Parameters.Add("@IDADRESS", FbDbType.VarChar).Value = idadress;
                    command.Parameters.Add("@ADRESS", FbDbType.VarChar).Value = adress;
                    command.ExecuteScalar();
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось добавить Новый адрес. Подробности ", error.Message,
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    idadress = -1;
                }
            }
            return idadress;
        }

        /// <summary>
        /// Возвращает максимальное значение ID из таблицы Destination_Adress
        /// </summary>
        /// <returns></returns>
        public int ReturnMaxIdAdress()
        {
            int idadress = 0;
            using (FbConnection con = new FbConnection(props.ConnectStr()))
            {
                try
                {
                    con.Open();
                    FbCommand command = con.CreateCommand();
                    command.CommandText = @"SELECT MAX(DESTINATION_ADRESS.ID) FROM DESTINATION_ADRESS";
                    FbDataReader dr = command.ExecuteReader();

                    dr.Read();
                    idadress = dr.GetInt32(0);
                    con.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Не удалось получить максимальное значение ID. Подробности ", error.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            return idadress;
        }

    }
}
