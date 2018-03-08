using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUI;
using System.Drawing;
using ControlEngine;
using ControlEngineUI.Properties;
using Serializable;
using TransactionCore;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Common;

namespace ControlEngineUI
{
    /// <summary>
    /// Узел дерева контролов
    /// </summary>
    [Serializable]
    class TreeViewNodeControls : TreeNodeBase
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
                ControlBase control = Tag as ControlBase;
                if (control == null)
                {
                    return string.Empty;                    
                }
                return control.Name;
            }
            set
            {
                ControlBase control = Tag as ControlBase;
                if (control != null &&
                    control.Name != value)
                {
                    control.Name = value;
                    NotifyModel();
                }
            }
        }

        Image _iconOverlay;
        
        public TreeViewNodeControls(SerializableTreeNode sNode, TransactionManager transManager) :
            base(transManager)
        {
            Text = sNode.Text;
            Tag = sNode.Tag;
        }

        public TreeViewNodeControls(ControlBase tag, TransactionManager transManager) :
            base(transManager)
        {
            Tag = tag;
        }
        

        private System.Drawing.Icon GetIcon()
        {
            if (Tag is ControlTapScene)
            {
                return Resources.IconHandPoint;
            }
            if (Tag is ControlButton)
            {
                return Resources.IconButton;
            }
            if (Tag is ControlImage)
            {
                return Resources.IconImage;
            }
            if (Tag is ControlBehavior)
            {
                return Resources.IconBehavior;
            }
            if (Tag is ControlText)
            {
                return Resources.IconText;
            }

            return Resources.IconItem;
        }
    }
}
