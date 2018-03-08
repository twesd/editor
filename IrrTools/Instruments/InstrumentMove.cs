using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrrlichtWrap;

namespace IrrTools
{
    /// <summary>
    /// Инструмент перемещения объекта
    /// </summary>
    public class InstrumentMove
    {
        /// <summary>
        /// Переместить объект
        /// </summary>
        /// <param name="irrDevice"></param>
        public static void Move(IrrDevice irrDevice)
        {
            IrrDeviceW deviceW = irrDevice.DeviceW;
            SceneNodeW node;
            if (deviceW.Selector.SelectionResult.Count == 1)
            {
                node = deviceW.Selector.SelectionResult[0];
            }
            else
            {
                node = irrDevice.Selector.SelectSingleNode();
            }
            if (node == null) return;

            Vertex3dW newPos = irrDevice.Editor.PickPoint(node.GetPosition().Y);
            if (newPos == null) return;
            if (newPos.X == 0 && newPos.Y == 0 && newPos.Z == 0) return;
            node.SetPosition(newPos);
        }
    }
}
