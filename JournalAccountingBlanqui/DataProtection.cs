using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace JournalAccountingBlanqui
{
    public class DataProtection
    {
        // Create byte array for additional entropy when using Protect method.    
        //Создайте байтовый массив для дополнительной энтропии при использовании метода Protect
        static byte[] s_additionalEntropy = { 9, 8, 7, 6, 5 };

        //public static void Main()
        //{
        //    // Create a simple byte array containing data to be encrypted.
        //    //Создайте простой массив байтов, содержащий данные для шифрования
        //    byte[] secret = { 0, 1, 2, 3, 4, 1, 2, 3, 4 };

        //    //Encrypt the data.
        //    //Шифрование данных
        //    byte[] encryptedSecret = Protect(secret);
        //    Console.WriteLine("Зашифрованный массив байтов - это:"); // Console.WriteLine("The encrypted byte array is:");
        //    PrintValues(encryptedSecret);

        //    // Decrypt the data and store in a byte array. 
        //    byte[] originalData = Unprotect(encryptedSecret);
        //    Console.WriteLine("{0}Исходные данные таковы:", Environment.NewLine); // Console.WriteLine("{0}The original data is:", Environment.NewLine);
        //    PrintValues(originalData);
        //}

        public byte[] Protect(byte[] data)
        {
            try
            {
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted only by the same current user.
                // Зашифруйте данные используя DataProtectionScope.CurrentUser. Результат может быть расшифрован только тем же самым текущим пользователем.
                return ProtectedData.Protect(data, s_additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                MessageBox.Show("Данные не были зашифрованы. Произошла ошибка: " + e.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                //Console.WriteLine("Данные не были зашифрованы. Произошла ошибка."); // Console.WriteLine("Data was not encrypted. An error occurred.");
                //Console.WriteLine(e.ToString());
                return null;
            }
        }

        public byte[] Unprotect(byte[] data)
        {
            try
            {
                //Decrypt the data using DataProtectionScope.CurrentUser.
                //Расшифруйте данные с помощью DataProtectionScope.CurrentUser.
                return ProtectedData.Unprotect(data, s_additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                MessageBox.Show("Данные не были расшифрованы. Произошла ошибка: " + e.Message, "Журнал учёта бланков и распорядительных документов суда", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                //Console.WriteLine("Данные не были расшифрованы. Произошла ошибка."); // Console.WriteLine("Data was not decrypted. An error occurred.");
                //Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static void PrintValues(Byte[] myArr)
        {
            foreach (Byte i in myArr)
            {
                Console.Write("\t{0}", i);
            }
            Console.WriteLine();
        }
    }
}
