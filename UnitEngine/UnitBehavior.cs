using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using UnitEngine;
using System.ComponentModel;
using Common;
using Serializable;
using UnitEngine.Behavior;

namespace UnitEngine
{
    /// <summary>
    /// Поведение юнита
    /// </summary>
    [Serializable]
    public class UnitBehavior : IPathConvertible
    {
        /// <summary>
        /// Путь до файла модели
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Модель юнита")]
        public UnitModelBase UnitModel { get; set; }

        /// <summary>
        /// Индентификатор типа модели
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Индентификатор типа модели")]
        public int NodeId { get; set; }

        /// <summary>
        /// Дочерние юниты
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Дочерние юниты")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public List<string> ChildsBehaviorsPaths { get; set; }

        /// <summary>
        /// Условия установки параметров
        /// </summary>
        [CategoryAttribute("Параметры")]
        [DescriptionAttribute("Условия установки параметров из вне")]
        [Editor(typeof(UnitEngine.UITypeEditors.UITypeEditorUnitExprParameterCollection), typeof(System.Drawing.Design.UITypeEditor))]
        public List<UnitExprParameter> ExprParameterExt { get; set; }

        /// <summary>
        /// Начальные параметры
        /// </summary>
        [CategoryAttribute("Параметры")]
        [DescriptionAttribute("Начальные параметры")]
        [Editor(typeof(CommonUI.UITypeEditors.UITypeEditorParameters), typeof(System.Drawing.Design.UITypeEditor))]
        public List<Parameter> StartParameters { get; set; }

        /// <summary>
        /// Основная модель
        /// </summary>
        public ContainerNode MainModel;

        /// <summary>
        /// Модели окружающей среды
        /// </summary>
        public List<ContainerNode> EnviromentModels;

        /// <summary>
        /// Доступные анимации
        /// </summary>
        public List<UnitAnimation> Animations;

        /// <summary>
        /// Дерево объектов
        /// </summary>
        public ContainerTreeView TreeView;

        /// <summary>
        /// Описание положения камеры
        /// </summary>
        public ContainerCamera Camera;

        /// <summary>
        /// Скрипт для данного юнита
        /// </summary>
        public string ScriptFileName;

        /// <summary>
        /// Имя модуля для данного юнита
        /// </summary>
        public string ModuleName;

        /// <summary>
        /// Для Serializable
        /// </summary>
        public UnitBehavior() 
        {
            NodeId = 0;
            UnitModel = new UnitModelEmpty();
            ChildsBehaviorsPaths = new List<string>();            
            ExprParameterExt = new List<UnitExprParameter>();
            EnviromentModels = new List<ContainerNode>();
            StartParameters = new List<Parameter>();
            ScriptFileName = string.Empty;
            ModuleName = string.Empty;
            TreeView = new ContainerTreeView();
            Animations = new List<UnitAnimation>();
        }

        public UnitBehavior DeepClone()
        {
            return Common.SerializeWorker.Clone(this) as UnitBehavior;
        }

        /// <summary>
        /// Получить контэйнер с абсолютными путями
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public void ToAbsolutePaths(string root)
        {
            // Получаем абсолютные пути
            this.UnitModel.ToAbsolutePaths(root);

            if (this.MainModel != null)
                this.MainModel.Path = Common.UtilPath.GetAbsolutePath(MainModel.Path, root);
            List<ContainerNode> envNodes = new List<ContainerNode>();
            foreach (ContainerNode envNode in this.EnviromentModels)
                envNode.Path = Common.UtilPath.GetAbsolutePath(envNode.Path, root);

            this.ChildsBehaviorsPaths = Common.UtilPath.GetAbsolutePath(this.ChildsBehaviorsPaths, root);            
            this.ScriptFileName = Common.UtilPath.GetAbsolutePath(this.ScriptFileName, root);

            foreach (UnitAction action in this.GetItems())
                action.ToAbsolutePaths(root);

            foreach (XRefData xrefData in this.GetXRefs())
                xrefData.ToAbsolutePaths(root);

            foreach (UnitExprParameter exprParam in this.ExprParameterExt)
                exprParam.ToAbsolutePaths(root);

        }

        /// <summary>
        /// Получить контэйнер с относительными путями
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public void ToRelativePaths(string root)
        {
            // Получаем относительные пути
            //
            this.UnitModel.ToRelativePaths(root);

            if (this.MainModel != null)
                this.MainModel.Path = Common.UtilPath.GetRelativePath(MainModel.Path, root);
            List<ContainerNode> envNodes = new List<ContainerNode>();
            foreach (ContainerNode envNode in this.EnviromentModels)
                envNode.Path = Common.UtilPath.GetRelativePath(envNode.Path, root);

            this.ChildsBehaviorsPaths = Common.UtilPath.GetRelativePath(this.ChildsBehaviorsPaths, root);            
            this.ScriptFileName = Common.UtilPath.GetRelativePath(this.ScriptFileName, root);

            foreach (UnitAction action in this.GetItems())
                action.ToRelativePaths(root);

            foreach (XRefData xrefData in this.GetXRefs())
                xrefData.ToRelativePaths(root);

            foreach (UnitExprParameter exprParam in this.ExprParameterExt)
                exprParam.ToRelativePaths(root);
        }

        public List<UnitAction> GetItems()
        {
            List<UnitAction> unitActions = new List<UnitAction>();
            List<object> oList = TreeView.GetTags(typeof(UnitAction));
            if (oList == null || oList.Count == 0)
                return unitActions;
            return oList.Cast<UnitAction>().ToList();
        }

        public List<XRefData> GetXRefs()
        {
            List<XRefData> xrefs = new List<XRefData>();
            List<object> oList = TreeView.GetTags(typeof(XRefData));
            if (oList == null || oList.Count == 0)
                return xrefs;
            return oList.Cast<XRefData>().ToList();
        }

        /// <summary>
        /// Загрузить описания
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static UnitBehavior LoadFromFile(string path)
        {
            if (!File.Exists(path)) return null;
            UnitBehavior behaviors;
            XmlSerializer xmlSerelialize = new XmlSerializer(typeof(UnitBehavior), GetExtraTypes());
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                behaviors = xmlSerelialize.Deserialize(reader) as UnitBehavior;
            }
            return behaviors;
        }

        public static Type[] GetExtraTypes()
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(UnitAnimation));
            extraTypes.Add(typeof(UnitSound));
            extraTypes.Add(typeof(UnitEffect));
            extraTypes.Add(typeof(UnitAction));
            extraTypes.Add(typeof(ExecuteTransform));
            extraTypes.Add(typeof(XRefData));
            extraTypes.Add(typeof(GroupData));
            return extraTypes.ToArray();
        }
    }
}
