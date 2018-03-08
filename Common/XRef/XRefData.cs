using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// Данные внешней ссылки
    /// </summary>
    [Serializable]
    public class XRefData : IPathConvertible
    {
        /// <summary>
        /// Путь до файла
        /// </summary>
        [CategoryAttribute("Базовые")]
        [DescriptionAttribute("Путь до ссылки")]
        public string FileName { get; set; }

        /// <summary>
        /// Операции
        /// </summary>
        [CategoryAttribute("Базовые")]
        [DescriptionAttribute("Операции с внешней ссылкой")]
        public List<XRefOperation> Operations { get; set; }

        public XRefData()
        {
            FileName = string.Empty;
            Operations = new List<XRefOperation>();
        }

        public void ToRelativePaths(string root)
        {
            FileName = Common.UtilPath.GetRelativePath(FileName, root);
        }

        public void ToAbsolutePaths(string root)
        {
            FileName = Common.UtilPath.GetAbsolutePath(FileName, root);
        }

        /// <summary>
        /// Удаление повторяющихся операций
        /// </summary>
        public void RemoveDublicateOperations()
        {
            var handledProps = new Dictionary<string, List<string>>();
            var cloneOps = new List<XRefOperation>(Operations);
            cloneOps.Reverse();
            foreach (XRefOperation operation in cloneOps)
            {
                if (operation is XRefOperationChange)
                {
                    var changeOp = operation as XRefOperationChange;
                    if (!handledProps.ContainsKey(changeOp.Id))
                    {
                        handledProps.Add(
                            changeOp.Id, 
                            new List<string>() { changeOp.PropertyName });
                        continue;
                    }
                    List<string> props = handledProps[changeOp.Id];

                    if (props.Contains(changeOp.PropertyName))
                    {
                        Operations.Remove(operation);
                    }
                    else
                    {
                        props.Add(changeOp.PropertyName);
                    }
                }
            }
        }
    }
}
