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
using Common.Geometry;

namespace StageEngine
{
    /// <summary>
    /// Поведение камеры - следовать за моделью
    /// </summary>
    [Serializable]
    public class CameraBehaviorFollowToNode : CameraBehaviorBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Имя юнита")]
        public string UnitInstanceName { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Поворачивать камеру с поворотом юнита")]
        public bool RotateWithUnit { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Фильтр препятствий")]
        public int ObstacleFilterId { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Размер 2d карты")]
        public uint MapSize2d { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Размер 3d карты")]
        public float MapSize3d { get; set; }

        public CameraBehaviorFollowToNode()
        {
            UnitInstanceName = string.Empty;
            RotateWithUnit = false;
            MapSize2d = 256;
            MapSize3d = 256.0f;
            ObstacleFilterId = 0;
        }

        public override string ToString()
        {
            return "Cледовать за моделью";
        }
    }
}
