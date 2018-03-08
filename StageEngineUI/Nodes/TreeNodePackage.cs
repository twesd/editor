using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUI;
using System.Drawing;
using StageEngine;
using StageEngineUI.Properties;
using Serializable;
using TransactionCore;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Common;
using ControlEngine;

namespace StageEngineUI
{
    [Serializable]
    class TreeNodePackage : TreeNodeBase
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
        public override string Text { get; set; }

        Image _iconOverlay;

        public TreeNodePackage(SerializableTreeNode sNode, TransactionManager transManager) :
            base(transManager)
        {
            Text = sNode.Text;
            Tag = sNode.Tag;
        }

        public TreeNodePackage(string text, ControlPackage tag, TransactionManager transManager) :
            base(transManager)
        {
            Text = text;
            Tag = tag;
        }


        private System.Drawing.Icon GetIcon()
        {
            return Resources.IconControlPackage;
        }
    }
}
