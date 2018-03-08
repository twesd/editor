using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Serializable
{
    [Serializable]    
    public class SerializableTreeNode
    {
        /// <summary>
        /// Текст узла
        /// </summary>
        public string Text;

        /// <summary>
        /// Дополнительные данные
        /// </summary>
        public object Tag;

        /// <summary>
        /// Дочернии узлы
        /// </summary>
        public List<SerializableTreeNode> Nodes = new List<SerializableTreeNode>();

        /// <summary>
        /// Тип объекта
        /// </summary>
        public string Type;

        public SerializableTreeNode(TreeNode node) 
        {
            Text = node.Text;
            Tag = node.Tag;
            Type = node.GetType().ToString();
            foreach (TreeNode childNode in node.Nodes)
            {
                Nodes.Add(new SerializableTreeNode(childNode));
            }
        }

        public SerializableTreeNode(string text, object tag, string type) 
        {
            Text = text;
            Tag = tag;
            Type = type;
        }

        public SerializableTreeNode() { }
    }
}
