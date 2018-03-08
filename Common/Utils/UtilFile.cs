using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Common
{
    public class UtilFile
    {
        /// <summary>
        /// Чтение текстового файла целиком
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadTextFile(string path)
        {
            if(!File.Exists(path)) return string.Empty;
            using(TextReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Запись в текстовый файл
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void WriteTextFile(string path, string data)
        {
            if (!File.Exists(path)) return;
            using (TextWriter writer = new StreamWriter(path))
            {
                writer.Write(data);
            }
        }

        /// <summary>
        /// Получить имя файла
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string OpenDialog(string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файл (*" + filter + ")|*" + filter + "|" +
                         "Все файлы (*.*)|*.*";
            dialog.Title = "Открыть файл поведения";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return null;
            return dialog.FileName;
        }
    }
}
