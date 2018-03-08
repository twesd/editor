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
using UnitEngine;

namespace StageEngineUI
{
    [Serializable]
    class TreeNodeToolboxUnit : TreeNodeBase
    {
        /// <summary>
        /// Иконка узла
        /// </summary>
        public Image Icon
        {
            get
            {
                if (_icon != null)
                {
                    return _icon;
                }

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
            set
            {
                _icon = new Bitmap(value, 16, 16);
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

        public string FilePath { get; set; }

        Image _icon;

        Image _iconOverlay;

        public TreeNodeToolboxUnit(string filePath, UnitBehavior tag, TransactionManager transManager) :
            base(transManager)
        {
            FilePath = filePath;
            Text = System.IO.Path.GetFileNameWithoutExtension(filePath);
            Tag = tag;
        }


        private System.Drawing.Icon GetIcon()
        {
            return Resources.IconItem;
        }
    }
}
