﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Common;

namespace UnitEngine
{
    /// <summary>
    /// Выбор по расстоянию между моделями
    /// </summary>
    [Serializable]
    public class UnitSelectSceneNodeDistance : UnitSelectSceneNodeBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Дистанция")]
        public float Distance { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Тип сравнения")]
        public CompareType CompareType { get; set; }

        /// <summary>
        /// Учитывать размеры юнитов
        /// </summary>
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Учитывать размеры юнитов")]
        public bool UseUnitSize { get; set; }

        public UnitSelectSceneNodeDistance()
        {
            Distance = 1;
            CompareType = CompareType.Less;
            UseUnitSize = false;
        }

        public override string ToString()
        {
            return string.Format("Выборка по дистанции [{0}]({1})", Distance, CompareType);
        }
    }
}
