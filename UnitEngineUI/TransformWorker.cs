using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitEngine;
using UnitEngine.Behavior;

namespace UnitEngineUI
{
    /// <summary>
    /// Класс для работы с трансформациями
    /// </summary>
    class TransformWorker
    {
        /// <summary>
        /// Установить трансформацию
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="model"></param>
        public static void SetTransform(ExecuteTransform transform, SceneNodeW model)
        {
            if (transform == null || model == null) return;
            List<Vertex3dW> points = new List<Vertex3dW>();
            List<UInt32> times = new List<UInt32>();
            foreach (TransformItem item in transform.Items)
            {
                points.Add(new Vertex3dW(item.X, item.Y, item.Z));
                times.Add(item.Time);
            }
            model.AddTransform((int)transform.Type, points, times);
        }
    }
}
