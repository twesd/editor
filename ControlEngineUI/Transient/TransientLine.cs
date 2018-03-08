using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;
using IrrTools;

namespace ControlEngineUI.Transient
{
    /// <summary>
    /// Линия
    /// </summary>
    class TransientLine : TransientGeometry
    {
        public Vertex Start { get; set; }

        public Vertex End { get; set; }

        public int Width { get; set; }

        public UInt32 Color { get; set; }

        public TransientLine(Vertex start, Vertex end, UInt32 color, int width = 1)
        {
            if (start == null)
            {
                throw new ArgumentNullException();
            }
            if (end == null)
            {
                throw new ArgumentNullException();
            }
            Start = start;
            End = end;
            Color = color;
            Width = width;
        }

        public override void Draw(IrrDeviceW _irrDevice)
        {
            _irrDevice.Controls.AddLine(
                Convertor.CreateVertex(Start),
                Convertor.CreateVertex(End),
                Width,
                Color);
        }
    }
}
