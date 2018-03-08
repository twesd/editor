using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Получение данных
    /// </summary>
    [XmlInclude(typeof(DataGetterTapControl))]
    [XmlInclude(typeof(DataGetterId))]
    [XmlInclude(typeof(DataGetterDistance))]
    [Serializable]
    public class DataGetterBase
    {
        public DataGetterBase()
        { 
        }
    }
}
