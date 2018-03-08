using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CommonUI.Properties;
using Serializable;
using Common;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace CommonUI
{
    [Serializable]
    public class TreeNodeGroup : TreeNodeBase
    {
        [IgnoreDataMember]
        [XmlIgnore]
        public GroupData GroupData
        {
            get
            {
                return Tag as GroupData;
            }
        }


        /// <summary>
        /// Иконка узла
        /// </summary>
        public Image Icon
        {
            get
            {
                Icon icon = Resources.FolderIcon;
                Icon resIcon = new Icon(icon, 16, 16);
                return resIcon.ToBitmap();
            }
        }

        public TreeNodeGroup(GroupData groupData)
        {
            if (groupData == null)
            {
                throw new ArgumentNullException();
            }
            Tag = groupData;
        }

        private TreeNodeGroup()
        {
            Tag = new GroupData();
        }
    }
}
