using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlEngineUI.Transient
{
    abstract class TransientGeometry
    {
        public bool IsVisible { get; set; }

        public TransientGeometry()
        {
            IsVisible = true;
        }

        public abstract void Draw(IrrDeviceW _irrDevice);
    }
}
