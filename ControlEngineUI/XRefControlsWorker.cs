using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUI;
using Common;
using ControlEngine;
using TransactionCore;
using System.Reflection;
using ControlEngineUI.Properties;

namespace ControlEngineUI
{
    class XRefControlsWorker
    {
        static Dictionary<string, ControlPackage> _cachePackages = new Dictionary<string, ControlPackage>();

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
            if(xRefData == null)
            {
                return;
            }

            // Перезагружаем данные
            //

            ControlPackage package = null;
            if (_cachePackages.ContainsKey(xRefData.FileName))
            {
                package = _cachePackages[xRefData.FileName];
            }
            else
            {
                package = ControlPackageWorker.Read(xRefData.FileName);
                _cachePackages[xRefData.FileName] = package;
            }
            if (package == null)
            {
                return;
            }

            List<TreeNodeBase> nodes = TreeViewConvertor.ConvertToNodes(
                package.TreeView.Nodes,
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
                    var control = addOp.Item as ControlBase;
                    if (control == null)
                    {
                        continue;
                    }

                    // Устанавливаем доп. иконку для отображения, что объект добавлен
                    //
                    var newNode = new TreeViewNodeControls(control, transManager);
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

                    if (changeTreeViewNode is TreeViewNodeControls)
                    {
                        // Устанавливаем доп. иконку для отображения, что объект изменён
                        //
                        var controlTreeViewNode = changeTreeViewNode as TreeViewNodeControls;
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
            _cachePackages = new Dictionary<string, ControlPackage>();
        }

        /// <summary>
        /// Контрол является оригинальной частью ссылки, 
        /// т.е. не добавлен через операцию добавления
        /// </summary>
        /// <param name="xRefData"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsOrigOwnControl(XRefData xRefData, ControlBase control)
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
    }
}
