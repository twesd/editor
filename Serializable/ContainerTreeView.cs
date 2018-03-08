using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Serializable
{
    [Serializable]

    public class ContainerTreeView
    {
        /// <summary>
        /// Узлы для серелизации
        /// </summary>
        public List<SerializableTreeNode> Nodes = new List<SerializableTreeNode>();

        /// <summary>
        /// Дополнительные данные
        /// </summary>
        public Object CustomData;
        
        public ContainerTreeView() { }

        public List<object> GetTags(Type type)
        {
            return GetTags(type, Nodes);
        }

        public List<object> GetTags(Type type, List<SerializableTreeNode> nodes)
        {
            List<object> outNodes = new List<object>();
            if (nodes == null || nodes.Count == 0) return outNodes;
            foreach (SerializableTreeNode node in nodes)
            {
                if (node.Tag != null && CompareType(node.Tag.GetType(), type))
                {
                    outNodes.Add(node.Tag);
                }
                outNodes.AddRange(GetTags(type, node.Nodes));
            }
            return outNodes;
        }

        bool CompareType(Type type1, Type type2)
        {
            if (type1 == type2) return true;
            if (type1.BaseType != null)
                return CompareType(type1.BaseType, type2);
            return false;
        }
    }
}
