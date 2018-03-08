using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine.Events
{
    /// <summary>
    /// Описание события от кнопки
    /// </summary>
    [Serializable]
    public class UnitEventControlButton : UnitEventBase
    {
        /// <summary>
        /// Наименование кнопки
        /// </summary>
        public string ButtonName = string.Empty;

        /// <summary>
        /// Состояние
        /// </summary>
        public UnitEventControlButtonState State;

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="buttonName">Наименование кнопки</param>
        /// <param name="state">Состояние клавиши</param>
        public UnitEventControlButton(string buttonName, UnitEventControlButtonState state)
        {
            ButtonName = buttonName;
            State = state;
        }

        /// <summary>
        /// Для Serializable
        /// </summary>
        public UnitEventControlButton() { }

        public override string ToString()
        {
            return string.Format("Button [{0}] [{1}]", ButtonName, State.ToString());
        }
    }
}
