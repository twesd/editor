using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace CommonUI
{
    /// <summary>
    /// Базовые настройки редактора
    /// </summary>
    [Serializable]
    public class CommonEditorSettings
    {
        public delegate void OpenItemFunc(string filename);

        /// <summary>
        /// Функция открытия файла
        /// </summary>
        OpenItemFunc _openItem;

        /// <summary>
        /// Последние открытые файлы
        /// </summary>
        public List<string> RecentFiles = new List<string>();

        /// <summary>
        /// Меню для заполнения настроек
        /// </summary>
        ToolStripMenuItem _menu;

        public CommonEditorSettings()
        {
        }

        /// <summary>
        /// Открытие файла настроек
        /// </summary>
        /// <param name="path"></param>
        /// <param name="openItem"></param>
        /// <returns></returns>
        public static CommonEditorSettings Open(string path)
        {
            CommonEditorSettings settings = null;
            try
            {
                XmlSerializer xmlSerelialize = new XmlSerializer(typeof(CommonEditorSettings));
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    settings = xmlSerelialize.Deserialize(reader) as CommonEditorSettings;
                }
            }
            catch
            {
                settings = new CommonEditorSettings();
            }

            // Составляем корректный список. Не существущие файлы убираем
            //
            List<string> validList = new List<string>();
            foreach (string filename in settings.RecentFiles)
            {
                if (File.Exists(filename))
                    validList.Add(filename);
            }
            settings.RecentFiles = validList;
            return settings;
        }

        /// <summary>
        /// Сохранить настройки в файл
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            XmlSerializer xmlSerelializeDsg = new XmlSerializer(typeof(CommonEditorSettings));
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                xmlSerelializeDsg.Serialize(writer, this);
            }
        }

        /// <summary>
        /// Событие открытия файла
        /// </summary>
        /// <param name="filename"></param>
        public void FileOpened(string filename)
        {
            filename = filename.Replace("\\", "/");

            if (RecentFiles.Contains(filename))
            {
                RecentFiles.Remove(filename);
            }

            if (RecentFiles.Count < 7)
            {
                RecentFiles.Insert(0, filename);
            }
            else
            {
                RecentFiles.Insert(0, filename);
                RecentFiles.Remove(RecentFiles.Last());
            }
        }

        /// <summary>
        /// Заполнить меню
        /// </summary>
        /// <param name="menu"></param>
        public void FillMenu(ToolStripMenuItem menu, OpenItemFunc openItem)
        {
            _openItem = openItem;
            _menu = menu;

            // Удаляем предыдущие пункты
            //
            DeleteMenuItems(menu);

            // Добавляем новые пункты
            //
            ToolStripItem item;
            foreach (string filename in RecentFiles)
            {
                item = menu.DropDownItems.Add(filename);
                item.Tag = this;
                item.Click += new EventHandler(ItemOpen_Click);
            }
                
            var separator = new ToolStripSeparator();
            separator.Tag = this;
            menu.DropDownItems.Add(separator);
            
            item = menu.DropDownItems.Add("Очистить");
            item.Tag = this;            
            item.Click += new EventHandler(ItemClear_Click);
        }

        /// <summary>
        /// Удаляем пункты меню
        /// </summary>
        /// <param name="menu"></param>
        private static void DeleteMenuItems(ToolStripMenuItem menu)
        {
            List<ToolStripItem> deleteItems = new List<ToolStripItem>();
            foreach (ToolStripItem item in menu.DropDownItems)
            {
                if (item.Tag is CommonEditorSettings)
                {
                    deleteItems.Add(item);
                }
            }
            foreach (ToolStripItem item in deleteItems)
            {
                menu.DropDownItems.Remove(item);
            }
        }

        /// <summary>
        /// Открытие файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemOpen_Click(object sender, EventArgs e)
        {
            _openItem((sender as ToolStripItem).Text);
        }

        /// <summary>
        /// Очистка меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemClear_Click(object sender, EventArgs e)
        {
            RecentFiles = new List<string>();
            if (_menu != null)
            {
                DeleteMenuItems(_menu);
            }
        }
    }
}
