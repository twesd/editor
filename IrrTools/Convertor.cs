using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;
using Serializable;

namespace IrrTools
{
    public class Convertor
    {
        /// <summary>
        /// Преобразовать из Vertex3dW в GeometryEngine.Vertex
        /// </summary>
        /// <param name="irrVertex"></param>
        /// <returns></returns>
        public static Vertex CreateVertex(Vertex3dW irrVertex)
        {
            return new Vertex(irrVertex.X, irrVertex.Y, irrVertex.Z);
        }

        /// <summary>
        /// Преобразовать из GeometryEngine.Vertex в Vertex3dW
        /// </summary>
        /// <param name="irrVertex"></param>
        /// <returns></returns>
        public static Vertex3dW CreateVertex(Vertex irrVertex)
        {
            return new Vertex3dW(irrVertex.X, irrVertex.Y, irrVertex.Z);
        }


        public static BoundboxW CreateBoundbox(Boundbox boundbox)
        {
            return new BoundboxW(
                CreateVertex(boundbox.MinPoint),
                CreateVertex(boundbox.MaxPoint));
        }
    }
}
