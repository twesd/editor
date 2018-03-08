using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUI;
using System.Drawing;
using Serializable;
using TransactionCore;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Common;
using UnitEngine;
using UnitEngineUI.Properties;

namespace UnitEngineUI
{
    /// <summary>
    /// Узел дерева поведения
    /// </summary>
    [Serializable]
    class TreeNodeAction : TreeNodeBase
    {
        /// <summary>
        /// Иконка узла
        /// </summary>
        public Image Icon
        {
            get
            {
                Icon icon = GetIcon();
                Icon resIcon = new Icon(icon, 16, 16);

                Image destImg = resIcon.ToBitmap();
                if (_iconOverlay != null)
                {
                    using (Graphics gr = Graphics.FromImage(destImg))
                    {
                        gr.DrawImage(_iconOverlay, new Point(0, 0));
                    }
                }

                return destImg;
            }
        }

        /// <summary>
        /// Иконка которая добавляется к основной
        /// </summary>
        public Image IconOverlay
        {
            get
            {
                return _iconOverlay;
            }
            set
            {
                _iconOverlay = value;
            }
        }

        /// <summary>
        /// Выводимый текст
        /// </summary>
        public override string Text
        {
            get
            {
                UnitAction action = Tag as UnitAction;
                if (action == null)
                {
                    return string.Empty;
                }
                return string.Format("{0} [{1}]", action.Name, action.Priority);                
            }
            set { }
        }

        Image _iconOverlay;

        public TreeNodeAction(SerializableTreeNode sNode, TransactionManager transManager) :
            base(transManager)
        {
            Text = sNode.Text;
            Tag = sNode.Tag;
        }

        public TreeNodeAction(UnitAction tag, TransactionManager transManager) :
            base(transManager)
        {
            Tag = tag;
        }


        private System.Drawing.Icon GetIcon()
        {
            if (Tag is UnitBlockAction)
            {
                return Resources.IconBlock;
            }
            
            if (Tag is UnitAction)
            {
                return Resources.IconItem;
            }

            return Resources.IconItem;
        }
    }
}
