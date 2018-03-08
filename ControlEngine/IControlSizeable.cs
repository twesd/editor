using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Common.Geometry;

namespace ControlEngine
{
    /// <summary>
    /// Используется, если контрол имеет видимые границы
    /// </summary>
    public interface IControlSizeable
    {
        Vertex Position { get; set; }

        Size GetSize();

        bool IsPointInside(int x, int y);
    }
}
