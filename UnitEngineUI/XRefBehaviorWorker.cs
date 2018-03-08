using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serializable;
using CommonUI;
using System.Windows.Forms;
using UnitEngine;
using System.Xml.Serialization;
using System.IO;
using UnitEngine.Behavior;
using Common;
using TransactionCore;
using UnitEngineUI.Properties;
using System.Reflection;

namespace UnitEngineUI
{
    /// <summary>
    /// Класс для работы с внешними ссылками
    /// </summary>
    class XRefBehaviorWorker
    {
        static Dictionary<string, UnitBehavior> _cacheContainer = new Dictionary<string, UnitBehavior>();

        /// <summary>
        /// Добавить новую ссылку
        /// </summary>
        /// <param name="xrefPath"></param>
        public static TreeNodeXref CreateXreference(
            string xRefPath,
            TransactionManager transManager,
            TreeViewConvertor.CreateTreeNodeHandler createHandler)
        {
            var xRefData = new XRefData()
            {
                FileName = xRefPath
            };
            TreeNodeXref treeNodeXRef = new TreeNodeXref(xRefData)
            {
                Text = System.IO.Path.GetFileName(xRefPath)
            };
            UpdateXref(treeNodeXRef, transManager, createHandler);
            return treeNodeXRef;
        }

        /// <summary>
        /// Получаем внешнию ссылку, если узел входит в неё
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TreeNodeXref GetXRefNodeRoot(TreeNodeBase node)
        {
            if (node == null)
            {
                return null;
            }
            TreeNodeBase parent = node.Parent;
            while (parent != null)
            {
                if (parent is TreeNodeXref)
                {
                    return parent as TreeNodeXref;
                }
                parent = parent.Parent;
            }
            return null;
        }

        /// <summary>
        /// Обновление внешней ссылки
        /// </summary>
        /// <param name="treeNodeXRef"></param>
        public static void UpdateXref(
            TreeNodeXref treeNodeXRef,
            TransactionManager transManager,
            TreeViewConvertor.CreateTreeNodeHandler createHandler)
        {
            if (treeNodeXRef == null)
            {
                return;
            }

            treeNodeXRef.Nodes.Clear();

            XRefData xRefData = treeNodeXRef.XRefData;
            if (xRefData == null)
            {
                return;
            }

            // Перезагружаем данные
            //

            UnitBehavior behavior = null;
            if (_cacheContainer.ContainsKey(xRefData.FileName))
            {
                behavior = _cacheContainer[xRefData.FileName];
            }
            else
            {
                behavior = ReadBehaviorFromFile(xRefData.FileName);
                _cacheContainer[xRefData.FileName] = behavior;
            }
            if (behavior == null)
            {
                return;
            }

            List<TreeNodeBase> nodes = TreeViewConvertor.ConvertToNodes(
                behavior.TreeView.Nodes,
                transManager,
                createHandler);

            List<TreeNodeBase> destNodes = ApplyOperations(xRefData, nodes, transManager);

            foreach (TreeNodeBase node in destNodes)
            {
                treeNodeXRef.Nodes.Add(node);
            }

        }

        /// <summary>
        /// Применить операции к внешней ссылки
        /// </summary>
        /// <param name="xRefData"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private static List<TreeNodeBase> ApplyOperations(
            XRefData xRefData, List<TreeNodeBase> nodes, TransactionManager transManager)
        {
            var resNodes = new List<TreeNodeBase>(nodes);
            if (xRefData.Operations == null)
            {
                return resNodes;
            }

            foreach (XRefOperation operation in xRefData.Operations)
            {
                if (operation is XRefOperationDelete)
                {
                    // Выполняем операцию удаления 
                    //
                    var delOp = operation as XRefOperationDelete;
                    foreach (TreeNodeBase node in resNodes)
                    {
                        string id = Helper.GetItemId(node.Tag);
                        if (id == delOp.Id)
                        {
                            resNodes.Remove(node);
                            break;
                        }
                    }
                }
                else if (operation is XRefOperationAdd)
                {
                    // Выполняем операцию добавления 
                    //
                    var addOp = operation as XRefOperationAdd;
                    var control = addOp.Item as UnitAction;
                    if (control == null)
                    {
                        continue;
                    }

                    // Устанавливаем доп. иконку для отображения, что объект добавлен
                    //
                    var newNode = new TreeNodeAction(control, transManager);
                    newNode.IconOverlay = Resources.IconOverlayAdd.ToBitmap();

                    resNodes.Add(newNode);
                }
                else if (operation is XRefOperationChange)
                {
                    // Ищем заданный объект
                    //
                    var changeOp = operation as XRefOperationChange;
                    object changeItem = null;
                    TreeNodeBase changeTreeViewNode = null;
                    foreach (TreeNodeBase node in resNodes)
                    {
                        string id = Helper.GetItemId(node.Tag);
                        if (id == changeOp.Id)
                        {
                            changeTreeViewNode = node;
                            changeItem = node.Tag;
                            break;
                        }
                    }
                    if (changeItem == null)
                    {
                        continue;
                    }

                    if (changeTreeViewNode is TreeNodeAction)
                    {
                        // Устанавливаем доп. иконку для отображения, что объект изменён
                        //
                        var controlTreeViewNode = changeTreeViewNode as TreeNodeAction;
                        controlTreeViewNode.IconOverlay = Resources.IconOverlayChange.ToBitmap();
                    }

                    // Изменяем свойство объекта
                    //
                    foreach (PropertyInfo destProperty in changeItem.GetType().GetProperties())
                    {
                        if (destProperty.Name == changeOp.PropertyName)
                        {
                            if (!destProperty.CanWrite)
                            {
                                throw new ArgumentException("Свойство " + destProperty.Name + " не доступно для записи");
                            }
                            destProperty.SetValue(changeItem, changeOp.PropertyValue, new object[] { });
                            break;
                        }
                    }
                }
                else
                {
                    throw new NotSupportedException("Обнаружена не поддерживаемая операция " + operation.ToString());
                }
            }

            return resNodes;
        }


        /// <summary>
        /// Очистка кэша
        /// </summary>
        public static void ClearCache()
        {
            _cacheContainer.Clear();
        }

        /// <summary>
        /// Контрол является оригинальной частью ссылки, 
        /// т.е. не добавлен через операцию добавления
        /// </summary>
        /// <param name="xRefData"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsOrigOwnControl(XRefData xRefData, UnitAction control)
        {
            foreach (XRefOperation op in xRefData.Operations)
            {
                if (op is XRefOperationAdd)
                {
                    var addOp = op as XRefOperationAdd;
                    if (addOp.Item == control)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        /// <summary>
        /// Десерилизация поведения из файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static UnitBehavior ReadBehaviorFromFile(string path)
        {
            UnitBehavior container;
            XmlSerializer xmlSerelialize = new XmlSerializer(typeof(UnitBehavior), Helper.GetExtraTypes());
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                container = xmlSerelialize.Deserialize(reader) as UnitBehavior;
                container.ToAbsolutePaths(path);
            }
            return container;
        }
    }
}
