using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine
{
    /// <summary>
    /// Описание участка анимации
    /// </summary>
    [Serializable]
    public class UnitAnimation
    {
        public string Id;

        /// <summary>
        /// Наименование анимации
        /// </summary>
        public string Name;

        /// <summary>
        /// Начальный кадр
        /// </summary>
        public int StartFrame;

        /// <summary>
        /// Конечный кадр
        /// </summary>
        public int EndFrame;

        /// <summary>
        /// Скорость анимации
        /// </summary>
        public int Speed;

        /// <summary>
        /// Повторять движение
        /// </summary>
        public bool Loop;

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="name">Наименование анимации</param>
        /// <param name="startFrame">Начальный кадр</param>
        /// <param name="endFrame">Конечный кадр</param>
        /// <param name="speed">Скорость анимации</param>
        /// <param name="loop">Повторять движение</param>
        public UnitAnimation(string name, int startFrame, int endFrame, int speed, bool loop)
        {
            Id = Guid.NewGuid().ToString("B").ToUpper();
            Name = name;
            StartFrame = startFrame;
            EndFrame = endFrame;
            Loop = loop;
            Speed = speed;
        }

        /// <summary>
        /// Иницилизация (Для Serializable)
        /// </summary>
        private UnitAnimation() { }

        public override string ToString()
        {
            return Name;
        }
    }
}
