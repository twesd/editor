using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Xml.Serialization;
using UnitEngine.Behavior;

namespace UnitEngine
{
    [XmlInclude(typeof(UnitBlockAction))]
    [Serializable]
    public class UnitAction : IPathConvertible, ISupportId
    {
        /// <summary>
        /// Индентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Индентификатор анимации
        /// </summary>
        public string AnimationId { get; set; }

        /// <summary>
        /// Приоритет
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Неизменять текущее действие
        /// </summary>
        public bool NoChangeCurrentAction { get; set; }

        /// <summary>
        /// Условия
        /// </summary>
        public UnitClause Clause { get; set; }

        /// <summary>
        /// Выполнить действия
        /// </summary>
        public List<ExecuteBase> Executes { get; set; }

        /// <summary>
        /// Условия отмены действия
        /// </summary>
        public UnitActionBreak Break { get; set; }

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="name"></param>
        public UnitAction(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Иницилизация
        /// </summary>
        public UnitAction() 
        {
            Id = Guid.NewGuid().ToString("B").ToUpper();
            Name = string.Empty;
            AnimationId = string.Empty;
            Priority = 0;
            NoChangeCurrentAction = false;
            Clause = new UnitClause();
            Executes = new List<ExecuteBase>();
            Break = new UnitActionBreak();
        }

        /// <summary>
        /// Deep clone
        /// </summary>
        /// <returns></returns>
        public UnitAction DeepClone()
        {
            return SerializeWorker.Clone(this) as UnitAction;
        }

        /// <summary>
        /// Конвертировать в относительные пути
        /// </summary>
        /// <param name="root"></param>
        public void ToRelativePaths(string root)
        {
            Break.ToRelativePaths(root);
            Clause.ToRelativePaths(root);
            foreach (var execute in Executes)
            {
                execute.ToRelativePaths(root);
            }
        }

        /// <summary>
        /// Конвертировать в абсолютные пути
        /// </summary>
        /// <param name="root"></param>
        public void ToAbsolutePaths(string root)
        {
            Break.ToAbsolutePaths(root);
            Clause.ToAbsolutePaths(root);
            foreach (var execute in Executes)
            {
                execute.ToAbsolutePaths(root);
            }
        }
    }
}
