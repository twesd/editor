using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrrlichtWrap;

namespace ControlEngineUI.Transient
{
    /// <summary>
    /// Менеджер временной графики
    /// </summary>
    class TransientManager
    {
        public List<TransientGeometry> Geometries { get; private set; } 

        IrrDevice _irrDevice;

        public TransientManager(IrrDevice irrDevice)
        {
            if (irrDevice == null)
            {
                throw new ArgumentNullException();
            }
            _irrDevice = irrDevice;

            Geometries = new List<TransientGeometry>();
        }


        /// <summary>
        /// Отрисовка данных
        /// </summary>
        public void Draw()
        {
            foreach (TransientGeometry geom in Geometries)
            {
                if (!geom.IsVisible)
                {
                    continue;
                }
                geom.Draw(_irrDevice.DeviceW);
            }
        }
    }
}
