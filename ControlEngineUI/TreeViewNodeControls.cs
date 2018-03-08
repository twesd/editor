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

namespace ControlEngineUI
{
    /// <summary>
    /// Узел дерева контролов
    /// </summary>
    [Serializable]
    class TreeViewNodeControls : TreeViewModelNode
    {
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


        /// <summary>
        /// Иконка узла
        /// </summary>
        public Image Icon
        {
            get 
            { 
                Icon icon = GetIcon();
                Icon resIcon = new Icon(icon, 16, 16);
                return resIcon.ToBitmap();
            }
        }
        
        public TreeViewNodeControls(SerializableTreeNode sNode, TransactionManager transManager) :
            base(transManager)
        {
            Text = sNode.Text;
            Tag = sNode.Tag;
        }

        public TreeViewNodeControls(string text, object tag, TransactionManager transManager) :
            base(transManager)
        {
            Text = text;
            Tag = tag;
        }
        

        private System.Drawing.Icon GetIcon()
        {
            if (Tag is TreeViewNodeGroup)
            {
                return Resources.FolderIcon;
            }
            if (Tag is ControlTapScene)
            {
                return Resources.HandPointIcon;
            }
            if (Tag is ControlButton)
            {
                return Resources.ButtonIcon;
            }
            if (Tag is ControlImage)
            {
                return Resources.ImageIcon;
            }
            if (Tag is ControlBehavior)
            {
                return Resources.BehaviorIcon;
            }
            if (Tag is ControlText)
            {
                return Resources.TextIcon;
            }

            return Resources.ItemIcon;
        }
    }
}
