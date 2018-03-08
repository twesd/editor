using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class UtilPath
    {
        /// <summary>
        /// Получение директории сборки
        /// </summary> 
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Получить относительный путь
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="root">Путь ок которого строится относительный</param>
        /// <returns></returns>
        public static string GetRelativePath(string path, string root)
        {
            if (string.IsNullOrEmpty(root))
                throw new ArgumentNullException();
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            path = path.Replace("/","\\");
            root = root.Replace("/", "\\");
            Uri pathUri = new Uri(path);
            Uri rootUri = new Uri(root);
            string relativePath = rootUri.MakeRelativeUri(pathUri).ToString();
            return relativePath.Replace("\\", "/");
        }

        /// <summary>
        /// Получить полный путь
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="root">Путь от которого строится относительный</param>
        /// <returns></returns>
        public static string GetAbsolutePath(string path, string root)
        {
            if (string.IsNullOrEmpty(root))
                throw new ArgumentNullException();
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            path = path.Replace("/", "\\");
            root = root.Replace("/", "\\");
            root = System.IO.Path.GetDirectoryName(root);
            var dest = System.IO.Path.Combine(root, path).Replace("\\", "/");
            dest = System.IO.Path.GetFullPath(dest).Replace("\\", "/");            
            return dest;
        }

        public static List<string> GetRelativePath(List<string> Paths, string root)
        {
            List<string> resPaths =  new List<string>();
            foreach (string path in Paths)
            {
                resPaths.Add(GetRelativePath(path, root));
            }
            return resPaths;
        }

        public static List<string> GetAbsolutePath(List<string> Paths, string root)
        {
            List<string> resPaths = new List<string>();
            foreach (string path in Paths)
            {
                resPaths.Add(GetAbsolutePath(path, root));
            }
            return resPaths;
        }

    }
}
