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

namespace UnitEngineUI.Behavior
{
    /// <summary>
    /// Класс для работы с внешними ссылками
    /// </summary>
    class XrefBehaviorWorker
    {
        /// <summary>
        /// Добавить новую ссылку
        /// </summary>
        /// <param name="source"></param>
        /// <param name="xrefPath"></param>
        public static TreeNodeGroup NewXreference(UnitBehavior source, string xrefPath)
        {
            if (source.XRefPaths.Contains(xrefPath))
            {
                MessageBox.Show(string.Format("Ссылка '{0}' уже прикреплена", xrefPath));
                return null;
            }

            TreeNodeGroup group = CreateXRefGroup(xrefPath);

            UnitBehavior container = ReadBehaviorFromFile(xrefPath);// Десерелизуем данные 
            FillXrefNodes(xrefPath, group, container);

            source.XRefPaths.Add(xrefPath);

            return group;
        }

        private static TreeNodeGroup CreateXRefGroup(string xRefPath)
        {
            var xRefData = new XRefData()
            {
                FileName = xRefPath
            };
            TreeNodeGroup group = new TreeNodeGroup()
            {
                Text = System.IO.Path.GetFileName(xRefPath),
                Tag = xRefData
            };
            return group;
        }

        /// <summary>
        /// Заполнить данные
        /// </summary>
        /// <param name="xrefPath"></param>
        /// <param name="group"></param>
        /// <param name="container"></param>
        private static void FillXrefNodes(string xrefPath, TreeNodeGroup group, UnitBehavior container)
        {
            if (container == null) return;
            List<TreeNode> nodes = TreeViewWorker.GetTreeNodes(container.TreeView);
            foreach (TreeNode node in nodes)
            {
                UnitAction action = node.Tag as UnitAction;
                if (action != null)
                {
                    action.LinkPath = xrefPath;
                    group.Nodes.Add(node);
                }
            }
        }

        /// <summary>
        /// Обновление внешних сслылок
        /// </summary>
        /// <param name="source"></param>
        public static void UpdateXReferences(UnitBehavior source)
        {
            List<TreeNode> destNodes = TreeViewWorker.GetTreeNodes(source.TreeView);
            
            foreach (string xRefPath in source.XRefPaths)
            {
                UnitBehavior xRefBehavior = null;
                try
                {
                    xRefBehavior = ReadBehaviorFromFile(xRefPath);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(string.Format("Ссылка '{0}' не загружена.\n{1}",
                        xRefPath, ex.Message));
                }
                TreeNodeGroup group = FindInBehavior(destNodes, xRefPath);
                if (group == null)
                {
                    group = CreateXRefGroup(xRefPath);
                    destNodes.Add(group);
                }
                group.Nodes.Clear();
                FillXrefNodes(xRefPath, group, xRefBehavior);

                source.TreeView = new ContainerTreeView(destNodes);
            }
        }

        /// <summary>
        /// Поиск ссылки в дереве
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="xRefPath"></param>
        /// <returns></returns>
        private static TreeNodeGroup FindInBehavior(List<TreeNode> nodes, string xRefPath)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is XRefData)
                {
                    XRefData xrefData = node.Tag as XRefData;
                    if (string.Compare(xrefData.FileName, xRefPath, true) == 0)
                        return node as TreeNodeGroup;
                }
            }
            return null;
        }

        /// <summary>
        /// Десерилизация поведения из файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static UnitBehavior ReadBehaviorFromFile(string path)
        {
            UnitBehavior container;
            XmlSerializer xmlSerelialize = new XmlSerializer(typeof(UnitBehavior), GetExtraTypes());
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                container = xmlSerelialize.Deserialize(reader) as UnitBehavior;
                container = container.GetWithAbsolutePaths(path);
            }
            return container;
        }

        private static Type[] GetExtraTypes()
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(UnitAnimation));
            extraTypes.Add(typeof(UnitSound));
            extraTypes.Add(typeof(UnitEffect));
            extraTypes.Add(typeof(UnitAction));
            extraTypes.Add(typeof(ExecuteBase));
            return extraTypes.ToArray();
        }
    }
}
