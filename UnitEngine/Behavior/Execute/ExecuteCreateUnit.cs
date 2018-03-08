using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Создать новый юнит
    /// </summary>
    [Serializable]
    public class ExecuteCreateUnit : ExecuteBase
    {
        /// <summary>
        /// Тип создания юнита
        /// </summary>
        public enum CreationTypeEnum
        {
            /// <summary>
            /// Создать дочерний юнит
            /// </summary>
            Child,
            /// <summary>
            /// Создать внешний юнит
            /// </summary>
            External
        }

        /// <summary>
        /// Путь до файла поведения
        /// </summary>
        public string BehaviorsPath;

        /// <summary>
        /// Позиция (относительно основного юнита)
        /// </summary>
        public Vertex Position = new Vertex();

        /// <summary>
        /// Поворот (относительно основного юнита)
        /// </summary>
        public Vertex Rotation = new Vertex();

        /// <summary>
        /// Позволить создавать несколько экземпляров
        /// </summary>
        public bool AllowSeveralInstances = false;
        
        /// <summary>
        /// Получить позицию из TapScene
        /// </summary>
        public bool GetPositionFromTapScene = false;

        /// <summary>
        /// Наименование TapScene
        /// </summary>
        public string TapSceneName = string.Empty;

        /// <summary>
        /// Наименование кости к оторой должен быть присоеденён юнит
        /// </summary>
        public string JointName = string.Empty;

        /// <summary>
        /// Тип создания
        /// </summary>
        public CreationTypeEnum CreationType = CreationTypeEnum.Child;

        /// <summary>
        /// Начальный скрипт
        /// </summary>
        public string StartScriptFileName = string.Empty;

        public ExecuteCreateUnit() { }
        
        public override void ToRelativePaths(string root)
        {
            BehaviorsPath = Common.UtilPath.GetRelativePath(BehaviorsPath, root);
            StartScriptFileName = Common.UtilPath.GetRelativePath(StartScriptFileName, root);
        }

        public override void ToAbsolutePaths(string root)
        {
            BehaviorsPath = Common.UtilPath.GetAbsolutePath(BehaviorsPath, root);
            StartScriptFileName = Common.UtilPath.GetAbsolutePath(StartScriptFileName, root);
        }

        public override string ToString()
        {
            return string.Format("Добавить юнит {0}",
                System.IO.Path.GetFileNameWithoutExtension(BehaviorsPath));
        }
    }
}
