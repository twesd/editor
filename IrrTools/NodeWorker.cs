using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serializable;
using UnitEngine;

namespace IrrTools
{
    public class NodeWorker
    {
        protected SceneNodeW Node;

        public NodeWorker(SceneNodeW node)
        {
            Node = node;
        }

        /// <summary>
        /// Обновить контэйнер
        /// </summary>
        /// <param name="container"></param>
        public void Update(ContainerNode container)
        {
            container.Position = Convertor.CreateVertex(Node.GetPosition());
            container.Rotation = Convertor.CreateVertex(Node.GetRotation());
            container.Id = Node.Id;
        }

        /// <summary>
        /// Получить описание модели
        /// </summary>
        /// <returns></returns>
        public ContainerNode GetContainer(string path)
        {
            ContainerNode container = new ContainerNode(
                Node.Id,
                path,
                Convertor.CreateVertex(Node.GetPosition()),
                Convertor.CreateVertex(Node.GetRotation())
                );
            return container;
        }

        /// <summary>
        /// Применить данные из описания
        /// </summary>
        /// <param name="container"></param>
        public void Apply(ContainerNode container)
        {
            if (container == null) return;
            Node.SetPosition(Convertor.CreateVertex(container.Position));
            Node.SetRotation(Convertor.CreateVertex(container.Rotation));
        }


    }
}
