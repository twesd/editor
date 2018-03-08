using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Common
{    
    [Serializable]
    [XmlInclude(typeof(XRefOperationDelete))]
    [XmlInclude(typeof(XRefOperationAdd))]
    [XmlInclude(typeof(XRefOperationChange))]
    public abstract class XRefOperation
    {
    }
}
