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
    /// Узел дерева анимации
    /// </summary>
    [Serializable]
    class TreeNodeAnimation : TreeNodeBase
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
                UnitAnimation anim = Tag as UnitAnimation;
                if (anim == null)
                {
                    return string.Empty;
                }
                return anim.Name;
            }
            set 
            {
                UnitAnimation anim = Tag as UnitAnimation;
                if (anim == null || string.IsNullOrEmpty(value))
                {
                    return;
                }
                anim.Name = value;
            }
        }

        Image _iconOverlay;

        public TreeNodeAnimation(SerializableTreeNode sNode, TransactionManager transManager) :
            base(transManager)
        {
            Text = sNode.Text;
            Tag = sNode.Tag;
        }

        public TreeNodeAnimation(UnitAnimation tag, TransactionManager transManager) :
            base(transManager)
        {
            Tag = tag;
        }
        
        private System.Drawing.Icon GetIcon()
        {
            return Resources.IconItem;
        }
    }
}
