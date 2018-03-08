using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnitEngine;
using ControlEngine;
using StageEngine;
using Serializable;

namespace StageEngine
{
    /// <summary>
    /// Описание стадии
    /// </summary>
    [Serializable]
    public class ContainerStage
    {
        /// <summary>
        /// Пути до поведений юнитов
        /// </summary>
        public List<string> UnitBehaviorPaths = new List<string>();

        /// <summary>
        /// Модели, которые будут подзагружены
        /// </summary>
        public List<string> CacheModelPaths = new List<string>();

        /// <summary>
        /// Текстуры, которые будут подзагружены
        /// </summary>
        public List<string> CacheTexturePaths = new List<string>();

        /// <summary>
        /// Пути до xml файлов, которые будут подзагружены
        /// </summary>
        public List<string> CacheXmlFiles = new List<string>();

        /// <summary>
        /// Пути до скриптовых файлов, которые будут подзагружены
        /// </summary>
        public List<string> CacheScripts = new List<string>();

        /// <summary>
        /// Дерево объектов
        /// </summary>
        public ContainerTreeView TreeView;

        /// <summary>
        /// Пакеты контролов
        /// </summary>
        public ControlPackages ControlPackages = new ControlPackages();

        /// <summary>
        /// Описание положения системной камеры
        /// </summary>
        public ContainerCamera Camera;

        /// <summary>
        /// Начальные глобальные параметры
        /// </summary>
        public List<Parameter> StartGlobalParameters = new List<Parameter>();

        /// <summary>
        /// Наименование начальной камеры
        /// </summary>
        public string StartCameraName = string.Empty;

        public ContainerStage()
        {
        }

        public ContainerStage DeepClone()
        {
            return SerializeWorker.Clone(this) as ContainerStage;
        }

        /// <summary>
        /// Получить контэйнер с абсолютными путями
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public ContainerStage GetWithAbsolutePaths(string root)
        {
            // Получаем абсолютные пути
            //
            ContainerStage container = this.DeepClone();
            container.ConvertTreeItemToAbsolutePath(container.TreeView.Nodes, root);
            container.UnitBehaviorPaths = Common.UtilPath.GetAbsolutePath(container.UnitBehaviorPaths, root);
            container.CacheModelPaths = Common.UtilPath.GetAbsolutePath(container.CacheModelPaths, root);
            container.CacheTexturePaths = Common.UtilPath.GetAbsolutePath(container.CacheTexturePaths, root);
            container.CacheXmlFiles = Common.UtilPath.GetAbsolutePath(container.CacheXmlFiles, root);
            container.CacheScripts = Common.UtilPath.GetAbsolutePath(container.CacheScripts, root);
            container.ControlPackages.ToAbsolutePaths(root);

            return container;
        }


        /// <summary>
        /// Получить контэйнер с относительными путями
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public ContainerStage GetWithRelativePaths(string root)
        {
            // Получаем относительные пути
            //
            ContainerStage container = this.DeepClone();

            container.ConvertTreeItemToRelativePath(container.TreeView.Nodes, root);
            container.UnitBehaviorPaths = Common.UtilPath.GetRelativePath(container.UnitBehaviorPaths, root);
            container.CacheModelPaths = Common.UtilPath.GetRelativePath(container.CacheModelPaths, root);
            container.CacheTexturePaths = Common.UtilPath.GetRelativePath(container.CacheTexturePaths, root);
            container.CacheXmlFiles = Common.UtilPath.GetRelativePath(container.CacheXmlFiles, root);
            container.CacheScripts = Common.UtilPath.GetRelativePath(container.CacheScripts, root);
            container.ControlPackages.ToRelativePaths(root);

            return container;
        }

        private void ConvertTreeItemToRelativePath(List<SerializableTreeNode> nodes, string root)
        {
            foreach (var node in nodes)
            {
                if (node.Tag is IPathConvertible)
                {
                    (node.Tag as IPathConvertible).ToRelativePaths(root);
                }
                else if (node.Tag is UnitInstanceCamera)
                {
                    // ничего
                }
                else if (node.Tag is UnitInstanceEmpty)
                {
                    // ничего
                }
                else if (node.Type == typeof(CommonUI.TreeNodeGroup).ToString())
                {
                    // ничего
                }
                else
                {
                    throw new NotSupportedException();
                }
                if (node.Nodes.Count > 0)
                    ConvertTreeItemToRelativePath(node.Nodes, root);
            }
        }

        private void ConvertTreeItemToAbsolutePath(List<SerializableTreeNode> nodes, string root)
        {
            foreach (var node in nodes)
            {
                if (node.Tag is IPathConvertible)
                {
                    (node.Tag as IPathConvertible).ToAbsolutePaths(root);
                }
                else if (node.Tag is UnitInstanceCamera)
                {
                    // ничего
                }
                else if (node.Tag is UnitInstanceEmpty)
                {
                    // ничего
                }
                else if (node.Type == typeof(CommonUI.TreeNodeGroup).ToString())
                {
                    // ничего
                }
                else
                {
                    throw new NotSupportedException();
                }
                if (node.Nodes.Count > 0)
                    ConvertTreeItemToAbsolutePath(node.Nodes, root);
            }
        }

        /// <summary>
        /// Обновить вспомогательные значения в дереве
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="behaviorList"></param>
        public void UpdateTreeViewTags(List<SerializableTreeNode> nodes, Dictionary<string, UnitBehavior> behaviorDict)
        {
            foreach (var node in nodes)
            {
                if (node.Tag is UnitInstanceStandard)
                {
                    var instance = node.Tag as UnitInstanceStandard;

                    string behaviorPath = instance.BehaviorsPath;
                    UnitBehavior behavior = LoadBehavior(behaviorDict, behaviorPath);
                    if (behavior != null)
                    {
                        instance.Behavior = behavior;
                    }
                }
                if (node.Nodes.Count > 0)
                {
                    UpdateTreeViewTags(node.Nodes, behaviorDict);
                }
            }
        }

        private static UnitBehavior LoadBehavior(Dictionary<string, UnitBehavior> behaviorDict, string behaviorPath)
        {
            UnitBehavior behavior = null;
            if (!behaviorDict.Keys.Contains(behaviorPath))
            {
                behavior = UnitBehavior.LoadFromFile(behaviorPath);
                if (behavior != null)
                {
                    behavior.ToAbsolutePaths(behaviorPath);
                    behaviorDict.Add(behaviorPath, behavior);
                }
                else
                {
                    return null;
                }
            }

            behavior = behaviorDict[behaviorPath].DeepClone();

            return behavior;
        }

    }
}
