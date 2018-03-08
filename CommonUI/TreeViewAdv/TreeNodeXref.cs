using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Drawing;
using CommonUI.Properties;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace CommonUI
{
    [Serializable]
    public class TreeNodeXref : TreeNodeBase
    {        
        [XmlIgnore]
        [IgnoreDataMember]
        public XRefData XRefData
        {
            get
            {
                return Tag as XRefData;
            }
        }

        /// <summary>
        /// Иконка узла
        /// </summary>
        public Image Icon
        {
            get
            {
                Icon icon = Resources.XRef;
                Icon resIcon = new Icon(icon, 16, 16);
                return resIcon.ToBitmap();
            }
        }

        public TreeNodeXref(XRefData xrefData)
        {
            if (xrefData == null)
            {
                throw new ArgumentNullException();
            }

            Tag = xrefData;
        }

        private TreeNodeXref()
        {
            Tag = new XRefData();
        }
    }
}
