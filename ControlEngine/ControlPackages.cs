using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Serializable;
using System.Windows.Forms;

namespace ControlEngine
{
    [Serializable]
    public class ControlPackages
    {
        /// <summary>
        /// Пути до пакетов
        /// </summary>
        public List<string> Paths = new List<string>();

        /// <summary>
        /// Пакет по умолчанию
        /// </summary>
        public string DefaultPath;

        /// <summary>
        /// Пути до пакетов - Описание пакетов
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, ControlPackage> Items = new Dictionary<string, ControlPackage>();

        public ControlPackages()
        {
        }

        /// <summary>
        /// Обновить данные о пакетах
        /// </summary>
        public void Reload()
        {
            var clonedPaths = new List<string>(Paths);
            Paths.Clear();
            Items.Clear();
            foreach (string path in clonedPaths)
            {
                Add(path);
            }
        }

        /// <summary>
        /// Добавить новый пакет
        /// </summary>
        /// <param name="path"></param>
        public bool Add(string path)
        {
            if (!File.Exists(path)) return false;            
            ControlPackage cntrlPackage = ControlPackage.LoadFromFile(path);
            if (Items.ContainsKey(path))
            {               
                return false;
            }
            Paths.Add(path);
            Items.Add(path, cntrlPackage);
            return true;
        }

        /// <summary>
        /// Удалить пакет
        /// </summary>
        /// <param name="path"></param>
        public void Remove(string path)
        {
            if (!Paths.Contains(path)) return;
            Paths.Remove(path);
            Items.Remove(path);
        }

        /// <summary>
        /// Преобразовать в относительные пути
        /// </summary>
        /// <param name="root"></param>
        public void ToRelativePaths(string root)
        {
            DefaultPath = Common.UtilPath.GetRelativePath(DefaultPath, root);

            var newPaths = new List<string>();
            foreach (string path in Paths)
            {
                newPaths.Add(Common.UtilPath.GetRelativePath(path, root));
            }
            Paths = newPaths;
        }

        /// <summary>
        /// Преобразовать в абсолютные пути
        /// </summary>
        /// <param name="root"></param>
        public void ToAbsolutePaths(string root)
        {
            DefaultPath = Common.UtilPath.GetAbsolutePath(DefaultPath, root);

            var newPaths = new List<string>();
            foreach (string path in Paths)
            {
                newPaths.Add(Common.UtilPath.GetAbsolutePath(path, root));
            }
            Paths = newPaths;
        }

        /// <summary>
        /// Клонирование
        /// </summary>
        /// <returns></returns>
        public ControlPackages DeepClone()
        {
            return Common.SerializeWorker.Clone(this) as ControlPackages;
        }
    }
}
