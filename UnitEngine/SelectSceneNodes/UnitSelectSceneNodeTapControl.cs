using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine
{
    /// <summary>
    /// Выбор по значению в контролле TapScene
    /// </summary>
    [Serializable]
    public class UnitSelectSceneNodeTapControl : UnitSelectSceneNodeBase
    {
        /// <summary>
        /// Наименование контрола TapScene
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Наименование контрола TapScene")]
        public string TapSceneName { get; set; }

        public UnitSelectSceneNodeTapControl()
        {
            TapSceneName = string.Empty;
        }

        public override string ToString()
        {
            return string.Format("Выборка по TapScene [{0}]", TapSceneName);
        }
    }
}
