using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serializable;
using IrrTools;
using StageEngine;

namespace StageEngineUI
{
    public class NodeWorkerEx : NodeWorker
    {
        SceneNodeW _node;

        public NodeWorkerEx(SceneNodeW node) : base(node)
        {
            _node = node;
        }

        /// <summary>
        /// Обновить экземпляр юнита
        /// </summary>
        /// <param name="container"></param>
        public void Update(UnitInstanceBase unitInstance)
        {
            unitInstance.StartPosition = Convertor.CreateVertex(_node.GetPosition());
            unitInstance.StartRotation = Convertor.CreateVertex(_node.GetRotation());
            unitInstance.EditorModelId = _node.Id;
        }
       
        /// <summary>
        /// Применить данные из описания
        /// </summary>
        /// <param name="instance"></param>
        public void Apply(UnitInstanceBase instance)
        {
            if (instance == null) return;
            _node.SetPosition(Convertor.CreateVertex(instance.StartPosition));
            _node.SetRotation(Convertor.CreateVertex(instance.StartRotation));
            if (instance is UnitInstanceBillboard)
            {
                var billboardInstance = instance as UnitInstanceBillboard;
                var billboardW = (_node as BillboardW);

                billboardW.SetUseUpVector(billboardInstance.UseUpVector);
                billboardW.SetUpVector(Convertor.CreateVertex(billboardInstance.UpVector));

                billboardW.SetUseViewVector(billboardInstance.UseViewVector);
                billboardW.SetViewVector(Convertor.CreateVertex(billboardInstance.ViewVector));

                billboardW.SetDimension(billboardInstance.Width, billboardInstance.Height);

                billboardW.SetMaterialType((int)billboardInstance.MaterialType);
                if (System.IO.File.Exists(billboardInstance.Texture))
                {
                    billboardW.SetTexture(0, billboardInstance.Texture);
                }
            }
        }
            

    }
}
