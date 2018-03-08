using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine.Events
{
    /// <summary>
    /// Описание события от клика по сцене
    /// </summary>
    [Serializable]
    public class UnitEventControlTapScene : UnitEventBase
    {
        /// <summary>
        /// Наименование контрола
        /// </summary>
        public string TapSceneName = string.Empty;

        /// <summary>
        /// Игнорировать модель
        /// </summary>
        public bool IgnoreNode = false;

        /// <summary>
        /// Модели должны совпадать
        /// </summary>
        public bool IdentNode = true;

        /// <summary>
        /// Индентификатор типа модели
        /// </summary>
        public int FilterId = -1;

        /// <summary>
        /// Сохранить модель в данных
        /// </summary>
        public string DataName = string.Empty;
        
        /// <summary>
        /// Состояние
        /// </summary>
        public UnitEventControlButtonState State = UnitEventControlButtonState.Down;

        public override string ToString()
        {
            return string.Format("TapScene [{0}] [{1}]", TapSceneName, State.ToString());
        }
    }
}
