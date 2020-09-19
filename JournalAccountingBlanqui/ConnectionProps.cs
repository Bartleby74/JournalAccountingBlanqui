using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace JournalAccountingBlanqui
{
    public class PropsFields
    {
        //Чтобы добавить настройку в программу просто добавьте суда строку вида -
        //public ТИП ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию;
        //private static string baseFileDir = @"VIKI:D:\DATA\JUSTICE\BLANQUI.FDB";
        private static string baseFileDir = @"Server2019t01:D:\DATA\JUSTICE";
        //private static string baseFileDir = @"Server2009t01:C:\DATA\JUSTICE";
        private static string systemUser = @"SYSDBA";
        private static string systemPassword = @"m";
        private static string userLogin = "";
        private static string userPassw = "";
        private static int userID = 0;
        private static string userFIO = "";
        private static string userOffice = "";
        private static string userStatus = "";
        private static int selectRow = 0;
        private static DateTime dateSelect = DateTime.Now;

        private static string сonnectionString = "";

        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public string ConnectionString
        {
            get
            { return сonnectionString; }
            set
            { сonnectionString = value; }
        }

        /// <summary>
        /// Расположение базы данных
        /// </summary>
        public DateTime DateSelect
        {
            get
            { return dateSelect; }
            set
            { dateSelect = value; }
        }

        /// <summary>
        /// Расположение базы данных
        /// </summary>
        public string BaseFileDir
        {
            get
            { return baseFileDir; }
            set
            { baseFileDir = value; }
        }

        /// <summary>
        /// Администратор базы данных
        /// </summary>
        public string SystemUser
        {
            get
            { return systemUser; }
            set
            { systemUser = value; }
        }

        /// <summary>
        /// Пароль администратора базы данных
        /// </summary>
        public string SystemPassword
        {
            get
            { return systemPassword; }
            set
            { systemPassword = value; }
        }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserLogin
        {
            get
            { return userLogin; }
            set
            { userLogin = value; }
        }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string UserPassw
        {
            get
            { return userPassw; }
            set
            { userPassw = value; }
        }

        /// <summary>
        /// ID пользователя
        /// </summary>
        public int UserID
        {
            get
            { return userID; }
            set
            { userID = value; }
        }

        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string UserFIO
        {
            get
            { return userFIO; }
            set
            { userFIO = value; }
        }

        /// <summary>
        /// Должность пользователя
        /// </summary>
        public string UserOffice
        {
            get
            { return userOffice; }
            set
            { userOffice = value; }
        }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        public string UserStatus
        {
            get
            { return userStatus; }
            set
            { userStatus = value; }
        }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        public int SelectRow
        {
            get
            { return selectRow; }
            set
            { selectRow = value; }
        }
    }

    public class Props
    {
        /// <summary>
        /// Путь до файла настроек
        /// </summary>
        public string XMLFileName = Environment.CurrentDirectory + "\\setting.xml";

        string userid = "", passw = "";
        // Строка подключения
        string ConnectFileBase = "";
        
        public PropsFields Fields;
        /// <summary>
        /// Создаёт новый экземпляр класса ConnectionSettings
        /// </summary>
        public Props()
        {
            Fields = new PropsFields();
        }

        /// <summary>
        /// Формирует строку подключения к базе данных Судебное делопроизводство
        /// </summary>
        /// <returns>Возвращает строку подключения</returns>
        public string ConnectStr()
        {
            ReadXml();

            ConnectFileBase = Fields.BaseFileDir;
            userid = Fields.SystemUser;
            passw = Fields.SystemPassword;

            if (ConnectFileBase == "")
            { ConnectFileBase = Application.StartupPath + "\\BLANQUI.FDB"; }
            if (userid == "")
            { userid = "SYSDBA"; }
            if (passw == "")
            { passw = "m"; }

            StringBuilder csb = new StringBuilder();

            // Указываем тип используемого сервера
            csb.Append("server type=Embedded;");

            // Путь до файла с базой данных
            csb.Append("initial catalog=" + ConnectFileBase + ";");

            // Настройка параметров "общения" клиента с сервером
            csb.Append("character set=WIN1251;dialect=3;");

            // Настройки аутентификации
            csb.Append("user id=" + userid + ";");
            csb.Append("password=" + passw);

            Fields.ConnectionString = csb.ToString();

            return Fields.ConnectionString;
        }

        /// <summary>
        /// Чтение настроек из файла
        /// </summary>
        public void ReadXml()
        {
            if (File.Exists(XMLFileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextReader reader = new StreamReader(XMLFileName);
                Fields = ser.Deserialize(reader) as PropsFields;
                reader.Close();
            }
            else
            {
                MessageBox.Show("Неудаётся найти файл - setting.xml",
                    "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OKCancel);
            }
        }

        /// <summary>
        /// Запись настроек в файл
        /// </summary>
        public void WriteXml()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextWriter writer = new StreamWriter(XMLFileName);
                ser.Serialize(writer, Fields);
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неудаётся сохранить настройки: " + ex,
                    "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OKCancel);
            }
        }

    }
}
