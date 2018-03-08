using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUI;
using System.Drawing;
using ControlEngine;
using MainEditors.Properties;
using Serializable;
using TransactionCore;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Common;
using MainEditors;

namespace MainEditors.Main
{
    /// <summary>
    /// Узел дерева стадий
    /// </summary>
    [Serializable]
    class TreeViewNodeStage : TreeNodeBase
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
                return resIcon.ToBitmap();
            }
        }

        /// <summary>
        /// Выводимый текст
        /// </summary>
        public override string Text
        {
            get
            {
                StageItem stageItem = Tag as StageItem;
                if (stageItem == null)
                {
                    return string.Empty;
                }
                return System.IO.Path.GetFileName(stageItem.Path);
            }
            set { }
        }


        public TreeViewNodeStage(SerializableTreeNode sNode, TransactionManager transManager) :
            base(transManager)
        {
            Tag = sNode.Tag;
        }

        public TreeViewNodeStage(StageItem tag, TransactionManager transManager) :
            base(transManager)
        {
            Tag = tag;
        }


        private System.Drawing.Icon GetIcon()
        {
            if (Tag is TreeNodeGroup)
            {
                return Resources.IconFolder;
            }
            if (Tag is StageItem)
            {
                StageItem stageItem = Tag as StageItem;
                if (stageItem.IsStartStage)
                {
                    return Resources.IconStageStart;
                }
                else
                {
                    return Resources.IconItem;
                }
            }

            return Resources.IconItem;
        }
    }
}
