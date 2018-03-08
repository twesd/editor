using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUI;
using System.Drawing;
using StageEngine;
using Serializable;
using TransactionCore;
using StageEngineUI.Properties;

namespace StageEngineUI
{
    /// <summary>
    /// Узел дерева стадии
    /// </summary>
    [Serializable]
    class TreeNodeInstance : TreeNodeBase
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
                UnitInstanceBase instance = Tag as UnitInstanceBase;
                if (instance == null)
                {
                    return string.Empty;
                }
                return instance.Name;
            }
            set
            {
                UnitInstanceBase instance = Tag as UnitInstanceBase;
                if (instance != null &&
                    instance.Name != value)
                {
                    instance.Name = value;
                    NotifyModel();
                }
            }
        }

        Image _iconOverlay;

        public TreeNodeInstance(SerializableTreeNode sNode, TransactionManager transManager) :
            base(transManager)
        {
            Text = sNode.Text;
            Tag = sNode.Tag;
        }

        public TreeNodeInstance(UnitInstanceBase tag, TransactionManager transManager) :
            base(transManager)
        {
            Tag = tag;
        }


        private System.Drawing.Icon GetIcon()
        {
            //if (node.Tag is UnitInstanceEnv) return "home.ico";
            //if (node.Tag is UnitInstanceEmpty) return "home.ico";
            //if (node.Tag is UnitInstanceArea) return "area.ico";
            //if (node.Tag is UnitInstanceStandard) return "pers.ico";
            //if (node.Tag is UnitInstanceCamera)
            //{
            //    var camera = (node.Tag as UnitInstanceCamera);
            //    if (camera.Name == _mainEditor.StartCameraName)
            //        return "cameraStart.ico";
            //    else
            //        return "camera.ico";
            //}
            //return "item.ico";

            if (Tag is UnitInstanceEnv)
            {
                return Resources.IconHome;
            }
            else if (Tag is UnitInstanceEmpty)
            {
                return Resources.IconNone;
            }
            else if (Tag is UnitInstanceCamera)
            {
                return Resources.IconCamera;
            }

            return Resources.IconItem;
        }
    }
}
